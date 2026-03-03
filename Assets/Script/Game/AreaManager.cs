using UnityEngine;

public class AreaManager : MonoBehaviour
{
  public GameObject arenaGate;
  public EnemySpawner[] spawners;
  
  private bool arenaCleared = false;

  void Update()
  {
    if (!arenaCleared)
    {
      CheckEnemies();
    }
  }

  void CheckEnemies()
  {
    bool enemiesLeft = false;

    foreach (EnemySpawner spawner in spawners)
    {
      if (spawner.currentEnemies > 0 || !spawner.allEnemiesSpawned)
      {
        enemiesLeft = true;
        break;
      }
    }

    if (!enemiesLeft)
    {
      arenaCleared = true;
      OpenGate();
    }
  }

  void OpenGate()
  {
    if (arenaCleared != null)
    {
      arenaGate.SetActive(false);
      Debug.Log("Arena cleared");
    }
  }
}
