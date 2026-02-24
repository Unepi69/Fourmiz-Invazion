using UnityEngine;

public class Weapon : MonoBehaviour
{
   public GameObject bulletPrefab;
   public Transform firePoint;
   public float firForce = 20f;

   public void Fire()
   {
      GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
      bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.up * firForce, ForceMode2D.Impulse);
   }
}
