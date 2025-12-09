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
        Cooldown,
        Die
    }

    public EnemyState state = EnemyState.Idle;

    private static readonly WaitForSeconds WaitDelay = new WaitForSeconds(4f);

    [SerializeField] private EnemySO enemySO;
    protected float health;
    public float MaxHealth => enemySO.maxHealth;

    private float idleTimer = 0f;

    protected int attackDamage;
    protected float attackRange;
    private float attackDelayTime;
    private bool isAttacking = false;
    private float cooldownTimer;

    [SerializeField] private ParticleSystem damageParticle;

    private NavMeshAgent agent;
    protected Transform player;
    protected Animator animator;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

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

            case EnemyState.Cooldown:
                Cooldown();
                break;

            case EnemyState.Die:
                break;

        }
    }
    private IEnumerator FindPlayer()
    {
        //yield return WaitDelay;
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
        attackDelayTime = enemySO.attackDelayTime;
        attackRange = enemySO.attackRange;
        agent.stoppingDistance = attackRange;
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

        if (distance <= agent.stoppingDistance)
        {
            animator.SetBool("Walk", false);
            agent.ResetPath(); 
            state = EnemyState.Attack;
            return;
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
            PerformAttack();
            state = EnemyState.Cooldown;
        }
    }

    private void Cooldown()
    {
        isAttacking = false;
        cooldownTimer += Time.deltaTime;
        if (cooldownTimer >= attackDelayTime)
        {
            cooldownTimer = 0f;
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
        agent.ResetPath();
        if (health <= 0)
        {
            state = EnemyState.Die;
            Die();
        }
        else
        {
            state = EnemyState.Cooldown;
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
