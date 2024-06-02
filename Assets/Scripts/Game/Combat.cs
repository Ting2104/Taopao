using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.UIElements;
using static Unity.Collections.AllocatorManager;

public class Combat : MonoBehaviour
{
    [SerializeField] protected GameObject attackController;
    [SerializeField] public int currentHealthEnemy, currentHealthPlayer;
    [SerializeField] protected int radioAttack;
    [SerializeField] protected float timeSinceAttack = 1;
    [SerializeField] protected float nextAttack;
    [SerializeField] protected int attackDamage = 20;
    [SerializeField] public static int maxHealthEnemy, maxHealthPlayer;

    public event Action<bool> IsGameOver;
    public event Action<bool> IsGameWin;

    protected Animator animator;
    protected Rigidbody2D rb;
    public bool block = false;
    public bool deathPlayer = false, deathEnemy = false;   
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currentHealthEnemy = maxHealthEnemy;
        currentHealthPlayer = maxHealthPlayer;
    }
    public virtual void TakeDamage(int damage)
    {
        animator.SetTrigger("Hurt");
        if(deathPlayer)
            IsGameOver?.Invoke(deathPlayer);
        if(deathEnemy)
            IsGameWin?.Invoke(deathEnemy);
    }
    public virtual void Die()
    {
        animator.SetBool("noBlood", true);
        animator.SetTrigger("Death");
    }
}
