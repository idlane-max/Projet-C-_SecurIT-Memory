# Projet-C#_SecurIT-Memory

![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![Windows Forms](https://img.shields.io/badge/Windows%20Forms-5C2D91?style=for-the-badge&logo=.net&logoColor=white)
![Visual Studio](https://img.shields.io/badge/Visual%20Studio-5C2D91?style=for-the-badge&logo=visual-studio&logoColor=white)

## 📖 Contexte du Projet

[cite_start]Ce projet a été développé dans le cadre d'une mission pour **SecurIT**, une start-up spécialisée en cybersécurité[cite: 4]. [cite_start]L'objectif est de fournir à l'équipe marketing un mini-jeu interactif pour animer leur stand lors du prochain Salon de l'Innovation Tech[cite: 5].

[cite_start]Il s'agit d'un jeu de Memory mettant en scène des concepts clés de la cybersécurité (mots de passe, pare-feu, virus, etc.)[cite: 6]. [cite_start]Le but est de captiver les visiteurs tout en testant leur mémoire, et de démontrer nos compétences en développement orienté objet avec C# et l'interface WinForms[cite: 7, 19].

---

## 🚀 Fonctionnalités Principales

* **Menu interactif** : Lancement d'une partie, accès aux options et fermeture propre.
* **Tailles de grille dynamiques** : Possibilité de jouer en 4x4 (16 cartes) ou 6x6 (36 cartes).
* **Moteur de jeu robuste** : Mélange aléatoire des cartes via l'algorithme de Fisher-Yates.
* **Interface fluide** : Gestion des délais de mémorisation via `System.Windows.Forms.Timer` sans bloquer l'interface.
* **Statistiques en direct** : Suivi du chronomètre et du nombre d'essais.
* **Optimisation GDI+** : Nettoyage proactif de la mémoire vidéo (`Dispose()`) pour éviter les fuites.

---

## 🛠️ Prérequis

Pour compiler et exécuter ce projet, vous aurez besoin de :
* [cite_start]**IDE** : Visual Studio 2022 (Community, Professional ou Enterprise)[cite: 7].
* **Framework** : .NET 6.0 ou supérieur (ou .NET Framework 4.8 selon votre configuration de création de projet).
* **OS** : Windows 10 / Windows 11 (Requis pour l'exécution de WinForms).

---

## ⚙️ Instructions de Lancement

1.  **Cloner le dépôt** :
    ```bash
    git clone [https://github.com/](https://github.com/)[VotreNomDUtilisateur]/SecurIT-Memory.git
    ```
2.  **Ouvrir le projet** : Double-cliquez sur le fichier `SecurIT_Memory.sln` pour l'ouvrir dans Visual Studio.
3.  **Restaurer les paquets** : Laissez Visual Studio restaurer les packages NuGet si nécessaire.
4.  **Lancer l'application** : Appuyez sur `F5` ou cliquez sur le bouton "Démarrer" en haut de Visual Studio.

---

## 🏗️ Architecture du Projet

[cite_start]Le projet respecte une architecture stricte en **3 couches** pour garantir la séparation des responsabilités et la maintenabilité[cite: 39]:

1.  [cite_start]**Interface de Jeu (Dossier `UI`)** : Gère uniquement l'affichage visuel (WinForms), les clics et les timers[cite: 40].
2.  [cite_start]**Logique & Données (Dossier `Models`)** : Contient le moteur de jeu pur et les classes métiers, sans aucune dépendance à l'interface graphique[cite: 42].
3.  [cite_start]**Ressources** : Stockage des images thématiques[cite: 77].

### 📂 Arborescence Détaillée

```text
SecurIT_Memory/
│
├── 📄 Program.cs             # Point d'entrée de l'application, lance FormMenu
│
├── 📁 Models/                # Cœur logique du jeu (indépendant de l'interface)
│   ├── 📄 EtatCarte.cs       # Enumération sécurisant les états : Cachee, Revelee, Trouvee
│   ├── 📄 Carte.cs           # Modèle métier encapsulant l'ID, l'état et l'image d'une carte
│   └── 📄 JeuMemory.cs       # Moteur gérant la liste des cartes, le mélange et les règles
│
├── 📁 UI/                    # Interface Graphique (WinForms)
│   ├── 📄 FormMenu.cs        # Écran d'accueil gérant la navigation
│   ├── 📄 FormOptions.cs     # Fenêtre de configuration (taille de la grille)
│   └── 📄 FormJeu.cs         # Fenêtre principale générant la grille et gérant l'interaction
│
└── 📁 Resources/
    └── 📁 Images/            # Icônes de cybersécurité (virus.png, parefeu.png, etc.)