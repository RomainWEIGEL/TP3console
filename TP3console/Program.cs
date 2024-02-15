using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Globalization;
using System.Diagnostics;
using TP3console.Models.EntityFramework;

namespace TP3Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
           /*using (var ctx = new FilmsDBContext())
            {
                ctx.ChangeTracker.QueryTrackingBehavior = Microsoft.EntityFrameworkCore.QueryTrackingBehavior.NoTracking;
                FilmPart titanic = ctx.Films.AsNoTracking().First(f => f.Nom.Contains("Titanic"));
                titanic.Description = "Un bateau échoué. Date : " + DateTime.Now;
                int nbchanges = ctx.SaveChanges();

                Console.WriteLine("Nombre d'enregistrement modifiés ou ajoutés :  " + nbchanges);

            }
            using (var ctx = new FilmsDBContext())
            {
                //Chargement de la catégorie Action
                CategoriePart categorieAction = ctx.Categories.First(c => c.Nom == "Action");
                Console.WriteLine("Categorie : " + categorieAction.Nom);
                Console.WriteLine("Films : ");
                //Chargement des films de la catégorie Action.
                foreach (var film in ctx.Films.Where(f => f.CategorieNavigation.Nom ==
               categorieAction.Nom).ToList())
                {
                    Console.WriteLine(film.Nom);
                }
            }


            using (var ctx = new FilmsDBContext())
            {
                CategoriePart categorieAction = ctx.Categories.First(c => c.Nom == "Action");
                Console.WriteLine("Categorie : " + categorieAction.Nom);
                //Chargement des films dans categorieAction
                ctx.Entry(categorieAction).Collection(c => c.Films).Load();
                Console.WriteLine("Films : ");
                foreach (var film in categorieAction.Films)
                {
                    Console.WriteLine(film.Nom);
                }
            }

            using (var ctx = new FilmsDBContext())
            {
                //Chargement de la catégorie Action et des films de cette catégorie
                CategoriePart categorieAction = ctx.Categories
                .Include(c => c.Films)
                .First(c => c.Nom == "Action");
                Console.WriteLine("Categorie : " + categorieAction.Nom);
                Console.WriteLine("Films : ");
                foreach (var film in categorieAction.Films)
                {
                    Console.WriteLine(film.Nom);
                }
            }
            Console.ReadKey();
           */
            Exo2Q1();
            Console.ReadKey();
        }
        public static void Exo2Q1()
        {
            var ctx = new FilmsDBContext();
            foreach (var film in ctx.Films)
            {
                Console.WriteLine(film.ToString());
            }
        }

        //Autre possibilité :
        public static void Exo2Q1Bis()
        {
            var ctx = new FilmsDBContext();
            //Pour que cela marche, il faut que la requête envoie les mêmes noms de colonnes que les classes c#.
            var films = ctx.Films.FromSqlRaw("SELECT * FROM film");
            foreach (var film in films)
            {
                Console.WriteLine(film.ToString());
            }
        }
        public static void Exo2Q2()
        {
            var ctx = new FilmsDBContext();
            foreach (var utilisateur in ctx.Utilisateurs)
            {
                Console.WriteLine(utilisateur.Email);
            }
        }

        public static void Exo2Q3()
        {
            var ctx = new FilmsDBContext();
            foreach (var utilisateur in ctx.Utilisateurs.OrderBy(u => u.Login))
            {
                Console.WriteLine(utilisateur.Login);
            }
        }

        public static void Exo2Q4()
        {
            var ctx = new FilmsDBContext();
            foreach (var films in ctx.Films.Where(s => s.Categorie == 1))
            {
                Console.WriteLine(films.ToString());
            }
        }

        public static void Exo2Q5()
        {
            var ctx = new FilmsDBContext();
            int nb = 0;
            foreach (var categorie in ctx.Categories)
            {
                nb++;
            }
            Console.WriteLine("Il y a " + nb + " catégories");
        }

        public static void Exo2Q6()
        {
            var ctx = new FilmsDBContext();
            Console.WriteLine("La note la plus basse est la : " + ctx.Avis.Min(s => s.Note));
        }

        public static void Exo2Q7()
        {
            var ctx = new FilmsDBContext();
            /*CultureInfo ci = new CultureInfo("fr-FR")
            var films = ctx.Films.Where(s => s.Nom.StartsWith("LE", true, ci));*/ // Auttre manière de le faire
            var films = ctx.Films.Where(s => s.Nom.ToUpper().StartsWith("LE"));
            foreach (var film in films)
            {
                Console.WriteLine(film.Nom);
            }
        }

        public static void Exo2Q8()
        {
            var ctx = new FilmsDBContext();
            Film id_film = ctx.Films.First(s => s.Nom.ToUpper() == "PULP FICTION");
            ctx.Entry(id_film).Collection(s => s.Avis).Load();
            Console.WriteLine(id_film.Avis.Average(s => s.Note));
        }

        public static void Exo2Q9()
        {
            var ctx = new FilmsDBContext();
            Console.WriteLine(ctx.Utilisateurs.First(s => s.Id == ctx.Avis.First(s => s.Note == ctx.Avis.Max(s => s.Note)).Utilisateur));
        }

        public static void AjoutUtilisateur()
        {
            using (var ctx = new FilmsDBContext())
            {
                var user = new Utilisateur()
                {
                    Login = "Simon",
                    Email = "simon.goy@etu.univ-smb.fr",
                    Pwd = "nop"
                };
                ctx.Add(user);

                ctx.SaveChanges();
            }
        }

        public static void ModifFilm()
        {
            using (var ctx = new FilmsDBContext())
            {
                var film = ctx.Films.First<Film>(s => s.Nom.ToLower() == "l'armee des douze singes");
                var cate = ctx.Categories.First(ctx => ctx.Nom == "Drame").Id;
                film.Categorie = cate;
                ctx.SaveChanges();
            }
        }

        public static void SupprimeFilm()
        {
            using (var ctx = new FilmsDBContext())
            {
                var film = ctx.Films.First<Film>(s => s.Nom.ToLower() == "l'armee des douze singes");
                var id = film.Id;
                var avis = ctx.Avis.Where(s => s.Film == id);
                ctx.Films.Remove(film);
                foreach (var aviss in avis)
                    ctx.Avis.Remove(aviss);
                ctx.SaveChanges();
            }
        }

        public static void AjouterAvis()
        {
            using (var ctx = new FilmsDBContext())
            {
                var avis = new Avi()
                {

                    Film = 3,
                    Utilisateur = 1,
                    Avis = "Il est bien",
                    Note = 0.8M
                };
                ctx.Add(avis);

                ctx.SaveChanges();
            }
        }
        public static void AjoutFilms()
        {
            using (var ctx = new FilmsDBContext())
            {
                Categorie cate = ctx.Categories.First(ctx => ctx.Nom == "Drame");
                List<Film> liste = new List<Film>();
                int id = ctx.Films.Max(s => s.Id);
                var film = new Film()
                {
                    Id = id + 1,
                    Nom = "Le film",
                    Description = "bah un film",
                    CategorieNavigation = cate
                };
                var film2 = new Film()
                {
                    Id = id + 2,
                    Nom = "Le film2",
                    Description = "nah un film la suite",
                    CategorieNavigation = cate
                };
                liste.Add(film);
                liste.Add(film2);
                ctx.AddRange(liste);

                ctx.SaveChanges();
            }
        }
    }
}
