using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float detectionRange = 10f;
    public float attackRange = 1.5f;
    public float damage = 10f;
    public float attackRate = 1f;
    private float nextAttackTime;
    
    private Transform player;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null) player = playerObj.transform;
    }

    void Update()
    {
        if (player == null) return;
        float distance = Vector2.Distance(transform.position, player.position);
        if (distance <= detectionRange)
        {
            if (distance > attackRange)
            {
                MoveTowardsPlayer();
            }
            else
            {
                AttackPlayer();
            }
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    void MoveTowardsPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.linearVelocity = direction * moveSpeed;
        
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }

    void AttackPlayer()
    {
        rb.linearVelocity = Vector2.zero;

        if (Time.time >= nextAttackTime)
        {
            PlayerController playerHealth = player.GetComponent<PlayerController>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                Debug.Log("L'ennemi attaque");
            }

            nextAttackTime = Time.time + attackRate;
        }
    }
}
