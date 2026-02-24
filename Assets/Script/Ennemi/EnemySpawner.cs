using System;
using UnityEngine;
using Random = System.Random;

public class EnemySpawner : MonoBehaviour
{
   public GameObject enemyPrefab;
   public Transform[] spawnPoints;
   public int maxEnemies = 5;
   public float spawnRate = 2f;

   public static bool playerReturnedToSpawn = true;
   private float nextSpawnTime;

   private void Update()
   {
      int currentEnemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

      if (playerReturnedToSpawn && currentEnemyCount < maxEnemies)
      {
         if (Time.time >= nextSpawnTime)
         {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnRate;
         }
      }
   }

   void SpawnEnemy()
   {
      int randomIndex = UnityEngine.Random.Range(0, spawnPoints.Length);
      Instantiate(enemyPrefab,  spawnPoints[randomIndex].position, Quaternion.identity);
   }
}
