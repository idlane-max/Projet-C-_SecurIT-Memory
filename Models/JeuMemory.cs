// Fichier : Models/JeuMemory.cs

// Nécessaire pour la classe Random (génération de nombres pseudo-aléatoires)
using System; 
// Nécessaire pour l'utilisation des collections génériques comme List<T>
using System.Collections.Generic; 

namespace SecurIT_Memory.Models
{
    /// <summary>
    /// Moteur principal du jeu Memory. Gère la collection de cartes et les règles.
    /// </summary>
    public class JeuMemory
    {
        // On expose la liste des cartes en lecture seule pour l'extérieur.
        // Seule cette classe a le droit de modifier la liste (ajouter/supprimer/mélanger).
        public List<Carte> Cartes { get; private set; }

        // Générateur de nombres aléatoires réutilisé pour éviter l'instanciation multiple
        // qui pourrait générer les mêmes séquences si appelée trop vite.
        private readonly Random _random;

        public JeuMemory(int nombreDePaires)
        {
            Cartes = new List<Carte>();
            _random = new Random();
            
            InitialiserCartes(nombreDePaires);
            MelangerCartesFisherYates();
        }

        /// <summary>
        /// Crée les paires de cartes et les ajoute à la liste.
        /// </summary>
        private void InitialiserCartes(int nombreDePaires)
        {
            // Dans un vrai projet, ces chemins proviendraient d'une configuration.
            // Pour l'instant, on simule une banque d'images de cybersécurité.
            string[] banqueImages = { 
                "Images/virus.png", "Images/parefeu.png", "Images/cadenas.png", 
                "Images/mdp.png", "Images/hacker.png", "Images/phishing.png",
                "Images/vpn.png", "Images/cle_rsa.png"
            };

            for (int i = 0; i < nombreDePaires; i++)
            {
                // On utilise le modulo pour éviter une erreur si on demande plus 
                // de paires qu'on a d'images différentes.
                string imagePath = banqueImages[i % banqueImages.Length];

                // On crée deux cartes identiques (une paire) avec le MÊME Id.
                // L'Id est simplement 'i', ce qui nous permet de savoir qu'elles vont ensemble.
                Cartes.Add(new Carte(i, imagePath));
                Cartes.Add(new Carte(i, imagePath));
            }
        }

        /// <summary>
        /// Mélange la liste de cartes en utilisant l'algorithme robuste de Fisher-Yates.
        /// </summary>
        private void MelangerCartesFisherYates()
        {
            int n = Cartes.Count;
            // On parcourt la liste à l'envers
            while (n > 1)
            {
                n--;
                // On choisit un index aléatoire entre 0 et n inclus
                int k = _random.Next(n + 1);
                
                // On échange la carte à l'index n avec la carte à l'index k
                Carte carteTemporaire = Cartes[k];
                Cartes[k] = Cartes[n];
                Cartes[n] = carteTemporaire;
            }
        }

        /// <summary>
        /// Vérifie si deux cartes forment une paire valide.
        /// </summary>
        public bool VerifierPaire(Carte carte1, Carte carte2)
        {
            // Règle métier : deux cartes forment une paire si elles ont le même Id
            // ET qu'on ne compare pas exactement la même instance de carte (double clic).
            if (carte1.Id == carte2.Id && carte1 != carte2)
            {
                carte1.Etat = EtatCarte.Trouvee;
                carte2.Etat = EtatCarte.Trouvee;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Réinitialise l'état de deux cartes à "Cachee" suite à une erreur du joueur.
        /// Appelée par l'interface graphique une fois le délai de mémorisation écoulé.
        /// </summary>
        /// <param name="carte1">La première carte sélectionnée.</param>
        /// <param name="carte2">La deuxième carte sélectionnée.</param>
        public void ReinitialisationCartes(Carte carte1, Carte carte2)
        {
            // On vérifie par sécurité qu'on ne cache pas des cartes déjà trouvées
            if (carte1.Etat != EtatCarte.Trouvee)
            {
                carte1.Etat = EtatCarte.Cachee;
            }
            
            if (carte2.Etat != EtatCarte.Trouvee)
            {
                carte2.Etat = EtatCarte.Cachee;
            }

            // Note pour le bonus : C'est ici que l'on pourra ajouter la logique
            // du "Mode Hardcore" pour échanger les positions de ces cartes à l'avenir !
        }
    }
}