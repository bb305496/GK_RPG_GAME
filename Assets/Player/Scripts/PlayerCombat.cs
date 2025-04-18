using UnityEngine;
using UnityEngine.UI;

public class PlayerCombat : MonoBehaviour
{
    public static PlayerCombat Instance;

    public Transform attackPoint1;
    public Transform attackPoint2;
    public LayerMask enemyLayer;
    public float weaponRange = 1f;
    public StatsUI stastUI;

    public Animator anim;
    private bool isAttack2Unlocked = false;
    public float attack1Cooldown = 1f;
    private float attack1CooldownTimer;

    public float attack2Cooldown = 2f;
    private float attack2CooldownTimer;

    bool isCooldown1 = false;
    bool isCooldown2 = false;

    public Image attack1ImageBG;
    public Image attack2ImageBG;
    public Image attack2Image;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        attack1ImageBG.fillAmount = 0;
        attack2Image.fillAmount = 0;
    }

    private void Update()
    {
        UpdateAttacksTimer();
        UpdatePlayerUI();
    }

    public void UpdatePlayerUI()
    {
        Attack1UI();
        Attack2UI();
    }

    public void Attack1UI()
    {
        if(anim.GetBool("isAttacking1") && isCooldown1 == false)
        {
            isCooldown1 = true;
            attack1ImageBG.fillAmount = 1;
        }

        if(isCooldown1)
        {
            attack1ImageBG.fillAmount -= 1 / attack1Cooldown * Time.deltaTime;

            if(attack1ImageBG.fillAmount <= 0)
            {
                attack1ImageBG.fillAmount = 0;
                isCooldown1 = false;
            }
        }
    }
    public void Attack2UI()
    {
        if (isAttack2Unlocked)
        {
            if (anim.GetBool("isAttacking2") && isCooldown2 == false)
            {
                isCooldown2 = true;
                attack2ImageBG.fillAmount = 1;
            }

            if (isCooldown2)
            {
                attack2ImageBG.fillAmount -= 1 / attack2Cooldown * Time.deltaTime;

                if (attack2ImageBG.fillAmount <= 0)
                {
                    attack2ImageBG.fillAmount = 0;
                    isCooldown2 = false;
                }
            }
        }
        else
        {
            attack2ImageBG.fillAmount = 1;
            attack2ImageBG.gameObject.SetActive(false);
            attack2Image.gameObject.SetActive(false);
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
        if (isAttack2Unlocked)
        { 
            if (attack2CooldownTimer <= 0)
            {
                anim.SetBool("isAttacking2", true);
                attack2CooldownTimer = attack2Cooldown;
            }
        }
    }

    public void UpdateAttacksTimer()
    {
        if (attack1CooldownTimer > 0)
        {
            attack1CooldownTimer -= Time.deltaTime;
        }

        if (attack2CooldownTimer > 0)
        {
            attack2CooldownTimer -= Time.deltaTime;
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
            foreach (var enemy in enemis)
            {
                enemy.GetComponent<EnemyHealth>().ChangeHealth(-(2 * StatsManager.Instance.damage));
                enemy.GetComponent<EnemyKnockback>().Knockback(transform, 1.2f * StatsManager.Instance.knockbackForce, 1.2f * StatsManager.Instance.knockbackTime, 1.2f * StatsManager.Instance.stunTime);
            }
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

    public void UnlockAttack2()
    {
        isAttack2Unlocked = true;
        attack2ImageBG.fillAmount = 0;
        attack2ImageBG.gameObject.SetActive(true);
        attack2Image.gameObject.SetActive(true);
    }
}
