using System;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
    public float healAmount = 20f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();

            if (player != null)
            {
                player.Heal(healAmount);
                
                Destroy(gameObject);
                Debug.Log("Healed " + healAmount);
            }
        }
    }
}
