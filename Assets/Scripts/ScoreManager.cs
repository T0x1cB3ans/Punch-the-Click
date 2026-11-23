using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class ScoreManager : MonoBehaviour
{
    [Header("Timing windows (ms)")]
    public float great = 40f;
    public float good = 75f;
    public float okay = 100f;

    [Header("Points")]
    public int greatPoints = 10;
    public int goodPoints = 5;
    public int okayPoints = 2;
    public int missPenalty = 5;

    [Header("UI")]
    public TMP_Text scoreText;
    public TMP_Text ratingText;

    [Header("Song End")]
    public AudioSource music;
    public TMP_Text historyText;

    bool songStarted;
    bool songFinished;

    int score; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() => UpdateScoreText();

    void Update()
    {
        if (songFinished) return;
        if (PauseMenu.IsPaused) return;
        
        if (music.isPlaying) songStarted = true;
        else if (songStarted) FinishSong();
    }

    void FinishSong()
    {
        songFinished = true;
        ScoreHistory.Add(score);
        ShowHistory();
    }

    void ShowHistory()
    {
        if (historyText == null) return;

        List<int> scores = ScoreHistory.Load();
        string text = "Last Scores\n";
        for (int i =0; i < scores.Count; i++)
            text += (i +1) + ". " + scores[i] + "\n";
        historyText.text = text;
    }

    //How close???
    public bool TryHit(float offMs)
    {
        float off = Mathf.Abs(offMs);
        if (off <= great) { Add(greatPoints, "Great!"); return true; }
        if (off <= good) { Add(goodPoints, "Good"); return true; }
        if (off <= okay) { Add(okayPoints, "Okay"); return true; }
        return false;
    }

    public void Miss() => Add(-missPenalty, "Miss");

        void Add(int points, string rating)
    {
        score += points;
        UpdateScoreText();
        if (ratingText != null) ratingText.text = rating;
    }

    void UpdateScoreText()
    {
        if (scoreText != null) scoreText.text = "Score: " + score;
    }
}

