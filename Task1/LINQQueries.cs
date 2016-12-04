using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class Class1
    {
        ShawarmaEntities shawarma = new ShawarmaEntities();

        public void AddIngradient(int weight, string name)
        {
            Ingradient ing = new Ingradient();
            ing.TotalWeight = weight;
            ing.IngradientName = name;
            shawarma.Ingradient.Add(ing);
            shawarma.SaveChanges();
        }

        public void SellShawarma(string shawarmaName)
        {
            var sha = shawarma.Shawarma.First(p => p.ShawarmaName == shawarmaName);
            if (ReferenceEquals(sha, null)) throw new ArgumentException("There is no such shawarma!");
            foreach (var recipe in sha.ShawarmaRecipe)
            {
                if(recipe.Weight>recipe.Ingradient.TotalWeight) throw new ArgumentException("There is not enough ingradient for such shawarma!");
                recipe.Ingradient.TotalWeight -= recipe.Weight;
            }
            shawarma.SaveChanges();
        }

        public void CreateRecipe(string shawarmaName, int cookingTime, string[] ingradients, int[] weights)
        {
            Shawarma sh = new Shawarma();
            sh.ShawarmaName = shawarmaName;
            sh.CookingTime = cookingTime;
            shawarma.Shawarma.Add(sh);
            for (int i = 0; i < ingradients.Length; i++)
            {
                ShawarmaRecipe recipe = new ShawarmaRecipe();
                recipe.IngradientID =shawarma.Ingradient.First(ingred=>ingred.IngradientName == ingradients[i]).IngradientID;
                recipe.Weight = weights[i];
                shawarma.ShawarmaRecipe.Add(recipe);
            }
            shawarma.SaveChanges();
        }
    }
}
