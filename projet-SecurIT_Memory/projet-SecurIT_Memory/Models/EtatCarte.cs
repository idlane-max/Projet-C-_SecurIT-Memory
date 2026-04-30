// Fichier : Models/EtatCarte.cs
namespace SecurIT_Memory.Models
{
    /// <summary>
    /// Représente les différents états possibles d'une carte sur le plateau.
    /// </summary>
    public enum EtatCarte
    {
        Cachee,  // Face verso, icône invisible 
        Revelee, // Temporairement visible pendant le tour du joueur 
        Trouvee  // Paire identifiée, reste affichée jusqu'à la fin 
    }
}