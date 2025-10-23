using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeBook.Utilities
{
    public static class GeneralMethods
    {
        public static bool OverHalfSimilarity(string current, string updated)
        {
            string[] currentArray = current.Split(' ');
            string[] updatedArray = updated.Split(' ');
            int count = 0;

            foreach (string item in updatedArray)
            {
                if (current.Contains(item))
                {
                    count++;
                }
            }

            if (count < (currentArray.Length / 2))
            {
                return false;
            }
            return true;
        }
    }
}
