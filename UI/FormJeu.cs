using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq; // Pour la validation simplifiée de la victoire via .All()
using SecurIT_Memory.Models; // Accès au moteur JeuMemory et à la classe Carte

namespace SecurIT_Memory.UI
{
    /// <summary>
    /// Formulaire de jeu principal. Gère l'affichage dynamique, 
    /// les interactions utilisateur et le cycle de vie d'une partie.
    /// </summary>
    public partial class FormJeu : Form
    {
        // --- COMPOSANTS LOGIQUES ---
        private JeuMemory _moteurJeu;
        
        // --- COMPOSANTS VISUELS DYNAMIQUES ---
        private Label _lblChrono;
        private Label _lblEssais;
        
        // --- VARIABLES D'ÉTAT DU TOUR EN COURS ---
        private Carte _premiereCarte = null;
        private PictureBox _premierePicBox = null;
        private Carte _secondeCarte = null;
        private PictureBox _secondePicBox = null;

        private int _nbEssais = 0;
        private int _tempsEcouleSec = 0;

        // --- GESTION DU TEMPS ---
        private Timer _timerDelai;   // Délai avant de retourner les cartes si erreur
        private Timer _timerChrono;  // Chronomètre de la partie

        /// <summary>
        /// Constructeur injecté : impose les dimensions dès la création.
        /// </summary>
        public FormJeu(int lignes, int colonnes)
        {
            InitializeComponent();
            
            // Configuration de base de la fenêtre
            this.Text = "SecurIT Memory - Stand Salon Tech";
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedSingle; // Empêche le redimensionnement manuel
            this.MaximizeBox = false;

            // 1. Validation mathématique (Sécurité)
            int totalCartes = lignes * colonnes;
            if (totalCartes % 2 != 0)
            {
                throw new ArgumentException("Le nombre de cartes doit être pair pour former des paires !");
            }

            // 2. Initialisation du moteur et des timers
            _moteurJeu = new JeuMemory(totalCartes / 2);
            InitialiserTimers();

            // 3. Construction de l'interface
            GenererInterfaceDynamique(lignes, colonnes);

            // 4. Lancement de la partie
            _timerChrono.Start();
        }

        private void InitialiserTimers()
        {
            // Timer de mémorisation (1.5 seconde)
            _timerDelai = new Timer { Interval = 1500 };
            _timerDelai.Tick += TimerDelai_Tick;

            // Timer du chronomètre (1 seconde)
            _timerChrono = new Timer { Interval = 1000 };
            _timerChrono.Tick += TimerChrono_Tick;
        }

        private void GenererInterfaceDynamique(int lignes, int colonnes)
        {
            int largeurCarte = 100;
            int hauteurCarte = 140;
            int marge = 10;
            int hauteurHeader = 50; 
            int indexCarte = 0;

            // --- CRÉATION DU HEADER ---
            _lblChrono = new Label
            {
                Text = "Temps : 00:00",
                Location = new Point(marge, marge),
                AutoSize = true,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.DarkSlateGray
            };

            _lblEssais = new Label
            {
                Text = "Essais : 0",
                Location = new Point(marge + 180, marge),
                AutoSize = true,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.DarkSlateGray
            };

            this.Controls.Add(_lblChrono);
            this.Controls.Add(_lblEssais);

            // --- AJUSTEMENT TAILLE FENÊTRE ---
            this.ClientSize = new Size(
                (largeurCarte + marge) * colonnes + marge, 
                hauteurHeader + (hauteurCarte + marge) * lignes + marge
            );

            // --- GÉNÉRATION DE LA GRILLE ---
            for (int i = 0; i < lignes; i++)
            {
                for (int j = 0; j < colonnes; j++)
                {
                    Carte carte = _moteurJeu.Cartes[indexCarte];

                    PictureBox picBox = new PictureBox
                    {
                        Width = largeurCarte,
                        Height = hauteurCarte,
                        Location = new Point(
                            marge + j * (largeurCarte + marge), 
                            hauteurHeader + marge + i * (hauteurCarte + marge)
                        ),
                        SizeMode = PictureBoxSizeMode.StretchImage,
                        BackColor = Color.SlateGray, // Couleur dos de carte
                        Cursor = Cursors.Hand,
                        Tag = carte // On lie l'objet métier à la vue
                    };

                    picBox.Click += Carte_Click;
                    this.Controls.Add(picBox);
                    indexCarte++;
                }
            }
        }

