using UnityEngine;
using TMPro; // N'oublie pas d'importer TextMeshPro

public class PlayerInventory : MonoBehaviour
{
    [Header("Economie")]
    public int resources = 0;
    
    [Header("Interface")]
    public TextMeshProUGUI moneyText; // Ton texte "Ressources : 0"
    public GameObject popupPrefab;   // Ton prefab de texte flottant

    void Start()
    {
        // On initialise l'affichage au début du jeu
        UpdateUI();
    }

    // Cette fonction est appelée par ResourceItem ou lors d'un achat
    public void AddResources(int amount)
    {
        resources += amount;

        // On met à jour le texte du HUD immédiatement
        UpdateUI();

  
    }

    public void UpdateUI()
    {
        if (moneyText != null)
        {
            moneyText.text = "Ressources: " + resources;
        }
    }

    // Fonction utilitaire pour vérifier si on peut acheter
    public bool CanAfford(int cost)
    {
        return resources >= cost;
    }
}