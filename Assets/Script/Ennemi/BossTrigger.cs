using UnityEngine;
using System.Collections;
using TMPro; 

public class BossTrigger : MonoBehaviour
{
    public BossAI bossScript;
    public Transform respawnPoint;
    public GameObject victoryScreen; 
    private bool victoryTriggered = false;
    

    private void OnTriggerEnter2D(Collider2D other)
    {
       Debug.Log(other.name);
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("je suis la fdp");
            bossScript.ActivateBoss();
        }
    }

    void Update()
    {
       
        if (bossScript != null && !bossScript.gameObject.activeSelf && !victoryTriggered)
        {
            victoryTriggered = true;
            StartCoroutine(VictoryRoutine());
        }
    }

    IEnumerator VictoryRoutine()
    {
        // 1. Afficher l'écran de victoire
        if (victoryScreen != null) victoryScreen.SetActive(true);
        Debug.Log("Boss vaincu !");

        yield return new WaitForSeconds(3f); // Temps pour lire "Victoire"

        // 2. Téléporter le joueur
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null && respawnPoint != null)
        {
            player.transform.position = respawnPoint.position;
        }

        // 3. Faire respawn le boss pour la prochaine fois
        //if(bossScript.ResetBoss();

        // 4. Cacher l'écran de victoire et réinitialiser le trigger
        if (victoryScreen != null) victoryScreen.SetActive(false);
        victoryTriggered = false; 
    }
}