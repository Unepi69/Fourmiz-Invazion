using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public PlayerInventory inventory; 
    public InventoryFeedback feedback; 
    
    public void TryBuild(GameObject buildingPrefab, Vector3 position, int cost)
    {
        if (inventory.resources >= cost)
        {
            // IMPORTANT : On passe par AddResources pour que le HUD s'actualise !
            inventory.AddResources(-cost); 

            Instantiate(buildingPrefab, position, Quaternion.identity);
            
            // Son de succès
            // Instantiate(buildEffect, position, ...);
        }
        else
        {
            // Si pas assez d'argent, on fait flasher le texte en rouge
            if (feedback != null) feedback.TriggerError();
        }
    }
}
