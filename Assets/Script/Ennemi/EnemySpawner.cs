using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
   public GameObject enemyPrefab;
   public Transform[] spawnPoints;
   public int maxEnemies = 5;
   public float spawnRate = 2f;

   // --- AJOUTS POUR ARENA MANAGER ---
   public int totalToSpawn = 10; // Nombre total d'ennemis pour cette zone
   [HideInInspector] public int currentEnemies; // Ennemis vivants
   [HideInInspector] public bool allEnemiesSpawned = false;
   private int spawnedCount = 0;
   // ---------------------------------

   public static bool playerReturnedToSpawn = true;
   private float nextSpawnTime;

   private void Update()
   {
      // On compte les ennemis vivants sur la map
      currentEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;

      // Si on n'a pas encore tout spawn et qu'il y a de la place
      if (playerReturnedToSpawn && currentEnemies < maxEnemies && spawnedCount < totalToSpawn)
      {
         if (Time.time >= nextSpawnTime)
         {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnRate;
         }
      }
        
      // Si on a atteint le quota, on prévient l'arène
      if (spawnedCount >= totalToSpawn)
      {
         allEnemiesSpawned = true;
      }
   }

   void SpawnEnemy()
   {
      int randomIndex = Random.Range(0, spawnPoints.Length);
      Instantiate(enemyPrefab, spawnPoints[randomIndex].position, Quaternion.identity);
      spawnedCount++;
   }
}
