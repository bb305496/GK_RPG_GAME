using UnityEngine;

public class ThunderBolt : MonoBehaviour
{
    [Header("Settings")]
    public int damage = 3;
    public float lifetime = 0.8f;
    public float strikeDelay = 0.1f;

    [Header("References")]
    public Animator anim;
    public Collider2D hitCollider;

    private void Start()
    {
        hitCollider.enabled = false;
        Invoke("EnableStrike", strikeDelay);
        Destroy(gameObject, lifetime);
    }

    private void EnableStrike()
    {
        hitCollider.enabled = true;
        if (anim != null)
        {
            anim.SetTrigger("Strike");
        }
    }

    public void DealDamage()
    {
        Collider2D[] hits = Physics2D.OverlapBoxAll(hitCollider.bounds.center, hitCollider.bounds.size, 0);

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                hit.GetComponent<PlayerHealth>().ChangeHealth(-damage);
            }
        }
    }
}