using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private float maxHealth= 10;
    public float health;
    [SerializeField] private Slider healthSlider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = maxHealth;
        healthSlider.value = health / maxHealth;
    }
    public void Damage(float damage)
    {
        health -= damage;
        healthSlider.value = health / maxHealth;
        if(health < 0)
        {
            Debug.Log("GameOver");
        }
    }
}
