using System;
using System.Windows.Forms;
using SecurIT_Memory.UI; // Indispensable pour trouver notre FormMenu

namespace SecurIT_Memory
{
    internal static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread] // Requis par Windows pour que l'interface graphique fonctionne correctement
        static void Main()
        {
            // Configuration de base pour un rendu visuel moderne sous Windows
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // C'est ici qu'on lance notre application en lui passant le menu principal
            Application.Run(new FormMenu());
        }
    }
}