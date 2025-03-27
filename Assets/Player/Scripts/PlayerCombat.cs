using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Transform attackPoint;
    public float weaponRange = 1f;
    public LayerMask enemyLayer;
    public int damage = 1;


    public Animator anim;
    public float attack1Cooldown = 1f;
    private float attack1CooldownTimer;

    private void Update()
    {
        if(attack1CooldownTimer > 0)
        {
            attack1CooldownTimer -= Time.deltaTime;
        }
    }

    public void Attack1()
    {
        if(attack1CooldownTimer <= 0)
        {
            anim.SetBool("isAttacking", true);
            attack1CooldownTimer = attack1Cooldown;
        }
    }

    public void DealDamage()
    {
        Collider2D[] enemis = Physics2D.OverlapCircleAll(attackPoint.position, weaponRange, enemyLayer);

        if (enemis.Length > 0)
        {
            enemis[0].GetComponent<EnemyHealth>().ChangeHealth(-damage);
        }
    }

    public void FinishAttacking1()
    {
        anim.SetBool("isAttacking", false);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(attackPoint.position, weaponRange);

    }
}
