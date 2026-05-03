using System.Collections;
using UnityEngine;

public class BoostJoueur : MonoBehaviour
{
   [Header("Invincibility")] public bool hasInvincibilityUnlocked = false;
   public float invulnDuration = 3f;
   public float invulnCooldown = 10f;
   public bool isInvulnerable = false;
   private bool canInvuln = true;
   public EnemySpawner player;
   [Header("Dash")]
   public bool hasDashUnlocked = false;
   public MerchantSystem merchantSystem;

   public float dashForce = 10f;
   private Rigidbody2D rb;

   void Start()
   {
      rb = GetComponent<Rigidbody2D>();
      isInvulnerable = false;
   }

   private void Update()
   {
      if (hasInvincibilityUnlocked && canInvuln && Input.GetKeyDown(KeyCode.E))
      {
         StartCoroutine(ActivateInvincibility());
      }

      if (hasDashUnlocked && Input.GetKeyDown(KeyCode.LeftShift))
      {
         Debug.Log("Dash");
         rb.AddForce(transform.up * dashForce, ForceMode2D.Impulse);
      }
   }

   public void ResetBoost()
   {
      
         hasDashUnlocked = false;
         hasInvincibilityUnlocked = false;
         merchantSystem.iconInvisibility.SetActive(false);
         merchantSystem.iconFireRate.SetActive(false);
         merchantSystem.iconMaxHealth.SetActive(false);
      
         
   }

   IEnumerator ActivateInvincibility()
   {
      isInvulnerable = true;
      canInvuln = false;
      Debug.Log("Invincibility");
      yield return new WaitForSeconds(invulnDuration);
      
      isInvulnerable = false;
      Debug.Log("Fin d'invincibilité, recharge...");
      
      yield return new WaitForSeconds(invulnCooldown);
      canInvuln = true;
   }
}
