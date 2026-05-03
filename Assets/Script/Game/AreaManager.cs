using UnityEngine;

public class AreaManager : MonoBehaviour
{
  public GameObject arenaGate;
  public EnemySpawner[] spawners;
  public EnemySpawner spawn;
  public bool arenaCleared = false;

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

    if (spawn.enemydeath = true)
    {
      Debug.Log("Tout les ennemis sont mort");
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
