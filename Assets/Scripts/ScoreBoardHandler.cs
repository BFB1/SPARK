using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreBoardHandler : MonoBehaviour
{
    
    // I wanted the outputted score to be spelled out for style reasons. Since I have a fixed maximum score
    // I can get away with making a simple map. Not the best solution but it works just fine.
    
    private const string TemplateText = "TOTAL SCORE=>{0}\nLEVEL THREE=>{1}\nLEVEL TWO=>{2}\nLEVEL ONE=>{3}";
    private readonly string[] numberToWordMap = { "ZERO", "ONE", "TWO", "THREE", "FOUR", "FIVE", "SIX", "SEVEN",
        "EIGHT", "NINE", "TEN", "ELEVEN", "TWELVE", "THIRTEEN", "FOURTEEN",
        "FIFTEEN", "SIXTEEN", "SEVENTEEN", "EIGHTEEN", "NINETEEN" };

    [SerializeField]
    private TextMeshProUGUI scoreBoard;
    
    private void Awake()
    {
        object[] scoreArray = new object[4];
        List<int> scoreList = Score.GetScores();
        for (int i = 0; i < 4; i++)
        {
            scoreArray[i] = numberToWordMap[scoreList[i]];
        }
        scoreBoard.text = string.Format(TemplateText, scoreArray);
    }


    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}