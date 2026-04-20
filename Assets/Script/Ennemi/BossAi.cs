using UnityEngine;

public class BossAI : MonoBehaviour
{
    [Header("Activation")]
    public bool isActivated = false;
    public GameObject bossUI;

    [Header("Stats de Base")]
    public float speed = 2f;
    public Transform player;
    private EnemyHealth health;
    private SpriteRenderer sr;

    [Header("Phase 2")]
    public float speedPhase2 = 4.5f;   
    public Color colorPhase2 = Color.red;
    private bool isPhase2 = false;
    
    private Vector3 startPosition;
    private Color originalColor;

    public float detectionRange = 5f; // Ta distance de détection
    public Color debugColor = new Color(1f, 0f, 0f, 0.2f);
    void Start()
    {
        startPosition = transform.position;
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;

        if(bossUI != null) bossUI.SetActive(false);
    }
    public void ResetBoss()
    {
        isActivated = false;
        isPhase2 = false;
        transform.position = startPosition;

        if (sr != null) sr.color = originalColor;

        health.currentHealth = health.maxHealth; 
    
        if (health.healthSlider != null) 
        {
            health.healthSlider.maxValue = health.maxHealth;
            health.healthSlider.value = health.maxHealth;
        }

        if (bossUI != null) bossUI.SetActive(false);
        gameObject.SetActive(true);
    }

    void Update()
    {
        if (!isActivated || player == null || health == null) return;

        // VERIFICATION PHASE 2 (50% de vie)
        if (!isPhase2 && health.GetCurrentHealth() <= (health.maxHealth / 2))
        {
            TriggerPhase2();
        }

      
        float currentSpeed = isPhase2 ? speedPhase2 : speed;
        float distance = Vector3.Distance(transform.position, player.position);
        
        if (distance > 1.2f && player != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, currentSpeed * Time.deltaTime);
        }
    }

    void TriggerPhase2()
    {
        isPhase2 = true;
        Debug.Log("ATTENTION : PHASE 2 !");
        
        
        if (sr != null) sr.color = colorPhase2;
        
        
    }

    public void ActivateBoss()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        health = GetComponent<EnemyHealth>();
        sr = GetComponent<SpriteRenderer>();
        isActivated = true;
        if(bossUI != null) bossUI.SetActive(true);
    }

    private void OnDrawGizmos()
    {
        
        Gizmos.color = debugColor;
    
        
        Gizmos.color = new Color(debugColor.r, debugColor.g, debugColor.b, 0.1f);
        Gizmos.DrawSphere(transform.position, detectionRange);
    }
}