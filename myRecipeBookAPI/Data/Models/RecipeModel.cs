namespace myRecipeBookAPI.Data.Models
{
    public class RecipeModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? Version { get; set; }

        public int CookTime { get; set; }

        public string Desciption { get; set; }

    }
}
