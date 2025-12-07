using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : MonoBehaviour
{
    public enum EnemyState
    {
        Idle,
        Move,
        Attack,
        Damage,
        Die
    }

    public EnemyState state = EnemyState.Idle;

    private float minDistance = 3f;

    [SerializeField] private EnemySO enemySO;
    protected float health;
    public float MaxHealth => enemySO.maxHealth;

    private float idleTimer = 0f;

    protected int attackDamage;
    protected float attackRange;
    private float attackDelayTime;
    private float attackTimer;
    private bool isAttacking = false;

    [SerializeField] private ParticleSystem damageParticle;

    private NavMeshAgent agent;
    protected Transform player;
    protected Animator animator;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        StartCoroutine(FindPlayer());
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        //Debug.Log($"current State: {state}");
        switch (state)
        {
            case EnemyState.Idle:
                Idle();
                break;

            case EnemyState.Move:
                Move();
                break;

            case EnemyState.Attack:
                Attack();
                break;

            case EnemyState.Damage:
                break;

            case EnemyState.Die:
                break;

        }
    }
    private IEnumerator FindPlayer()
    {
        while (player == null)
        {
            GameObject target = GameObject.FindWithTag("Player");
            if (target != null)
            {
                player = target.transform;
                break;
            }
            yield return null; 
        }

        health = MaxHealth;
        agent.speed = enemySO.moveSpeed;
        attackDamage = enemySO.attackDamage;
        attackRange = enemySO.attackRange;
        attackDelayTime = enemySO.attackDelayTime;
    }

    private void Idle()
    {
        animator.SetBool("Walk", false);

        idleTimer += Time.deltaTime;
        if (idleTimer > 2f)
        {
            idleTimer = 0f;          
            state = EnemyState.Move;
        }
    }
    private void Move()
    {
        animator.SetBool("Walk", true);

        Vector3 dir = (player.position - transform.position).normalized;
        dir.y = 0;

        Quaternion targetRot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * 5f);

        agent.SetDestination(player.position);

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > minDistance)
        {
            agent.SetDestination(player.position);
        }
        else
        {
            agent.ResetPath(); 
        }

        if (distance < attackRange)
        {
            state = EnemyState.Attack;
        }
    }
    private void Attack()
    {
        Vector3 dir = (player.position - transform.position).normalized;
        dir.y = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 7f);

        if (!isAttacking)
        {
            isAttacking = true;
            attackTimer = 0f;
            PerformAttack();
        }

        attackTimer += Time.deltaTime;
        if (attackTimer >= attackDelayTime)
        {
            isAttacking = false;
            state = EnemyState.Move;
        }
    }

    protected virtual void PerformAttack()
    {
        animator.SetTrigger("Attack");
    }

    public void DoDamage()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, attackRange, 1 << 6);

        foreach (Collider hit in hits)
        {
            hit.GetComponent<PlayerHealth>().Damage(attackDamage);
        }
    }

    public virtual void Damage()
    {
        Debug.Log("damage clear");
        if (health <= 0)
        {
            state = EnemyState.Die;
            Die();
        }
        else
        {
            state = EnemyState.Move;
        }
    }

    public void Damaged(int damage)
    {
        if (state == EnemyState.Die || health <= 0) return;

        animator.SetTrigger("Damage");
        health -= damage;
        damageParticle.Play();
        state = EnemyState.Damage;
    }
    private void Die()
    {
        animator.SetTrigger("Die");
        agent.velocity = Vector3.zero;
        FindAnyObjectByType<StageManager>()?.OnEnemyKilled();
    }
    public void Destoy()
    {
        gameObject.SetActive(false);
    }
}
