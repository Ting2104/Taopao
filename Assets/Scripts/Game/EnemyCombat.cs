using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using static Unity.Collections.AllocatorManager;

public class EnemyCombat : Combat
{
    [SerializeField] Transform player;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }
    public override void Die()
    {
        base.Die();
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }
    public override void Attack()
    {
        Collider2D[] hitCharacter = Physics2D.OverlapCircleAll(attackController.transform.position, radioAttack);
        if (!deathPlayer)
        {
            foreach (Collider2D character in hitCharacter)
            {
                if (character.CompareTag("Player"))
                {
                    character.GetComponent<Combat>().TakeDamage(attackDamage);
                }
            }
        }
    }
    public override void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(attackController.transform.position, radioAttack * 2);
    }
    private void Update()
    {
        if (nextAttack > 0)
            nextAttack -= Time.deltaTime;

        if ((player.transform.position.x >= transform.position.x - radioAttack) && (nextAttack <= 0) && !deathPlayer)
        {
            nextAttack = timeSinceAttack;
            if (!block)
            {
                animator.SetTrigger("Attack");
                Attack();
            }
        }
        if (currentHealth <= 0)
        {
            Die();
            deathEnemy = true;
        }
    }
}
