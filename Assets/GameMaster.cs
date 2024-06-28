using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static event System.EventHandler LevelUp;

    public delegate void EventHandler(EventArgs e);

    public AsteroidSpawner Spawner;
    public Player Player;

    public TMP_Text ScoreLabel;
    public LevelUpMenu LevelUpMenu;

    public Tuple<int, int> ScoreToLevelUp = new Tuple<int, int> (5, 8);
    public int Score = 0;
    public int Level = 1;

    private void Start()
    {
        Asteroid.AsteroidDestroyed += HandleAsteroidDestroyed;
        ItemCard.PlayerItemSelected += HandlePlayerItemSelected;
    }

    private void HandleAsteroidDestroyed(object sender, AsteroidDestroyedEventArgs e) =>
        UpdateScore(e.Score);

    private void HandlePlayerItemSelected(object sender, PlayerItemSelectedEventArgs e)
    {
        LevelUpMenu.ShowMenu(false);
        ResumeGame();
    }


    private void UpdateScore(int scoreIncrease)
    {
        Score += scoreIncrease;
        ScoreLabel.text = $"{Score} / {ScoreToLevelUp.Item2}";

        if (Score >= ScoreToLevelUp.Item2)
        {
            ProcessLevelUp();
        }
    }

    private void ProcessLevelUp()
    {
        var nextLevel = ScoreToLevelUp.Item1 + ScoreToLevelUp.Item2;

        ScoreToLevelUp = new Tuple<int, int>(ScoreToLevelUp.Item2, nextLevel);

        Level++;

        var levelUp = LevelUp;

        if (levelUp != null)
        {
            PauseGame();
            levelUp(this, new EventArgs());
            LevelUpMenu.ShowMenu(true);
        }
    }

    private void PauseGame() =>
        Time.timeScale = 0;

    private void ResumeGame() =>
        Time.timeScale = 1;
}
