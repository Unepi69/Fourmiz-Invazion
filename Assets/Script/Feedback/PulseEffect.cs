using UnityEngine;

public class PulseEffect : MonoBehaviour
{
   public float pulseSpeed = 2f;
   public float maxScale = 1.2f;
   private Vector3 originalScale;

   void Start()
   {
      originalScale = transform.localScale;
   }

   void Update()
   {
      float scale = Mathf.PingPong(Time.time * pulseSpeed, maxScale - originalScale.x) +  originalScale.x;
      transform.localScale = new Vector3(scale, scale, 1);
   }
}
