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

    private bool hasStruck;

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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasStruck && other.CompareTag("Player"))
        {
            hasStruck = true;
            other.GetComponent<PlayerHealth>().ChangeHealth(-damage);
        }
    }
}