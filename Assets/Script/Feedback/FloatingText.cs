using UnityEngine;
using TMPro; // Obligatoire

public class FloatingText : MonoBehaviour
{
   public float moveSpeed = 2f;
   public float destroyTime = 0.8f;
    
   // Utilise "TMP_Text" : c'est le type universel qui accepte 
   // le TextMeshPro classique ET la version UI.
   public TMP_Text textMesh; 

   void Start()
   {
      Destroy(gameObject, destroyTime);
   }

   void Update()
   {
      // Le texte monte vers le haut
      transform.position += Vector3.up * moveSpeed * Time.deltaTime;
   }

   public void SetText(string content)
   {
      if (textMesh != null)
         textMesh.text = content;
   }
}
