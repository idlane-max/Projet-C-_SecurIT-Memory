// Fichier : Models/Carte.cs
using System; // Seul namespace de base nécessaire ici

namespace SecurIT_Memory.Models
{
    /// <summary>
    /// Représente une carte individuelle du jeu Memory.
    /// </summary>
    public class Carte
    {
        // L'identifiant sert à valider les paires. Deux cartes avec le même Id forment une paire[cite: 53, 54].
        // Le 'private set' empêche la triche ou les erreurs : l'ID ne peut pas changer après la création de la carte.
        public int Id { get; private set; }

        [cite_start]// Chemin vers l'image de l'icône de cybersécurité (ex: "Images/virus.png") 
        public string ImagePath { get; private set; }

        // L'état actuel de la carte. Seul l'état peut être modifié librement pendant la partie.
        public EtatCarte Etat { get; set; }

        /// <summary>
        /// Constructeur de la carte.
        /// </summary>
        /// <param name="id">Identifiant unique de la paire.</param>
        /// <param name="imagePath">Chemin vers la ressource visuelle.</param>
        public Carte(int id, string imagePath)
        {
            Id = id;
            ImagePath = imagePath;
            Etat = EtatCarte.Cachee; // Par défaut, une carte est toujours cachée au début 
        }
    }
}