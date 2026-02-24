using UnityEngine;

public class ResourceItem : MonoBehaviour
{
   public int value = 10;
   public float followSpeed = 10f;
   private Transform player;
   private bool isFollowing = false;

   void Start()
   {
      player = GameObject.FindGameObjectWithTag("Player").transform;
      // Petit saut aléatoire à l'apparition
      GetComponent<Rigidbody2D>().AddForce(Random.insideUnitCircle * 5f, ForceMode2D.Impulse);
      Invoke("StartFollowing", 0.5f);
   }

   void StartFollowing() { isFollowing = true; }

   void Update()
   {
      if (isFollowing && player != null)
      {
         transform.position = Vector3.MoveTowards(transform.position, player.position, followSpeed * Time.deltaTime);

         if (Vector3.Distance(transform.position, player.position) < 0.2f)
         {
            Collect();
         }
      }
   }

   void Collect()
   {
      PlayerInventory inv = player.GetComponent<PlayerInventory>();
      int finalAmount = value + (int)GlobalStats.bonusResource;
      inv.AddResources(finalAmount);

      // Feedback : Texte flottant
      if (inv.popupPrefab != null)
      {
         GameObject popup = Instantiate(inv.popupPrefab, transform.position, Quaternion.identity);
         popup.GetComponent<FloatingText>().SetText("+" + finalAmount);
      }

      Destroy(gameObject);
   }
}
