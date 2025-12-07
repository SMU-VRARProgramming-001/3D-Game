using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance { get; private set; }

    public event System.Action OnStatsChanged;

    public int statPoint = 0;
    [Header("Base Stats")]
    public int attackPower = 1;
    public int maxHealth = 10;
    public int curHealth = 10;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); 
    }

    public void ApplyDamage(int amount)
    {
        curHealth -= amount;
        curHealth = Mathf.Clamp(curHealth, 0, maxHealth);
        OnStatsChanged?.Invoke();
    }

    public void AddAttackStat(int amount)
    {
        attackPower += amount;
        OnStatsChanged?.Invoke();
    }

    public void AddMaxHealthStat(int amount)
    {
        maxHealth += amount;
        curHealth = maxHealth;
        OnStatsChanged?.Invoke();
    }

    public void ResetStats()
    {
        statPoint = 0;
        attackPower = 1;
        maxHealth = 10;
        curHealth = maxHealth;
        OnStatsChanged?.Invoke();
    }
}
