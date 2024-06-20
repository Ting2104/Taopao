using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using static Unity.Collections.AllocatorManager;

public class EnemyCombat : Combat
{
    [SerializeField] Transform player;
    private HealthPresenter healthPresenter;
    public static int actualHealth;
    PlayerCombat combatP;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        healthPresenter = GetComponent<HealthPresenter>();
        healthPresenter?.Reset();
        combatP = FindObjectOfType<PlayerCombat>();
        actualHealth = currentHealthEnemy;
    }
    public override void Die()
    {
        base.Die();
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        AudioManager.Instance.PlaySFX("EnemyDie");
    }
    public void Attack()
    {
        Collider2D[] hitCharacter = Physics2D.OverlapCircleAll(attackController.transform.position, radioAttack);
        if (!combatP.deathPlayer)
        {
            foreach (Collider2D character in hitCharacter)
            {
                if (character.CompareTag("Player"))
                {
                    if(!combatP.block)
                        character.GetComponent<PlayerCombat>().TakeDamage(attackDamage);
                }
            }
        }
        AudioManager.Instance.PlaySFX("EnemyAttack");
    }
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(attackController.transform.position, radioAttack * 2);
    }
    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        currentHealthEnemy -= damage;
        healthPresenter?.Damage(damage);
        AudioManager.Instance.PlaySFX("EnemyHurt");
    }
    private void Update()
    {
        if (nextAttack > 0)
            nextAttack -= Time.deltaTime;

        if ((player.transform.position.x >= transform.position.x - radioAttack) && (nextAttack <= 0) && !combatP.deathPlayer)
        {
            nextAttack = timeSinceAttack;
            animator.SetTrigger("Attack");
            Attack();
        }
        if (currentHealthEnemy <= 0 || Input.GetKeyDown(KeyCode.T))
        {
            Die();
            deathEnemy = true;
        }
    }
}
