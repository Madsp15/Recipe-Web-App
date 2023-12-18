using infrastructure;
using infrastructure.Repositories;

namespace service;

public class RecipeService
{
    private readonly RecipeRepository _repository;
    private readonly TagsRepository _tagsRepository;
    private readonly IngredientRepository _ingredientRepository;
    private readonly ReviewRepository _reviewRepository;
    private readonly UserRepository _userRepository;
    

    public RecipeService(RecipeRepository repository, TagsRepository tagsRepository, IngredientRepository ingredientRepository, ReviewRepository reviewRepository, UserRepository userRepository)
    {
        _repository = repository;
        _tagsRepository = tagsRepository;
        _ingredientRepository = ingredientRepository;
        _reviewRepository = reviewRepository;
        _userRepository = userRepository;
    }
   
    
    public Recipe CreateRecipe(Recipe recipe)
    {
        return _repository.CreateRecipe(recipe);
    }
    
    public bool DeleteRecipe(int id)
    {
        
        _ingredientRepository.DeleteIngredientsFromRecipe(id);
        _reviewRepository.DeleteReviewsByRecipeId(id);
        _tagsRepository.DeleteTagsFromRecipe(id);
        _userRepository.DeleteSavedRecipeFromUsers(id);
        return _repository.DeleteRecipe(id);
    }
    
    public IEnumerable<Recipe> GetAllRecipes()
    {
        return _repository.GetAllRecipes();
    }
    
    public IEnumerable<Recipe> GetRandomRecipes()
    {
        return _repository.GetRandomRecipes();
    }
    
    public Recipe UpdateRecipe(Recipe recipe)
    {
        return _repository.UpdateRecipe(recipe);
    }
    
    public IEnumerable<Recipe> GetRecipeByUserId(int id)
    {
        return _repository.GetRecipeByUserId(id);
    }
    public Recipe GetRecipeById(int id)
    {
        return _repository.GetRecipeById(id);
    }
}