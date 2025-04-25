
# 🧠 Explication technique détaillée - Movie Library (WPF MVVM)

Ce document explique les différents composants techniques de l'application **Movie Library**, avec un focus sur les éléments clés comme la navigation, les services, la configuration, les templates, et le stockage local.

---

## 🔁 Navigation centralisée (NavigationService)

### Objectif :
Permet de changer dynamiquement la vue affichée dans l'application sans couplage direct entre les vues.

### Structure :
- `INavigationHost` : interface avec `ObservableObject CurrentViewModel`
- `MainViewModel` : implémente `INavigationHost`
- `NavigationService` : reçoit un `INavigationHost` en paramètre, et modifie `CurrentViewModel`
- `App.xaml.cs` : initialise la navigation via `AppServices`

### Exemple :

```csharp
_navigation.NavigateTo(new LoginViewModel(...));
```

Cela met à jour `MainViewModel.CurrentViewModel` et affiche automatiquement la vue associée grâce aux `DataTemplate`.

---

## 🧩 AppServices - Injection manuelle centralisée

### Objectif :
Centraliser la création et la gestion des services partagés.

### Services gérés :
- `AppDbContext`
- `AuthService`
- `MovieServiceApi`
- `NavigationService`

### Initialisation :
```csharp
AppServices.Init(MainViewModel);
```

Ensuite dans les ViewModels :
```csharp
AppServices.Auth
AppServices.MovieService
AppServices.Navigation
```

---

## 🔀 AppConfiguration - Lecture de la config JSON

### Objectif :
Lire un fichier `appsettings.json` non versionné contenant la clé API.

### Exemple de contenu :

```json
{
  "ApiKeys": {
    "TheMovieDb": "ta_clé"
  }
}
```

### Chargement :
```csharp
var config = AppConfiguration.Load();
var apiKey = config.ApiKeys.TheMovieDb;
```

---

## 🖼️ Liaison automatique Vue / ViewModel

### Objectif :
Afficher automatiquement la bonne vue selon le type du ViewModel.

### Dans `Resources/ViewTemplates.xaml` :

```xml
<DataTemplate DataType="{x:Type viewmodels:LoginViewModel}">
    <views:LoginView />
</DataTemplate>
```

### Dans `MainWindow.xaml` :

```xml
<ContentControl Content="{Binding CurrentViewModel}" />
```

➡️ WPF détecte le type du ViewModel et applique le `DataTemplate` correspondant.

---

## 🧱 BaseViewModel

### Objectif :
Fournir un socle commun pour tous les ViewModels avec des états génériques.

### Propriétés :
- `IsBusy` : pour un chargement en cours
- `ErrorMessage` / `SuccessMessage`
- `IsEmpty` (optionnel)

➡️ Permet de factoriser et d'afficher des retours utilisateurs facilement.

---

## 📁 Organisation modulaire

Les vues et leurs ViewModels sont regroupés par **fonctionnalité** :

```
Auth/
├── LoginView.xaml
├── LoginViewModel.cs
├── RegisterView.xaml
├── RegisterViewModel.cs
```

Idem pour `Home/`, `Dashboard/`, etc.

---

## 💾 Accès base de données

### ORM utilisé : Entity Framework Core avec SQLite

### Entités principales :
- `User` (Id, Nom, Email, MotDePasse)
- `Movie` (Id, Titre, Résumé, Poster)
- `UserMovie` (UserId, MovieId, Statut)

### Exemple d’initialisation :
```csharp
db.Database.EnsureCreated();
```

---

## 🔄 Services

### `AuthService`
- Crée un utilisateur
- Authentifie un utilisateur
- Hash le mot de passe

### `MovieServiceApi`
- Appelle TheMovieDB
- Transforme les DTO en modèles locaux

### `MovieServiceJson`
- Alternative pour tests hors ligne via un fichier `.json`

---

## 🎨 Ressources globales

Dans `Resources/` :
- `Styles.xaml` → Styles boutons et UI
- `Effects.xaml` → Effets visuels (ombres)
- `ViewTemplates.xaml` → Liens ViewModel ↔ Vue
- `Converters/` → Converters XAML (bool → visibilité)

---

