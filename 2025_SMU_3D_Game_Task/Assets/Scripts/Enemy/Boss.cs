using UnityEngine;
using UnityEngine.UI;

public class Boss : EnemyBase
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private GameObject mushroom;
    [SerializeField] private GameObject bat;

    private bool isDead = false;

    protected override void Start()
    {
        base.Start();
        if (healthSlider != null)
        {
            healthSlider.maxValue = MaxHealth;
            healthSlider.value = health;
            healthSlider.gameObject.SetActive(true);
        }
    }
    protected override void Update()
    {
        base.Update(); 

        if (!isDead && healthSlider != null)
        {
            healthSlider.value = health;
        }
    }

    protected override void PerformAttack()
    {
        int pattern = Random.Range(0, 3);
        switch(pattern)
        {
            case 0:
                Debug.Log("Basic Attack");
                break;

            case 1:
                SpawnEnemies();
                Debug.Log("Spawn Enemies");
                break;

            case 2:
                StunAnimation();
                Debug.Log("Stun Player");
                break;
        }
    }

    private void SpawnEnemies()
    {
        for(int i = 0; i < 3; i++)
        {
            float randomX = Random.Range(-2, 2);
            float randomZ = Random.Range(-2, 2);
            Vector3 randomPos = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
            Instantiate(mushroom, randomPos, Quaternion.identity);
        }

        for (int i = 0; i < 3; i++)
        {
            float randomX = Random.Range(-2, 2);
            float randomZ = Random.Range(-2, 2);
            Vector3 randomPos = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
            Instantiate(bat, randomPos, Quaternion.identity);
        }
    }

    private void StunAnimation()
    {
        animator.SetTrigger("Attack2");
    }

    private void StunPlayer()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, attackRange, 1 << 6);

        foreach (Collider hit in hits)
        {
            hit.GetComponent<PlayerController>().Stun(3f);
        }
    }

    public override void Damage()
    {
        base.Damage();

        if (health <= 0)
            OnBossDie();
    }

    private void OnBossDie()
    {
        isDead = true;
        if (healthSlider != null)
        {
            healthSlider.gameObject.SetActive(false);

        }
        SceneTransitionManager.Instance.ChangeScene("GameEndingScene");
    }
}
