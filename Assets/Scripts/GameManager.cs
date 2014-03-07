using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections;
using Random = System.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public UIPopupList Ingredient1PopupList;
    public UIPopupList Ingredient2PopupList;
    public UILabel HeatLabel;

    public GameObject GameOverPanelGO;
    public GameObject InstructionsPanelGO;

    public int HeatLevel = 0;
    public int NumberOfCombinations = 0;
    public bool DidWin = false;
    
    public List<Recipe> WinningRecipeList = new List<Recipe>();

    private int lives = 4;

    private void Start()
    {
        // set Instance to our copy of this Manager Class
        Instance = this;
        // Show the Instructions Panel on first load
        InstructionsPanelGO.SetActive(true);
    }

    public void CreateWinningList()
    {
        // Get the reciple list from the Ingredient Manager
        WinningRecipeList = IngredientManager.Instance.RecipesList;
        // Shuffle our List
        WinningRecipeList.Shuffle();
        // Set our display to the current lowest item
        // Update our display with our first recipe
        Ingredient1PopupList.value = WinningRecipeList[0].Ingredients[0];
        Ingredient2PopupList.value = WinningRecipeList[0].Ingredients[1];
    }

    public void EatItem()
    {
        // Get the heat level of the item we are making
        var recipeHeat = WinningRecipeList.IndexOf(IngredientManager.Instance.currentRecipe) + 1;
        // Compare it to our current heat level
        if (recipeHeat <= HeatLevel) // Not hot at all, do nothing
        {
            return;
        }
        else if (recipeHeat - 1 == HeatLevel) // Just the right heat, level up the heat level
        {
            SoundManager.Instance.PlaySoundClip(SoundManager.Instance.SuccessClip);
            HeatLevel++;
            HeatLabel.text = "Heat:\n" + HeatLevel;
            // and update the color of the Result Text label
            Ingredient1PopupList.value = Ingredient1PopupList.value;
            if (HeatLevel == WinningRecipeList.Count)
            {
                SoundManager.Instance.PlaySoundClip(SoundManager.Instance.WinClip);
                DidWin = true;
                GameOverPanelGO.SetActive(true);
                
            }
        }
        else // Too hot, lose a life
        {          
            GameObject.Find("Life Bar " + lives + " Sprite").GetComponent<UISprite>().spriteName = "Empty Glass";
            lives--;
            if (lives >= 0)
            {
                SoundManager.Instance.PlaySoundClip(SoundManager.Instance.FailedClip);
            }
        }

        if (lives <= 0)
        {
            SoundManager.Instance.PlaySoundClip(SoundManager.Instance.LoseClip);
            DidWin = false;
            GameOverPanelGO.SetActive(true);
        }
    }

    public void RestartGame()
    {
        HeatLevel = 0;
        HeatLabel.text = "Heat:\n0";
        NumberOfCombinations = 0;
        lives = 4;
        DidWin = false;
        for (int i = 1; i <= 4; i++)
        {
            GameObject.Find("Life Bar " + i + " Sprite").GetComponent<UISprite>().spriteName = "Full Glass";
        }
        CreateWinningList();
        GameOverPanelGO.SetActive(false);
    }
}

static class MyExtensions
{
    public static void Shuffle<T>(this IList<T> list)
    {
        Random rng = new Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