        private void Carte_Click(object sender, EventArgs e)
        {
            // Sécurité : on ignore le clic si le timer tourne ou si l'objet est invalide
            if (_timerDelai.Enabled) return;

            PictureBox pb = sender as PictureBox;
            Carte c = pb?.Tag as Carte;

            if (c == null || c.Etat != EtatCarte.Cachee) return;

            // 1. Retourner la carte
            c.Etat = EtatCarte.Revelee;
            AfficherImageCarte(pb, c);

            // 2. Logique de sélection
            if (_premiereCarte == null)
            {
                _premiereCarte = c;
                _premierePicBox = pb;
            }
            else
            {
                _secondeCarte = c;
                _secondePicBox = pb;
                _nbEssais++;
                _lblEssais.Text = $"Essais : {_nbEssais}";

                // 3. Vérification
                if (_moteurJeu.VerifierPaire(_premiereCarte, _secondeCarte))
                {
                    ReinitialiserSelection();
                    VerifierVictoire();
                }
                else
                {
                    _timerDelai.Start(); // Laisse le temps au joueur de voir l'erreur
                }
            }
        }

        private void TimerDelai_Tick(object sender, EventArgs e)
        {
            _timerDelai.Stop();

            // Retour au modèle : on cache les cartes
            _moteurJeu.ReinitialisationCartes(_premiereCarte, _secondeCarte);

            // Retour à la vue : on enlève les images
            CacherImageCarte(_premierePicBox);
            CacherImageCarte(_secondePicBox);

            ReinitialiserSelection();
        }

        private void TimerChrono_Tick(object sender, EventArgs e)
        {
            _tempsEcouleSec++;
            TimeSpan t = TimeSpan.FromSeconds(_tempsEcouleSec);
            _lblChrono.Text = $"Temps : {t.Minutes:D2}:{t.Seconds:D2}";
        }

        private void VerifierVictoire()
        {
            if (_moteurJeu.Cartes.All(c => c.Etat == EtatCarte.Trouvee))
            {
                _timerChrono.Stop();
                MessageBox.Show($"Bravo ! Stand SecurIT sécurisé !\nTemps : {_lblChrono.Text}\nEssais : {_nbEssais}", "Victoire !");
                this.Close(); // Retour au menu
            }
        }

        /// <summary>
        /// Affiche l'image de la carte et gère la libération de la mémoire vidéo.
        /// </summary>
        private void AfficherImageCarte(PictureBox pb, Carte c)
        {
            try 
            { 
                // 1. Libération de l'image précédente pour éviter les fuites GDI+
                pb.Image?.Dispose(); 
                
                // 2. Chargement de la nouvelle image
                pb.Image = Image.FromFile(c.ImagePath); 
            }
            catch 
            { 
                // Fallback visuel si l'image est introuvable sur le disque
                pb.BackColor = Color.LightCoral; 
            } 
        }

        /// <summary>
        /// Cache l'image de la carte (remet le dos) et nettoie la mémoire.
        /// </summary>
        private void CacherImageCarte(PictureBox pb)
        {
            // On nettoie la ressource image de la face révélée avant de la retirer
            pb.Image?.Dispose();
            
            pb.Image = null;
            pb.BackColor = Color.SlateGray;
        }

        private void ReinitialiserSelection()
        {
            _premiereCarte = null; _premierePicBox = null;
            _secondeCarte = null; _secondePicBox = null;
        }
    }
}