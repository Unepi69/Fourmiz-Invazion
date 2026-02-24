using UnityEngine;

public class Bullet : MonoBehaviour
{
  public float damage = 25f;
  public GameObject impactEffect;

  void OnCollisionEnter2D(Collision2D collision)
  {
    EnemyHealth enemy = collision.gameObject.GetComponentInParent<EnemyHealth>();

    if (enemy != null)
    {
      enemy.TakeDamage(damage+ GlobalStats.bonusDamage);
    }
    else
    {
      Debug.Log("La balle a touché un objet sans EnemyHealth:" + collision.gameObject.name);
    }
    
    if (impactEffect != null)
    {
      Instantiate(impactEffect, transform.position, Quaternion.identity);
    }
    
    Destroy(gameObject);
  }
}
