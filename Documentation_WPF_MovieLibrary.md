
# 📘 Documentation technique – Application WPF "Movie Library"

## 🧩 Objectif de l'application

Créer une application WPF MVVM (.NET 8) permettant :

- d'afficher des films aléatoires depuis TheMovieDB,
- de créer un compte utilisateur (nom, email, mot de passe),
- de se connecter et gérer deux listes : « vus » et « à voir »,
- de stocker les informations en base locale (SQLite),
- de disposer d'une interface moderne, modulaire et scalable.

## 🗂️ Architecture générale

L'application suit l’architecture **MVVM modulaire** :

```
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

## 📌 Technologies utilisées

| Composant                 | Technologie                               |
|--------------------------|-------------------------------------------|
| UI                       | WPF (.NET 8)                              |
| Modèle MVVM              | CommunityToolkit.Mvvm                     |
| Base de données locale   | SQLite via Entity Framework Core          |
| API films                | TheMovieDB (https://developer.themoviedb.org) |
| Configuration            | Microsoft.Extensions.Configuration.Json   |

## 🏗️ Structure MVVM

Chaque fonctionnalité suit le pattern :

```
MaFonction/
├── MaFonctionView.xaml
├── MaFonctionViewModel.cs
```

Les vues sont automatiquement liées aux ViewModels via des `DataTemplate` définis dans `Resources/ViewTemplates.xaml`.

## 📂 Ressources et styles

Tous les styles, effets, converters sont centralisés dans :

- `Resources/Styles.xaml` — boutons, bordures, couleurs
- `Resources/Effects.xaml` — ombres (`DropShadowEffect`)
- `Resources/ViewTemplates.xaml` — lien ViewModel ↔ Vue
- `Resources/Converters/` — `BoolToVisibilityConverter`, etc.

## 🔐 Configuration

La clé API de TheMovieDB est stockée dans un fichier **non versionné** :

```json
{
  "ApiKeys": {
    "TheMovieDb": "ta_clé_tmdb"
  }
}
```

Le chargement se fait via :

```csharp
var config = AppConfiguration.Load();
var apiKey = config.ApiKeys.TheMovieDb;
```

## 🧠 Navigation

Gérée via `NavigationService`, découplée de `MainViewModel` grâce à l'interface :

```csharp
public interface INavigationHost
{
    ObservableObject CurrentViewModel { get; set; }
}
```

Le `MainViewModel` implémente cette interface, et `App.xaml.cs` initialise tout au démarrage.

## 🧩 Services partagés (DI simplifiée)

Tous les services sont centralisés dans `AppServices` :

```csharp
AppServices.Auth       // AuthService (gestion utilisateurs)
AppServices.Db         // AppDbContext
AppServices.Navigation // NavigationService
AppServices.MovieService // MovieServiceApi (TheMovieDB)
```

Initialisés une seule fois dans `App.xaml.cs` via :

```csharp
AppServices.Init(MainViewModel);
```

## 💾 Base de données (SQLite + EF Core)

📄 `Data/AppDbContext.cs`

Contient 3 entités principales :

- `User` (Nom, Email, MotDePasseHashé)
- `Movie` (Id, Title, Overview, PosterPath)
- `UserMovie` (UserId, MovieId, Statut = "Seen" ou "ToWatch")

La base est auto-créée au démarrage :

```csharp
db.Database.Migrate(); // ou EnsureCreated()
```

## 🔄 Fonctionnalités principales

| Fonction                        | Emplacement                               |
|--------------------------------|-------------------------------------------|
| Inscription                    | `RegisterViewModel` + `AuthService`       |
| Connexion                      | `LoginViewModel` + `AuthService`          |
| Page d’accueil (anonyme)       | `HomeViewModel` + `MovieServiceApi`       |
| Liste "vus / à voir"           | `DashboardViewModel`                      |
| Recherche + suggestions        | `SearchQuery` + `MovieSuggestions` (ComboBox) |
| Ajout/suppression/transition   | `SaveUserMovie()` / `RemoveUserMovie()`   |
| Stockage utilisateur SQLite    | `UserMovie` + `AppDbContext`              |

## ✅ UX

- `ProgressBar` liée à `IsBusy`
- `ErrorMessage` / `SuccessMessage` via `BaseViewModel`
- Réinitialisation automatique du champ de recherche après ajout
- Vue responsive avec `Grid.RowDefinitions` et `ColumnDefinitions`

## 🚀 Améliorations possibles

- Gestion d’avatars utilisateurs ou préférences
- Export JSON/XML des listes
- Intégration Blazor ou WebAPI pour synchronisation cloud
- Ajout de tests unitaires avec `Moq` sur les services
- Notification toast en bas via `Snackbar`/`MessageQueue`
