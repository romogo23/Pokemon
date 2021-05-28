using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Pokemon.Models;
using Pokemon.Models.ViewModels;

namespace Pokemon.Controllers
{
    public class CatchedPokemonController : Controller
    {
        public static List<PokemonCatched> ListOfPokemon()
        {
            List<PokemonCatched> list = new List<PokemonCatched>();
            using (PokemonEntities db = new PokemonEntities())
            {
                list = (from p in db.Catched_Pokemon
                        select new PokemonCatched
                        {
                            Number = p.Number,
                            Identifier = p.Identifier,
                            Name = p.Name,
                            PrimaryType = p.PrimaryType,
                            SecundaryType = p.SecundaryType,
                            Image_Url = p.Image_Url
                        }).ToList();
            }
            return list;
        }
        // GET: CatchedPokemon
        public ActionResult Index()
        {
            return View(ListOfPokemon());
        }

        public async Task<ActionResult> Catch()
        {
            Random r = new Random();

            var httpClient = new HttpClient();
            var json = await httpClient.GetStringAsync("https://pokeapi.co/api/v2/pokemon/"+ r.Next(1, 898) +"/");

            //var pokemon = JsonConvert.DeserializeObject<ListCatchedPokemon>(json);

            var pokemonIn = new PokemonCatched();

            dynamic miarray = JsonConvert.DeserializeObject(json);

            pokemonIn.Identifier = miarray["id"];
            pokemonIn.Api_Version = 2;
            pokemonIn.Name = miarray["name"];
            foreach(var item in miarray["types"]) //PENSAR EN COMO NO CAERLE ENCIMA
            {
                if (item.slot != 1)
                {
                    pokemonIn.SecundaryType = item.type.name;
                    break;
                }
                pokemonIn.PrimaryType = item.type.name;
            }
            pokemonIn.Image_Url = miarray["sprites"].front_default;


            //JsonTextReader reader = new JsonTextReader(new StringReader(json));
            //while (reader.Read())
            //{
            //    if (reader.Value != null)
            //    {
            //        Console.WriteLine("Token: {0}, Value: {1}", reader.TokenType, reader.Value);
            //    }
            //    else
            //    {
            //        Console.WriteLine("Token: {0}", reader.TokenType);
            //    }
            //}

            using (PokemonEntities p = new PokemonEntities())
            {
                var newPoke = new Catched_Pokemon();
                newPoke.Identifier = pokemonIn.Identifier;
                newPoke.Api_Version = pokemonIn.Api_Version;
                newPoke.Name = pokemonIn.Name;
                newPoke.PrimaryType = pokemonIn.PrimaryType;
                newPoke.SecundaryType = pokemonIn.SecundaryType;
                newPoke.Image_Url = pokemonIn.Image_Url;
                newPoke.Owner_Id = pokemonIn.Owner_Id;

                p.Catched_Pokemon.Add(newPoke);
                p.SaveChanges();
            }

            return Redirect("~/CatchedPokemon/Index");
        }
    }
}