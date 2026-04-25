using Unity.VisualScripting;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Weapon weapon;

    public string enemyTag = "Enemy";
    public float range = 10f;
    public float fireRate = 0.5f;
    private float nextFireTime;
    
    private Vector2 moveDirection;
    private Transform target;
    
    public float maxHealth = 100f;
    private float currentHealth;
    public Transform respawnPoint;
    
    public Slider healthSlider;
    public TextMeshProUGUI healthText;
    

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }
    
    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2(moveX, moveY).normalized;

        FindClosestTarget();

        if (target != null && Vector2.Distance(transform.position, target.position) <= range)
        {
            if (Time.time >= nextFireTime)
            {
                weapon.Fire();
                nextFireTime = Time.time + fireRate;
            }
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = moveDirection * moveSpeed;

        if (target != null)
        {
            Vector2 aimDirection = (Vector2)target.position - rb.position;
            float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
            rb.rotation = aimAngle;
        }
    }

    void FindClosestTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float closestDistance = Mathf.Infinity;
        target = null;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance && distance <= range)
            {
                closestDistance = distance;
                target = enemy.transform;
            }
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        Debug.Log("Vie du joueur : " + currentHealth);
        UpdateHealthUI();
        
        if (currentHealth <= 0f)
        {
            Debug.Log("Game Over !");
            Respawn();
        }
    }
    public void UpdateMaxHealth(float bonus)
    {
        maxHealth += bonus;
        currentHealth += bonus; 
        UpdateHealthUI();
    }
    
    public void UpdateHealthUI()
    {
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }

        if (healthText != null)
        {
            healthText.text = $"{Mathf.RoundToInt(currentHealth)} / {maxHealth}";
        }
    }
    void Respawn()
    {
        transform.position = respawnPoint.position;
        currentHealth = maxHealth;
        UpdateHealthUI();
        Debug.Log("Respawn effectué");

        EnemySpawner.playerReturnedToSpawn = true;
    }

    public void Heal(float amount)
    {
        currentHealth += amount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        
        Debug.Log("Vie actuelle " + currentHealth);
    }
}
