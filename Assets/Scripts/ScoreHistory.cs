using UnityEngine;
using System.Collections.Generic;

public static class ScoreHistory
{
    const string Key = "LastScores";
    const int MaxScores = 5;

    //read saved scores
    public static List<int> Load()
    {
        var scores = new List<int>();
        string saved = PlayerPrefs.GetString(Key, "");
        if (saved == "") return scores;

        foreach (string part in saved.Split(','))
            if (int.TryParse(part, out int s)) scores.Add(s);

        return scores;
    }

    public static void Add(int score)
    {
        var scores = Load();
        scores.Insert(0, score);

        if (scores.Count > MaxScores)
            scores.RemoveRange(MaxScores, scores.Count - MaxScores);

        PlayerPrefs.SetString(Key, string.Join(",", scores));
        PlayerPrefs.Save();
    }
}


