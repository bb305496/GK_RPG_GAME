using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
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

    public void FinishAttacking1()
    {
        anim.SetBool("isAttacking", false);
    }
}
