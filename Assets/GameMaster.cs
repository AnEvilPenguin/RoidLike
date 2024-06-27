using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public AsteroidSpawner Spawner;
    public Player Player;

    public TMP_Text ScoreLabel;

    public int Score;

    private void Start()
    {
        Asteroid.AsteroidDestroyed += HandleAsteroidDestroyed;
    }

    private void HandleAsteroidDestroyed(object sender, AsteroidDestroyedEventArgs e) =>
        UpdateScore(e.Score);

    private void UpdateScore(int scoreIncrease)
    {
        Score += scoreIncrease;
        ScoreLabel.text = Score.ToString();
    }
}
