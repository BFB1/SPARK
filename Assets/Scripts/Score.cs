using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Score
{
    
    // Using a static class so that the score persists between levels.
    // This particular implementation does not require resetting between levels.

    private static readonly List<int> ScoreTable = new List<int>() {0, 0, 0};

    public static int TotalScore => Enumerable.Reverse(ScoreTable).Take(3).Sum();

    public static int CurrentLevelScore = 0;

    public static void SubmitScore()
    {
        ScoreTable.Add(CurrentLevelScore);
        CurrentLevelScore = 0;
    }

    public static List<int> GetScores()
    {
        List<int> scoreOutput = ScoreTable.ToList();
        scoreOutput.Add(TotalScore);
        scoreOutput.Reverse();
        return scoreOutput;
    }
}