using UnityEngine;
using System.Collections.Generic;

public class MerchantSystem : MonoBehaviour
{
    public List<RewardData> possibleRewards;
    public int price = 50;
    public PlayerInventory  playerInventory;
    public BoostJoueur boostJoueur;
    public PlayerController playerController;
    private bool isPlayerInside = false;
    public bool canBuy = true;
    

    public void TrybuyReward()
    {
        if (playerInventory.resources >= price)
        {
            playerInventory.resources -= price;
            RewardData reward = GetRandomReward();
            ApplyReward(reward);
            canBuy = false;
            Debug.Log("Bravo ! Tu as reçu :" + reward.itemName);
            
        }
        else
        {
            Debug.Log("Pas assez d'argent!");
        }
    }

    private void ApplyReward(RewardData reward)
    {
        switch (reward.type)
        {
            case RewardType.Invincibility:
                boostJoueur.hasInvincibilityUnlocked = true;
                break;
            
            case RewardType.Dash:
                boostJoueur.hasDashUnlocked = true;
                break;
            
            case RewardType.FireRate:
                playerController.fireRate += reward.value;
                break;
            
            case RewardType.MaxHealth:
                playerController.maxHealth += reward.value;
                break;
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInside = true;
            Debug.Log("Appuie sur F pour acheter");
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInside = false;
        }
    }
    void Update()
    {
        // On vérifie si le joueur est dans la zone ET appuie sur F ET que le marchand est prêt
        if (isPlayerInside && canBuy && Input.GetKeyDown(KeyCode.F))
        {
            TrybuyReward();
        }
    }

    private RewardData GetRandomReward()
    {
        int totalWeight = 0;
        foreach (var res in possibleRewards )
        {
            totalWeight += res.weight;
        }
        int randomNumber = Random.Range(0, totalWeight);
        int currentWeightSum = 0;

        foreach (var res in possibleRewards)
        {
            currentWeightSum += res.weight;
            if (randomNumber < currentWeightSum)
            {
                return res;
            }
        }
        return possibleRewards[0];
    }
}
