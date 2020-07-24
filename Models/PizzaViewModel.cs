using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TP4_Pizza_V2.BO;

namespace TP4_Pizza_V2.Models
{
    public class PizzaViewModel
    {
        //Création d'une représentation d'un objet complet

        public Pizza Pizza { get; set; }
        public List<SelectListItem> Ingredients { get; set; } = new List<SelectListItem>();

        public List<SelectListItem> Pates { get; set; } = new List<SelectListItem>();

        //Oblige le formulaire à contenir une valeur. Sécurité pour s'immuniser d'une modificat° du script coté client
        [Required]

        //Atribut initialisé à null pour pouvoir charger une nouvelle valeur à la création de l'objet
        public int? IdPate { get; set; }
        public List<int> IdsIngredients { get; set; } = new List<int>();
    }
}