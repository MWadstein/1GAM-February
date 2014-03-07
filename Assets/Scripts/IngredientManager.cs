using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class IngredientManager : MonoBehaviour
{
    public static IngredientManager Instance;

	public UILabel ResultLabel;

	public List<Recipe> RecipesList;
    public Recipe currentRecipe;

	private string ingredient1 = "";
	private string ingredient2 = "";

    private Color coolTopColor = new Color(51f/255, 180f/255, 0f/255);
    private Color coolBottomColor = new Color(0f/255, 86f/255, 255f/255);
    private Color mediumTopColor = new Color(0f/255, 86f/255, 255f/255);
    private Color mediumBottomColor = new Color(255f/255, 53f/255, 0f/255);
    private Color hotTopColor = new Color(255f/255, 153f/255, 0f/255);
    private Color hotBottomColor = new Color(255f/255, 0f/255, 0f/255);

	private void Start()
	{
	    Instance = this;

        // Populate our Ingredient List
		PopulateRecipesList();
        // Let our Game Manager know we have a fully populated list
	    GameManager.Instance.CreateWinningList();
	}

	private void PopulateRecipesList()
	{
        RecipesList = new List<Recipe>();
        RecipesList.Add(new Recipe { Name = "Frescini", Ingredients = new List<string> { "Peperoncini", "Fresno Chili" } });
        RecipesList.Add(new Recipe { Name = "Chipocini", Ingredients = new List<string> { "Peperoncini", "Chipotle" } });
        RecipesList.Add(new Recipe { Name = "Cumaricini", Ingredients = new List<string> { "Peperoncini", "Cumari Pepper" } });
        RecipesList.Add(new Recipe { Name = "Habacini", Ingredients = new List<string> { "Peperoncini", "Habanero Chili" } });
        RecipesList.Add(new Recipe { Name = "Infinicini", Ingredients = new List<string> { "Peperoncini", "Infinity Chili" } });
        RecipesList.Add(new Recipe { Name = "Frespotle", Ingredients = new List<string> { "Fresno Chili", "Chipotle" } });
        RecipesList.Add(new Recipe { Name = "Fresmari", Ingredients = new List<string> { "Fresno Chili", "Cumari Pepper" } });
        RecipesList.Add(new Recipe { Name = "Fresnero", Ingredients = new List<string> { "Fresno Chili", "Habanero Chili" } });
        RecipesList.Add(new Recipe { Name = "Fresfinity", Ingredients = new List<string> { "Fresno Chili", "Infinity Chili" } });
        RecipesList.Add(new Recipe { Name = "Cumapotle", Ingredients = new List<string> { "Chipotle", "Cumari Pepper" } });
        RecipesList.Add(new Recipe { Name = "Chiponero", Ingredients = new List<string> { "Chipotle", "Habanero Chili" } });
        RecipesList.Add(new Recipe { Name = "Chipofinity", Ingredients = new List<string> { "Chipotle", "Infinity Chili" } });
        RecipesList.Add(new Recipe { Name = "Cumarinero", Ingredients = new List<string> { "Cumari Pepper", "Habanero Chili" } });
        RecipesList.Add(new Recipe { Name = "Cumanity", Ingredients = new List<string> { "Cumari Pepper", "Infinity Chili" } });
        RecipesList.Add(new Recipe { Name = "Habafinity", Ingredients = new List<string> { "Habanero Chili", "Infinity Chili" } });
	}

	public void UpdateResultDisplay(int num, string ingredient)
	{
		if (num == 1)
			ingredient1 = ingredient;
		else
			ingredient2 = ingredient;

		if(string.IsNullOrEmpty(ingredient1) || string.IsNullOrEmpty(ingredient2))
			return;

        foreach (var recipe in RecipesList)
		{
		    if (ingredient1.Equals(ingredient2))
		    {
		        ResultLabel.text = "Bad Recipe";
                return;
		    }

		    if (recipe.Ingredients.Contains(ingredient1) && recipe.Ingredients.Contains(ingredient2))
			{
                // Add 1 to our Number Of Combinations var
			    GameManager.Instance.NumberOfCombinations++;
                // Cache the found Recipe
				currentRecipe = recipe;
                // Scale down the Result Label to 0
                ResultLabel.transform.scaleTo(0.4f, new Vector3(0, 0, 0)).setOnCompleteHandler(complete =>
                {
                    // Update the Result Label with the Name
                    ResultLabel.text = currentRecipe.Name;
                    // Update the Result Label with the new Color
                    var recipeHeat = GameManager.Instance.WinningRecipeList.IndexOf(currentRecipe) + 1;
                    if (recipeHeat <= GameManager.Instance.HeatLevel) // Cool item
                    {
                        ResultLabel.gradientTop = coolTopColor;
                        ResultLabel.gradientBottom = coolBottomColor;
                    }
                    else if (recipeHeat >= GameManager.Instance.HeatLevel && recipeHeat <= GameManager.Instance.HeatLevel + 2) // Medium item
                    {
                        ResultLabel.gradientTop = mediumTopColor;
                        ResultLabel.gradientBottom = mediumBottomColor;
                    }
                    else // Hot item
                    {
                        ResultLabel.gradientTop = hotTopColor;
                        ResultLabel.gradientBottom = hotBottomColor;
                    }
                        
                    // Scale in the new Result Label to 1
                    ResultLabel.transform.scaleTo(0.4f, new Vector3(1, 1, 1));
                });   
				return;
			}
		}
	}
}

public class Recipe
{
	public string Name;
    public List<string> Ingredients;
}