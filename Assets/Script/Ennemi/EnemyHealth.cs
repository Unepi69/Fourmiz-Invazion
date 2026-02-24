using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    [Header("Stats")]
    public float maxHealth = 100f;
    private float currentHealth;

    [Header("UI & Feedbacks")]
    public Slider healthSlider;
    public float smoothSpeed = 5f; 
    private SpriteRenderer sr;
    private Color originalColor;

    [Header("Loot & Mort")]
    public GameObject resourcePrefab;
    public GameObject deathEffect;    
    public GameObject healthPackPrefab;
    public float dropChance = 20f;

    void Start()
    {
        currentHealth = maxHealth;
        sr = GetComponent<SpriteRenderer>();
        if (sr != null) originalColor = sr.color;

        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = maxHealth;
        }
    }

    void Update()
    {
        // Effet de barre de vie réactive (le slider rattrape doucement la valeur)
        if (healthSlider != null && healthSlider.value != currentHealth)
        {
            healthSlider.value = Mathf.Lerp(healthSlider.value, currentHealth, Time.deltaTime * smoothSpeed);
        }
    }

    // CETTE FONCTION EST CELLE APPELÉE PAR LA BALLE
    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        // Déclenche le flash blanc
        StopAllCoroutines();
        StartCoroutine(FlashRoutine());

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    IEnumerator FlashRoutine()
    {
        if (sr != null)
        {
            sr.color = Color.white;
            yield return new WaitForSeconds(0.1f);
            sr.color = originalColor;
        }
    }

    void Die()
    {
        // 1. Explosion de particules
        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }

        // 2. Spawn de la ressource volante
        if (resourcePrefab != null)
        {
            Instantiate(resourcePrefab, transform.position, Quaternion.identity);
        }

        // 3. Chance de drop un pack de soin
        float random = Random.Range(0f, 100f);
        if (random <= dropChance && healthPackPrefab != null)
        {
            Instantiate(healthPackPrefab, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}