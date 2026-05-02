using System;
using System.Windows.Forms;

namespace SecurIT_Memory.UI
{
    /// <summary>
    /// Formulaire de configuration permettant de définir les paramètres de la partie.
    /// </summary>
    public partial class FormOptions : Form
    {
        // Propriété publique en lecture seule : c'est elle que FormMenu consultera
        // pour récupérer la nouvelle taille de grille après la fermeture.
        public int TailleChoisie { get; private set; }

        /// <summary>
        /// Constructeur : reçoit la taille actuelle pour pré-cocher le bon bouton.
        /// </summary>
        /// <param name="tailleActuelle">La taille (ex: 4 ou 6) actuellement configurée.</param>
        public FormOptions(int tailleActuelle)
        {
            InitializeComponent();
            this.Text = "Options de Sécurité";
            this.StartPosition = FormStartPosition.CenterParent;

            // Pré-sélection dynamique basée sur l'état actuel du jeu
            if (tailleActuelle == 6)
            {
                rb6x6.Checked = true;
            }
            else
            {
                rb4x4.Checked = true; // Par défaut
            }
        }

        /// <summary>
        /// Gestionnaire du clic sur le bouton Valider.
        /// </summary>
        private void btnValider_Click(object sender, EventArgs e)
        {
            // 1. On capture la valeur choisie selon le RadioButton coché
            if (rb6x6.Checked)
            {
                TailleChoisie = 6;
            }
            else
            {
                TailleChoisie = 4;
            }

            // 2. On définit le résultat du dialogue sur OK. 
            // Cela ferme automatiquement le formulaire et informe FormMenu de la validation.
            this.DialogResult = DialogResult.OK;
        }

        private void InitializeComponent()
        {
            btnValider = new Button();
            btnAnnuler = new Button();
            rb4x4 = new RadioButton();
            rb6x6 = new RadioButton();
            SuspendLayout();
            // 
            // btnValider
            // 
            btnValider.Location = new Point(152, 191);
            btnValider.Name = "btnValider";
            btnValider.Size = new Size(94, 29);
            btnValider.TabIndex = 0;
            btnValider.Text = "Valider";
            btnValider.UseVisualStyleBackColor = true;
            btnValider.Click += btnValider_Click;
            // 
            // btnAnnuler
            // 
            btnAnnuler.Location = new Point(24, 191);
            btnAnnuler.Name = "btnAnnuler";
            btnAnnuler.Size = new Size(94, 29);
            btnAnnuler.TabIndex = 1;
            btnAnnuler.Text = "Annuler";
            btnAnnuler.UseVisualStyleBackColor = true;
            btnAnnuler.Click += btnAnnuler_Click;
            // 
            // rb4x4
            // 
            rb4x4.AutoSize = true;
            rb4x4.Location = new Point(12, 64);
            rb4x4.Name = "rb4x4";
            rb4x4.Size = new Size(92, 24);
            rb4x4.TabIndex = 2;
            rb4x4.TabStop = true;
            rb4x4.Text = "Grille 4x4";
            rb4x4.UseVisualStyleBackColor = true;
            // 
            // rb6x6
            // 
            rb6x6.AutoSize = true;
            rb6x6.Location = new Point(164, 64);
            rb6x6.Name = "rb6x6";
            rb6x6.Size = new Size(92, 24);
            rb6x6.TabIndex = 3;
            rb6x6.TabStop = true;
            rb6x6.Text = "Grille 6x6";
            rb6x6.UseVisualStyleBackColor = true;
            // 
            // FormOptions
            // 
            ClientSize = new Size(292, 253);
            Controls.Add(rb6x6);
            Controls.Add(rb4x4);
            Controls.Add(btnAnnuler);
            Controls.Add(btnValider);
            Name = "FormOptions";
            ResumeLayout(false);
            PerformLayout();

        }

        /// <summary>
        /// Bouton Annuler : on ferme simplement sans rien valider.
        /// </summary>
        private void btnAnnuler_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private Button btnValider;
        private Button btnAnnuler;
        private RadioButton rb4x4;
        private RadioButton rb6x6;
    }
}

