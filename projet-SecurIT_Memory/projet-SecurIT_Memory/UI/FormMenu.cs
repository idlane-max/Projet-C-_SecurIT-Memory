// Fichier : UI/FormMenu.cs
using SecurIT_Memory.UI;
using System;
using System.Windows.Forms;

namespace SecurIT_Memory.UI
{
    /// <summary>
    /// Formulaire principal servant de point d'entrée et de menu pour le jeu.
    /// </summary>
    public partial class FormMenu : Form
    {
        // On stocke la taille de la grille en variable privée.
        // Par défaut, c'est du 4x4. Cette valeur sera potentiellement modifiée par FormOptions.
        private int _lignesGrille = 4;
        private int _colonnesGrille = 4;

        public FormMenu()
        {
            InitializeComponent();
            // Bonne pratique : donner un titre propre à la fenêtre
            this.Text = "SecurIT Memory - Menu Principal";
            this.StartPosition = FormStartPosition.CenterScreen; // Centrer la fenêtre au lancement
        }

        /// <summary>
        /// Événement déclenché au clic sur le bouton "Jouer".
        /// </summary>
        private void btnJouer_Click(object sender, EventArgs e)
        {
            // 1. On cache le menu principal pour ne pas encombrer l'écran
            this.Hide();

            // 2. On instancie le jeu en lui passant la taille de la grille (Rappelle-toi notre choix d'architecture !)
            // Utilisation d'un bloc 'using' : c'est une excellente pratique en C# pour s'assurer 
            // que les ressources visuelles de FormJeu sont libérées de la mémoire dès qu'on le ferme.
            using (FormJeu fenetreJeu = new FormJeu(_lignesGrille, _colonnesGrille))
            {
                // 3. On ouvre le jeu de manière bloquante
                fenetreJeu.ShowDialog();
            }

            // 4. Une fois FormJeu fermé par l'utilisateur, le code reprend ici. On réaffiche le menu.
            this.Show();
        }

        /// <summary>
        /// Événement déclenché au clic sur le bouton "Options".
        /// </summary>
        private void btnOptions_Click(object sender, EventArgs e)
        {
            // On ouvre les options de manière modale. 
            // On passera les paramètres actuels si on veut que la fenêtre d'options affiche le réglage en cours.
            using (FormOptions fenetreOptions = new FormOptions(_lignesGrille))
            {
                // Si l'utilisateur clique sur "Valider" dans les options (DialogResult.OK)
                if (fenetreOptions.ShowDialog() == DialogResult.OK)
                {
                    // On met à jour notre configuration avec ce qui a été choisi
                    _lignesGrille = fenetreOptions.TailleChoisie;
                    _colonnesGrille = fenetreOptions.TailleChoisie;
                }
            }
        }

        /// <summary>
        /// Événement déclenché au clic sur le bouton "Quitter".
        /// </summary>
        private void btnQuitter_Click(object sender, EventArgs e)
        {
            // On demande confirmation avant de quitter, c'est plus professionnel
            DialogResult resultat = MessageBox.Show(
                "Êtes-vous sûr de vouloir quitter SecurIT Memory ?",
                "Confirmation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resultat == DialogResult.Yes)
            {
                // Ferme l'application proprement
                Application.Exit();
            }
        }
    }
}