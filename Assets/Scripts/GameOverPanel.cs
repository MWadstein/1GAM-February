using UnityEngine;
using System.Collections;

public class GameOverPanel : MonoBehaviour
{
    public UILabel GameOverHeaderLabel;
    public UILabel GameOverMaxHeatLabel;
    public UILabel GameOverCombinationsLabel;

    void OnEnable()
    {
        // Check for Win/Lose and update Header Label
        if (GameManager.Instance.DidWin)
        {
            GameOverHeaderLabel.text = "You Win";
        }
        else
        {
            GameOverHeaderLabel.text = "You Lose";
        }

        // Update the Max Heat Label
        GameOverMaxHeatLabel.text = "Heat: " + GameManager.Instance.HeatLevel.ToString();

        // Update the Combinations Label
        GameOverCombinationsLabel.text = "Combinations: " + GameManager.Instance.NumberOfCombinations.ToString();
    }

    public void PlayAgainButtonClick()
    {
        GameManager.Instance.RestartGame();
    }
}
