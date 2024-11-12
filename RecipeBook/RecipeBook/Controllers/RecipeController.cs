using RecipeBook.Models;
using RecipeBook.Providers.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AutoMapper;
using RecipeBook.DTOs.Recipes;
using RecipeBook.Data.Models;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace RecipeBook.Controllers
{
    [Route("recipes")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly ILogger<RecipeController> _logger;
        private readonly IRecipeProvider _recipeProvider;
        private readonly IMapper _mapper;


        public RecipeController
            (
            ILogger<RecipeController> logger,
            IRecipeProvider recipeProvider,
            IMapper mapper
            )
        {
            _logger = logger;
            _recipeProvider = recipeProvider;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecipeDTO>>> GetRecipesAsync()
        {
            return Ok(await _recipeProvider.GetRecipesAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RecipeDTO>> GetRecipeById(int id)
        {
            var newRecipe = await _recipeProvider.GetRecipeById(id);
            return newRecipe != null ? Ok(newRecipe) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<RecipeDTO>> CreateRecipeAsync([FromBody] RecipeDTO newRecipe)
        {
            return Ok(await _recipeProvider.CreateRecipeAsync(newRecipe));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<RecipeDTO>> UpdateRecipeByIdAsync(int id, [FromBody] RecipeDTO updatedRecipe)
        {
            try
            {
                updatedRecipe = await _recipeProvider.UpdateRecipeByIdAsync(id, updatedRecipe);
                Console.WriteLine(updatedRecipe);

                // Check if the recipe was found and updated
                if (updatedRecipe != null)
                {
                    return Ok(_mapper.Map<RecipeDTO>(updatedRecipe));
                }
                else
                {
                    return NotFound(); // Recipe with the specified id not found
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating recipe with id {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating recipe");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRecipeById(int id)
        {
            await _recipeProvider.DeleteRecipeById(id);
            return NoContent();
        }

    }
}
