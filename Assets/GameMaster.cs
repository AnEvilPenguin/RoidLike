using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public AsteroidSpawner Spawner;
    public Player Player;

    public int Score;

    private void Start()
    {
        Asteroid.AsteroidDestroyed += HandleAsteroidDestroyed;
    }

    private void HandleAsteroidDestroyed(object sender, AsteroidDestroyedEventArgs e)
    {
        Score += e.Score;
    }
}
