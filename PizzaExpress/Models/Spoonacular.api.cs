using PizzaExpress.Controllers;

namespace PizzaExpress.Models
{
    public class SpoonacularAPI
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public SpoonacularAPI(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;

            _apiKey = configuration["Spoonacular:ApiKey"];
        }

        public async Task<List<string>> GetIngredientiDaPizzaAsync(string nomePizza)
        {
            var searchURL =
                $"https://api.spoonacular.com/recipes/complexSearch" +
                $"?query=pizza {nomePizza}" +
                $"&language=it" +
                $"&number=1" +
                $"&apikey={_apiKey}";

            var searchResponse =
               await _httpClient.GetFromJsonAsync<SearchResponse>(searchURL);

            if (searchResponse == null || searchResponse.Results.Count == 0)
                return new List<string>();

            int recipeId = searchResponse.Results[0].Id;

            var ingredientiUrl =
                $"https://api.spoonacular.com/recipes/{recipeId}/ingredientWidget.json" +
                $"?language=it" +
                $"&apiKey={_apiKey}";

            var ingredientiResponse =
                await _httpClient.GetFromJsonAsync<IngredientiResponse>(ingredientiUrl);

            if (ingredientiResponse == null)
                return new List<string>();

            return ingredientiResponse.Ingredients
                .Select(i => i.Name)
                .ToList();
        }
        private class SearchResponse
        {
            public List<SearchResult> Results { get; set; } = new();
        }
        private class SearchResult
        {
            public int Id { get; set; }
        }
        private class IngredientiResponse
        {
            public List<Ingrediente> Ingredients { get; set; } = new();
        }
        private class Ingrediente
        {
            public string Name { get; set; } = "";
        }
    }
}
