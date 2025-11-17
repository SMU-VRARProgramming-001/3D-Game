using UnityEngine;

[CreateAssetMenu(fileName = "EnemySO", menuName = "Scriptable Objects/EnemySO")]
public class EnemySO : ScriptableObject
{
    [Header("Enemy Info")]
    public string enemyName;

    [Header("Stats")]
    public float maxHealth;
    public float moveSpeed;
    public float attackDamage;

    [Header("GameObject")]
    public GameObject enemyPrefab;
}
