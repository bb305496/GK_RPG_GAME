using System.Collections;
using System.Diagnostics;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.XR;

public class EnemyMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform player;
    private Animator anim;
    private EnemyState enemyState;
    public float playerDetectRange = 5f;
    public Transform detectionPoint;
    public LayerMask playerLayer;

    public float weight = 1f;
    public float attackRange = 2f;
    public float attackCooldown = 2f;
    public float attackCooldownTimer;
    private int facingDirection = 1;
    public float speed = 1f;

    [Header("Thunder Attack Settings")]
    public bool hasThunderAttack = false;
    public float thunderAttackRange = 5f;
    public float thunderAttackCooldown = 5f;
    private float thunderAttackCooldownTimer;
    public float castingTime = 1f; 
    public GameObject thunderPrefab; 
    public Transform thunderSpawnPoint; 

    public EnemyState CurrentState => enemyState;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        ChangeState(EnemyState.Idle);
    }

    private void Update()
    {
        if (enemyState != EnemyState.TakingDamage && enemyState != EnemyState.Casting)
        {
            CheckForPlayer();

            if (attackCooldownTimer > 0) attackCooldownTimer -= Time.deltaTime;
            if (thunderAttackCooldownTimer > 0) thunderAttackCooldownTimer -= Time.deltaTime;

            if (enemyState == EnemyState.Chasing)
            {
                Chase();
            }
            else if (enemyState == EnemyState.Attacking || enemyState == EnemyState.ThunderAttack)
            {
                rb.linearVelocity = Vector2.zero;
            }
        }
    }

    public void Chase()
    {
        
        if (player.position.x > transform.position.x && facingDirection == -1 ||
            player.position.x < transform.position.x && facingDirection == 1)
        {
            Flip();
        }

        Vector2 direction = (player.position - transform.position).normalized;
        rb.linearVelocity = direction * speed;
    }

    void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    private void CheckForPlayer()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(detectionPoint.position, playerDetectRange, playerLayer);

        if (hits.Length > 0)
        {
            player = hits[0].transform;
            float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

            
            bool shouldFlip = (player.position.x > transform.position.x && facingDirection == -1) ||
                              (player.position.x < transform.position.x && facingDirection == 1);

            if (shouldFlip)
            {
                Flip();
            }


            if (hasThunderAttack && distanceToPlayer <= thunderAttackRange &&
                distanceToPlayer > attackRange && thunderAttackCooldownTimer <= 0)
            {
                thunderAttackCooldownTimer = thunderAttackCooldown;
                ChangeState(EnemyState.Casting);
                rb.linearVelocity = Vector2.zero;
                StartCoroutine(ThunderAttackSequence()); 
                return;
            }


            if (distanceToPlayer <= attackRange && attackCooldownTimer <= 0)
            {
                attackCooldownTimer = attackCooldown;
                ChangeState(EnemyState.Attacking);
            }
            else if (distanceToPlayer > attackRange &&
                    enemyState != EnemyState.Attacking &&
                    enemyState != EnemyState.Casting &&
                    enemyState != EnemyState.ThunderAttack)
            {
                ChangeState(EnemyState.Chasing);
            }
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
            ChangeState(EnemyState.Idle);
        }
    }

    public void ChangeState(EnemyState newState)
    {
        if (enemyState == newState) return;
        //Exit current animation
        if (enemyState == EnemyState.Idle)
            anim.SetBool("isIdle", false);
        else if (enemyState == EnemyState.Chasing)
            anim.SetBool("isMoving", false);
        else if (enemyState == EnemyState.Attacking)
            anim.SetBool("isAttacking", false);
        else if(enemyState == EnemyState.Casting)
            anim.SetBool("isCasting", false);
        else if (enemyState == EnemyState.TakingDamage)
            anim.SetBool("isTakingDamage", false);

        //Update our current state
        enemyState = newState;

        //Update new animation
        if (enemyState == EnemyState.Idle)
            anim.SetBool("isIdle", true);
        else if (enemyState == EnemyState.Chasing)
            anim.SetBool("isMoving", true);
        else if (enemyState == EnemyState.Attacking)
            anim.SetBool("isAttacking", true);
        else if (enemyState == EnemyState.Casting)
            anim.SetBool("isCasting", true);
        else if (enemyState == EnemyState.TakingDamage)
            anim.SetBool("isTakingDamage", true);

    }

    private IEnumerator ThunderAttackSequence()
    {
        yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).IsName("Casting") &&
                                      anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f);

        Vector2 thunderPosition = new Vector2(player.position.x, thunderSpawnPoint.position.y + 0.3f);
        Instantiate(thunderPrefab, thunderPosition, Quaternion.identity);

        ChangeState(EnemyState.Chasing); 
    }

    public void OnCastingComplete()
    {
        if (enemyState == EnemyState.Casting)
        {
            Vector2 thunderPosition = new Vector2(player.position.x, thunderSpawnPoint.position.y);
            Instantiate(thunderPrefab, thunderPosition, Quaternion.identity);
            ChangeState(EnemyState.Chasing);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(detectionPoint.position, playerDetectRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(detectionPoint.position, attackRange);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(thunderSpawnPoint.position, thunderAttackRange);
    }


}



public enum EnemyState
{
    Idle,
    Chasing,
    Attacking,
    Casting, 
    ThunderAttack, 
    TakingDamage
}
