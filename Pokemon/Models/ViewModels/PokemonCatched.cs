using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pokemon.Models.ViewModels
{
    public class PokemonCatched
    {
        public int Number { get; set; }
        public int Identifier { get; set; }
        public int Api_Version { get; set; }
        public string Name { get; set; }
        public string PrimaryType { get; set; }
        public string SecundaryType { get; set; }
        public string Image_Url { get; set; }
        public int Owner_Id { get; set; }
    }
}