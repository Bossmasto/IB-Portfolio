using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScoreSystem
{
    public static event Action<int> OnScoreChange;

    public static int Score { get; private set; }
    static int _highScore;

    public static void Add(int points)
    {
        Score += points;
        OnScoreChange?.Invoke(Score);

        if(Score > _highScore)
        {
            _highScore = Score;
            PlayerPrefs.SetInt("Highscore", _highScore);
        }
    }
}
