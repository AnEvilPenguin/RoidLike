using System.Collections;
using System.Collections.Generic;
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Spawn()
    {
        for (int i = 0; i < SpawnAmount; i++)
        {
            if (asteroids.Count >= Level * AsteroidsPerLevel)
                return;

            Vector3 spawnDirection = Random.insideUnitCircle.normalized * SpawnDistance;
            Vector3 spawnPoint = transform.position + spawnDirection;

            Asteroid asteroid = Instantiate(AsteroidPrefab, spawnPoint, Random.rotation);

            asteroid.Size = Random.Range(asteroid.Size - SizeVariance, asteroid.Size + SizeVariance);
            asteroid.Speed = Random.Range(asteroid.Speed - SpeedVariance, asteroid.Speed + SpeedVariance);

            asteroid.Project(-spawnDirection);

            asteroids.Add(asteroid);
        }
    }
}