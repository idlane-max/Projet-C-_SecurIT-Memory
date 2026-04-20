// Fichier : Models/EtatCarte.cs
namespace SecurIT_Memory.Models
{
    /// <summary>
    /// Représente les différents états possibles d'une carte sur le plateau.
    /// </summary>
    public enum EtatCarte
    {
        [cite_start]Cachee,  // Face verso, icône invisible 
        [cite_start]Revelee, // Temporairement visible pendant le tour du joueur 
        [cite_start]Trouvee  // Paire identifiée, reste affichée jusqu'à la fin 
    }
}