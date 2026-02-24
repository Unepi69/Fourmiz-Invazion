using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class BuildingSpot : MonoBehaviour
{
   public enum BuildingType
   {
      Health1,
      Health2,
      Health3,
      Damage1,
      Damage2,
      Damage3,
      Ressource1,
      Ressource2,
      Ressource3,
      Speed1,
      Speed2,
      Speed3,
   }
   public BuildingType typeDuBatiment;
   public GameObject visualUpgrade;
   public GameObject uiPanel;
   public TextMeshProUGUI infoText;

   private int prix;
   private bool isPlayerInside =  false;
   private bool isPurchased =  false;

   void Start()
   {
      if(visualUpgrade != null) visualUpgrade.SetActive(false);
      if(uiPanel != null) uiPanel.SetActive(false);
      
      DeterminerPrix();
      InitialiserTexte();
   }

   void DeterminerPrix()
   {
      string typeStr = typeDuBatiment.ToString();
      if (typeStr.Contains("1")) prix = 2000;
      else if (typeStr.Contains("2")) prix = 3000;
      else if (typeStr.Contains("3")) prix = 4000;
   }

   void InitialiserTexte()
   {
      if (infoText != null)
      {
         infoText.text = $"{typeDuBatiment}\nCoût: {prix}";
      }
   }
   void Update()
   {
      if (isPlayerInside && !isPurchased && Input.GetKeyDown(KeyCode.E))
      {
         TenterAchat();
      }
   }

   void TenterAchat()
   {
      PlayerInventory inv =  GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();

      if (inv != null && inv.resources >= prix)
      {
         inv.AddResources(-prix); 
         AppliquerBonus();
         isPurchased = true;
         if (visualUpgrade != null) visualUpgrade.SetActive(true);
         if (uiPanel != null) uiPanel.SetActive(false);
         Debug.Log("Bâtiment construit");
      }
      else
      {
         Debug.Log("Pas assez ! Il faut :" + prix);
      }
   }

   void AppliquerBonus()
   {
      GameObject player = GameObject.FindGameObjectWithTag("Player");
      PlayerController health = player.GetComponent<PlayerController>();
      PlayerController controller = player.GetComponent<PlayerController>();

      switch (typeDuBatiment)
      {
         //case BuildingType.Health1: health.maxHealth += 20; break;
         //case BuildingType.Health2: health.maxHealth += 40; break;
         //case BuildingType.Health3: health.maxHealth += 60; break;
         
         case BuildingType.Damage1: GlobalStats.bonusDamage += 20; break;
         case BuildingType.Damage2: GlobalStats.bonusDamage += 40; break;
         case BuildingType.Damage3: GlobalStats.bonusDamage += 60; break;
         
         case BuildingType.Ressource1: GlobalStats.bonusResource += 150; break;
         case BuildingType.Ressource2: GlobalStats.bonusResource += 200; break;
         case BuildingType.Ressource3: GlobalStats.bonusResource += 300; break;
         
         case BuildingType.Speed1: controller.fireRate -= 0.25f; break;
         case BuildingType.Speed2: controller.fireRate -= 0.50f; break;
         case BuildingType.Speed3: controller.fireRate -= 0.75f; break;
         
         case BuildingType.Health1: health.UpdateMaxHealth(20); break;
         case BuildingType.Health2: health.UpdateMaxHealth(40); break;
         case BuildingType.Health3: health.UpdateMaxHealth(60); break;
      }
      
   }

   private void OnTriggerEnter2D(Collider2D other)
   {
      if(other.CompareTag("Player"))
      {
         isPlayerInside = true;
         if(uiPanel != null) uiPanel.SetActive(true);
         Debug.Log("Appuyez sur E pour acheter" + typeDuBatiment + "(Prix:"  + prix + ")");
      }
   }

   private void OnTriggerExit2D(Collider2D other)
   {
      if (other.CompareTag("Player"))
      {
         isPlayerInside = false;
         if (uiPanel != null) uiPanel.SetActive(false);
      }
   }
}
