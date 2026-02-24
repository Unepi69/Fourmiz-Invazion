using UnityEngine;
using TMPro;
using System.Collections;

public class InventoryFeedback : MonoBehaviour
{
    public TextMeshProUGUI moneyText;
    private Color originalColor;

    void Awake() // On utilise Awake pour être sûr de choper la couleur avant le Start
    {
        if (moneyText != null) originalColor = moneyText.color;
    }

    public void UpdateMoneyUI(int amount)
    {
        if (moneyText != null) 
            moneyText.text = "Ressources: " + amount;
    }

    public void TriggerError()
    {
        StopAllCoroutines();
        StartCoroutine(ErrorRoutine());
    }

    IEnumerator ErrorRoutine()
    {
        moneyText.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        moneyText.color = originalColor;
    }
}
