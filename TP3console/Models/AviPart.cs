using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TP3console.Models.EntityFramework
{
    public partial class Avi
    {
        public override string? ToString()
        {
            return Film + " " + Utilisateur + " ";
        }
    }
}
