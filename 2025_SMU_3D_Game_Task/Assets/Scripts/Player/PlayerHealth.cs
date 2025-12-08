using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TMP_Text maxHealthTxt;
    [SerializeField] private TMP_Text curHealthTxt;

    private float curHealth;
    private float maxHealth;
    private void OnEnable()
    {
        PlayerStats.Instance.OnStatsChanged += UpdateMaxHealth;
    }

    private void OnDisable()
    {
        PlayerStats.Instance.OnStatsChanged -= UpdateMaxHealth;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateMaxHealth();
    }
    public void Damage(int damage)
    {
        PlayerStats.Instance.ApplyDamage(damage);

        curHealth = PlayerStats.Instance.curHealth;
        UpdateUI();

        if (curHealth <= 0)
        {
            Debug.Log("Player Dead");
            GameManager.Instance.GameOver();
        }
    }

    private void UpdateMaxHealth()
    {
        maxHealth = PlayerStats.Instance.maxHealth;
        curHealth = PlayerStats.Instance.curHealth;
        UpdateUI();
    }

    private void UpdateUI()
    {
        curHealthTxt.text = curHealth.ToString();
        maxHealthTxt.text = maxHealth.ToString();
        healthSlider.value = (float)curHealth / (float)maxHealth;
    }
}
