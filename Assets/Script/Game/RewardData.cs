using UnityEngine;

public enum RewardType {Invincibility, FireRate, Dash, MaxHealth}
[CreateAssetMenu(fileName = "NouvelleRecompense", menuName = "Marchand/Recompense")]
public class RewardData : ScriptableObject
{
    public string itemName;
    public RewardType type;
    public float value;
    public Sprite icon;
    public int weight;
}
