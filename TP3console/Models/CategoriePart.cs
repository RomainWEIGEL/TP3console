using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace TP3console.Models.EntityFramework
{ 
    public partial class Categorie
    {

        public override string? ToString()
        {
            return Id + " " + Nom + " ";
        }
    }
}
