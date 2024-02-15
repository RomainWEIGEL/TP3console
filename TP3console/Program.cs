// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using TP3console.Models.EntityFramework;

using (var ctx = new FilmsDBContext())
{
    ctx.ChangeTracker.QueryTrackingBehavior = Microsoft.EntityFrameworkCore.QueryTrackingBehavior.NoTracking;
    Film titanic = ctx.Films.AsNoTracking().First(f => f.Nom.Contains("Titanic"));
    titanic.Description = "Un bateau échoué. Date : " + DateTime.Now;
    int nbchanges = ctx.SaveChanges();

    Console.WriteLine("Nombre d'enregistrement modifiés ou ajoutés :  "+nbchanges);

}
Console.ReadKey();
