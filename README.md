
# 🎬 Movie Library - Application WPF MVVM (.NET 8)

Bienvenue dans **Movie Library**, une application WPF en architecture MVVM permettant de gérer une bibliothèque de films avec des listes de films vus et à voir, basée sur l'API TheMovieDB.

---

## 🚀 Fonctionnalités principales

- 🔍 Recherche de films via l'API de TheMovieDB
- ✅ Ajout de films à la liste "Vus"
- 📌 Ajout de films à la liste "À voir"
- 🔐 Création de compte utilisateur avec stockage local
- 🔑 Authentification et gestion de session
- 🧠 Navigation centralisée (via `NavigationService`)
- 💾 Stockage local avec SQLite via Entity Framework Core
- 🎨 UI moderne avec styles centralisés et effets
- 🗃️ Architecture MVVM modulaire par feature

---

## 📦 Packages NuGet nécessaires

```bash
dotnet add package CommunityToolkit.Mvvm
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet add package Microsoft.Extensions.Configuration
dotnet add package Microsoft.Extensions.Configuration.Json
dotnet add package Microsoft.Extensions.Configuration.Binder
```

---

## 🔧 Configuration API

Crée un fichier `appsettings.json` à la racine du projet avec ta clé API TMDB :

```json
{
  "ApiKeys": {
    "TheMovieDb": "ta_clé_api_tmdb_ici"
  }
}
```

Puis dans **les propriétés** du fichier :

- Action de génération : `Contenu`
- Copier dans le répertoire de sortie : `Toujours copier`

---

## 🏁 Lancement de l'application

1. Cloner le repo
2. Ajouter le fichier `appsettings.json`
3. S'assurer que les packages NuGet sont installés
4. Exécuter le projet

---

## 🧱 Architecture du projet

```plaintext
CESI_MoviesLibrary/
│
├── Auth/                // Authentification (Login, Register)
├── Dashboard/           // Gestion des listes de films
├── Home/                // Page d'accueil publique
├── Resources/           // Styles, effets, templates, converters
├── Dtos/                // Objets de transfert (TheMovieDB)
├── Data/                // AppDbContext et modèles EF
├── Configuration/       // Chargement des fichiers appsettings.json
├── Infrastructure/      // Services centralisés (AppServices)
├── Services/            // Logique applicative (navigation, auth, film)
├── ViewModels/          // ViewModels partagés ou abstraits
├── Views/               // Vues WPF
└── App.xaml             // MergedDictionaries & DataTemplates
```

---

## 🤝 Contribution

Les contributions sont les bienvenues ! Forkez, proposez des améliorations ou corrigez des bugs 👇

---

## 📄 Licence

MIT – Utilisation libre à des fins pédagogiques et personnelles.
