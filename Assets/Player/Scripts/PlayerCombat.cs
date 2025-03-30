using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Transform attackPoint1;
    public Transform attackPoint2;
    public LayerMask enemyLayer;
    public float weaponRange = 1f;

    public Animator anim;
    public float attack1Cooldown = 1f;
    private float attack1CooldownTimer;

    public float attack2Cooldown = 2f;
    private float attack2CooldownTimer;

    private void Update()
    {
        if(attack1CooldownTimer > 0)
        {
            attack1CooldownTimer -= Time.deltaTime;
        }

        if (attack2CooldownTimer > 0)
        {
            attack2CooldownTimer -= Time.deltaTime;
        }
    }

    public void Attack1()
    {
        if(attack1CooldownTimer <= 0)
        {
            anim.SetBool("isAttacking1", true);
            attack1CooldownTimer = attack1Cooldown;
        }
    }

    public void Attack2()
    {
        if (attack2CooldownTimer <= 0)
        {
            anim.SetBool("isAttacking2", true);
            attack2CooldownTimer = attack2Cooldown;
        }
    }

    public void DealDamageAttack1()
    {
        Collider2D[] enemis = Physics2D.OverlapCircleAll(attackPoint1.position, StatsManager.Instance.weaponRange, enemyLayer);

        if (enemis.Length > 0)
        {
            enemis[0].GetComponent<EnemyHealth>().ChangeHealth(-StatsManager.Instance.damage);
            enemis[0].GetComponent<EnemyKnockback>().Knockback(transform, StatsManager.Instance.knockbackForce, StatsManager.Instance.knockbackTime, StatsManager.Instance.stunTime);
        }
    }

    public void DealDamageAttack2()
    {
        Collider2D[] enemis = Physics2D.OverlapCircleAll(attackPoint2.position, StatsManager.Instance.weaponRange, enemyLayer);

        if (enemis.Length > 0)
        {
            enemis[0].GetComponent<EnemyHealth>().ChangeHealth(-(2* StatsManager.Instance.damage));
            enemis[0].GetComponent<EnemyKnockback>().Knockback(transform, 1.2f* StatsManager.Instance.knockbackForce, 1.2f* StatsManager.Instance.knockbackTime, 1.2f* StatsManager.Instance.stunTime);
        }
    }

    public void FinishAttacking1()
    {
        anim.SetBool("isAttacking1", false);
    }

    public void FinishAttacking2()
    {
        anim.SetBool("isAttacking2", false);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(attackPoint1.position, weaponRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint2.position, weaponRange);

    }
}
