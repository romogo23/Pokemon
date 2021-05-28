using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pokemon.Models.ViewModels
{
    public class PokemonTrainer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Region { get; set; }
        public string Email { get; set; }
    }
}