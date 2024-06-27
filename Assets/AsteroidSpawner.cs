using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public Asteroid AsteroidPrefab;

    public float SpawnRate = 2.0f;
    public float SpawnStart = 2.0f;
    public int SpawnAmount = 1;
    public float SpawnDistance = 20.0f;
    public float SizeVariance = 1;
    public float SpeedVariance = 1;

    public int Level = 1;

    public int AsteroidsPerLevel = 1;

    private List<Asteroid> asteroids;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(Spawn), SpawnStart, SpawnRate);
        asteroids = new List<Asteroid>();

        GameMaster.LevelUp += OnLevelUp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Spawn()
    {
        asteroids = asteroids.Where(x => x != null).ToList();

        for (int i = 0; i < SpawnAmount; i++)
        {
            if (asteroids.Count >= Level * AsteroidsPerLevel)
                return;

            Vector3 spawnDirection = Random.insideUnitCircle.normalized * SpawnDistance;
            Vector3 spawnPoint = transform.position + spawnDirection;

            Asteroid asteroid = Instantiate(AsteroidPrefab, spawnPoint, Random.rotation);

            asteroid.Size = Random.Range(asteroid.Size - SizeVariance, asteroid.Size + SizeVariance);
            asteroid.Speed = Random.Range(asteroid.Speed - SpeedVariance, asteroid.Speed + SpeedVariance);

            asteroid.CalculateSize();

            asteroid.Project(-spawnDirection * asteroid.Speed);
            asteroid.Source = this;

            asteroids.Add(asteroid);
        }
    }

    private void OnLevelUp(object sender, System.EventArgs e) =>
        Level++;

    public void TrackAsteroid(Asteroid roid) =>
        asteroids.Add(roid);
}
