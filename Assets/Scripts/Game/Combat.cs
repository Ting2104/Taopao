using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.UIElements;
using static Unity.Collections.AllocatorManager;

public abstract class Combat : MonoBehaviour
{
    [SerializeField] protected GameObject attackController;
    [SerializeField] protected int currentHealth;
    [SerializeField] protected int maxHealth;
    [SerializeField] protected int radioAttack;
    [SerializeField] protected float timeSinceAttack = 1;
    [SerializeField] protected float nextAttack;
    [SerializeField] protected int attackDamage = 20;
    protected Animator animator;
    protected Rigidbody2D rb;
    protected bool block = false;
    protected bool deathPlayer = false, deathEnemy = false;   
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        currentHealth = maxHealth;
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        animator.SetTrigger("Hurt");
    }
    public virtual void Die()
    {
        animator.SetBool("noBlood", true);
        animator.SetTrigger("Death");
    }
    public abstract void Attack();
    public abstract void OnDrawGizmos();
}
