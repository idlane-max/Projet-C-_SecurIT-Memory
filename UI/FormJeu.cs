// Fichier : UI/FormJeu.cs
using System;
using System.Drawing; // Nécessaire pour manipuler les tailles (Size) et positions (Point)
using System.Windows.Forms;
using SecurIT_Memory.Models; // Pour accéder à nos classes JeuMemory et Carte

namespace SecurIT_Memory.UI
{
    public partial class FormJeu : Form
    {
        // Notre moteur de jeu, bien encapsulé
        private JeuMemory _moteurJeu;

        public FormJeu(int lignes, int colonnes)
        {
            InitializeComponent();
            this.Text = "SecurIT Memory - En jeu";
            this.StartPosition = FormStartPosition.CenterParent;

            // 1. Validation de sécurité (Programmation défensive)
            int totalCartes = lignes * colonnes;
            if (totalCartes % 2 != 0)
            {
                throw new ArgumentException("Le nombre total de cartes doit être pair pour jouer au Memory !");
            }

            // 2. Calcul du nombre de paires et initialisation du moteur logique
            int nombreDePaires = totalCartes / 2;
            _moteurJeu = new JeuMemory(nombreDePaires);

            // 3. Génération visuelle
            GenererGrille(lignes, colonnes);
        }

        /// <summary>
        /// Génère dynamiquement les PictureBox sur le formulaire en fonction de la taille demandée.
        /// </summary>
        private void GenererGrille(int lignes, int colonnes)
        {
            // Configuration de la taille d'une carte et de l'espacement
            int largeurCarte = 100;
            int hauteurCarte = 140;
            int marge = 10;
            int indexCarte = 0; // Pour parcourir notre liste de cartes mélangées

            // On ajuste automatiquement la taille de la fenêtre pour s'adapter à la grille
            this.ClientSize = new Size(
                (largeurCarte + marge) * colonnes + marge, 
                (hauteurCarte + marge) * lignes + marge
            );

            // Double boucle pour créer la grille (Lignes x Colonnes)
            for (int i = 0; i < lignes; i++)
            {
                for (int j = 0; j < colonnes; j++)
                {
                    // On récupère la carte logique correspondante (déjà mélangée par JeuMemory)
                    Carte carteModele = _moteurJeu.Cartes[indexCarte];

                    // Création du contrôle visuel
                    PictureBox picBox = new PictureBox
                    {
                        Width = largeurCarte,
                        Height = hauteurCarte,
                        // Calcul des coordonnées (X, Y)
                        Location = new Point(
                            marge + j * (largeurCarte + marge), 
                            marge + i * (hauteurCarte + marge)
                        ),
                        SizeMode = PictureBoxSizeMode.StretchImage,
                        Cursor = Cursors.Hand,
                        BackColor = Color.SlateGray, // Couleur par défaut pour simuler le dos de la carte
                        
                        // C'EST ICI LA MAGIE : On attache l'objet métier au contrôle visuel !
                        Tag = carteModele 
                    };

                    // On abonne la PictureBox à notre événement de clic
                    picBox.Click += Carte_Click;

                    // On ajoute le contrôle visuel au formulaire
                    this.Controls.Add(picBox);
                    
                    indexCarte++;
                }
            }
        }

        /// <summary>
        /// Événement déclenché quand le joueur clique sur N'IMPORTE QUELLE carte de la grille.
        /// </summary>
        private void Carte_Click(object sender, EventArgs e)
        {
            // 'sender' représente le contrôle qui a déclenché l'événement.
            // On sait que c'est une PictureBox, on la convertit (cast).
            PictureBox picBoxCliquee = sender as PictureBox;

            if (picBoxCliquee != null)
            {
                // On récupère notre objet 'Carte' lié à cette PictureBox grâce à la propriété Tag
                Carte carteCliquee = picBoxCliquee.Tag as Carte;

                // On évite un crash si le cast échoue
                if (carteCliquee == null) return;

                // TODO Étape 5 : Implémenter la logique de retournement et appeler JeuMemory
                MessageBox.Show($"Tu as cliqué sur la carte ID : {carteCliquee.Id}");
            }
        }
    }
}