namespace SecurIT_Memory.UI
{
    partial class FormMenu
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblTitre = new Label();
            btnJouer = new Button();
            btnOptions = new Button();
            btnQuitter = new Button();
            SuspendLayout();
            // 
            // lblTitre
            // 
            lblTitre.AutoSize = true;
            lblTitre.Font = new Font("Segoe UI", 20F);
            lblTitre.Location = new Point(298, 21);
            lblTitre.Name = "lblTitre";
            lblTitre.Size = new Size(265, 46);
            lblTitre.TabIndex = 0;
            lblTitre.Text = "SecurIT Memory";
            lblTitre.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnJouer
            // 
            btnJouer.Location = new Point(92, 265);
            btnJouer.Name = "btnJouer";
            btnJouer.Size = new Size(159, 29);
            btnJouer.TabIndex = 1;
            btnJouer.Text = "Nouvelle Partie";
            btnJouer.UseVisualStyleBackColor = true;
            btnJouer.Click += btnJouer_Click;
            // 
            // btnOptions
            // 
            btnOptions.Location = new Point(391, 265);
            btnOptions.Name = "btnOptions";
            btnOptions.Size = new Size(94, 29);
            btnOptions.TabIndex = 2;
            btnOptions.Text = "Options";
            btnOptions.UseVisualStyleBackColor = true;
            btnOptions.Click += btnOptions_Click;
            // 
            // btnQuitter
            // 
            btnQuitter.Location = new Point(618, 265);
            btnQuitter.Name = "btnQuitter";
            btnQuitter.Size = new Size(94, 29);
            btnQuitter.TabIndex = 3;
            btnQuitter.Text = "Quitter";
            btnQuitter.UseVisualStyleBackColor = true;
            btnQuitter.Click += btnQuitter_Click;
            // 
            // FormMenu
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.MenuHighlight;
            ClientSize = new Size(800, 450);
            Controls.Add(btnQuitter);
            Controls.Add(btnOptions);
            Controls.Add(btnJouer);
            Controls.Add(lblTitre);
            Name = "FormMenu";
            Text = "FormMenu";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitre;
        private Button btnJouer;
        private Button btnOptions;
        private Button btnQuitter;
    }
}
