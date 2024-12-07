using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] asteroidPrefabs;
    [SerializeField] private float secondsBetweenAsteroids = 1.5f;
    [SerializeField] private Vector2 forceRange;

    private Camera mainCamera;
    private float timer;

    private void Start()
    {
        mainCamera = Camera.main;
        timer = secondsBetweenAsteroids;

    }
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            SpawnAsteroid();
            timer += secondsBetweenAsteroids;
        }
    }

    private void SpawnAsteroid()
    {
        int side = Random.Range(0, 4);
        Vector2 spawnPoint = Vector2.zero;
        Vector2 direction = Vector2.zero;
        // Get random direction and spawn point
        switch (side)
        {
            case 0:
                //Left
                spawnPoint.x = 0f;
                spawnPoint.y = Random.value;
                direction = new Vector2(1f, Random.Range(-1f, 1f));
                break;
            case 1:
                //Right
                spawnPoint.x = 1f;
                spawnPoint.y = Random.value;
                direction = new Vector2(-1f, Random.Range(-1f, 1f));
                break;
            case 2:
                //Bottom
                spawnPoint.x = Random.value;
                spawnPoint.y = 0f;
                direction = new Vector2(Random.Range(-1f, 1f), 1f);
                break;
            case 3:
                //Top
                spawnPoint.x = Random.value;
                spawnPoint.y = 1f;
                direction = new Vector2(Random.Range(-1f, 1f), -1f);
                break;
        }
        //transform it into a point in the world
        Vector3 worldSpawnPoint = mainCamera.ViewportToWorldPoint(spawnPoint);
        worldSpawnPoint.z = 0f;
        GameObject asteroidInstance = Instantiate(
            asteroidPrefabs[Random.Range(0, asteroidPrefabs.Length)], 
            worldSpawnPoint, 
            Quaternion.Euler(0f, 0f, Random.Range(0, 360f))
        );
        // give it velocity
        if (asteroidInstance.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            rb.velocity = direction.normalized * Random.Range(forceRange.x, forceRange.y);
        }
    }
}
