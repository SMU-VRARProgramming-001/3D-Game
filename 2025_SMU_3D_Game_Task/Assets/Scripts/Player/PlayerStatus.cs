using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance { get; private set; }

    [Header("Base Stats")]
    public int attack = 1;
    public int maxHealth = 10;
    public int currentHealth;

    private void Awake()
    {
        Instance = this;
        currentHealth = maxHealth;
    }

    public void AddAttackStat(int amount)
    {
        attack += amount;
    }

    public void AddMaxHealthStat(int amount)
    {
        maxHealth += amount;
        currentHealth = maxHealth;
    }

    public void ResetStats()
    {
        attack = 1;
        maxHealth = 10;
        currentHealth = maxHealth;
    }
}
