using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.UIElements;
using static Unity.Collections.AllocatorManager;

public class PlayerCombat : Combat
{
    int currentAttack = 0;
    private HealthPresenter healthPresenter;
    public static int actualHealth;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        healthPresenter = GetComponent<HealthPresenter>();
        healthPresenter?.Reset();
        actualHealth = currentHealthPlayer;
    }
    public void Attack()
    {
        Collider2D[] hitCharacter = Physics2D.OverlapCircleAll(attackController.transform.position, radioAttack);
        if (!deathEnemy)
        {
            foreach (Collider2D character in hitCharacter)
            {
                if (character.CompareTag("Enemy"))
                {
                    character.GetComponent<EnemyCombat>().TakeDamage(attackDamage);
                }
            }
        }
        AudioManager.Instance.PlaySFX("PlayerAttack");
    }
    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        currentHealthPlayer -= damage;
        healthPresenter?.Damage(damage);
        AudioManager.Instance.PlaySFX("PlayerHit");
    }
    private void Update()
    {
        if (nextAttack > 0)
            nextAttack -= Time.deltaTime;
        if (!deathPlayer)
        {
            if (Input.GetMouseButtonDown(0) && nextAttack <= 0)
            {
                /*Hay tres animaciones de ataque*/
                currentAttack++;

                if (currentAttack > 3)
                    currentAttack = 1;

                if (timeSinceAttack > 1.0f)
                    currentAttack = 1;

                animator.SetTrigger("Attack" + currentAttack);
                Attack();

                nextAttack = timeSinceAttack;
            }
            else if (Input.GetMouseButtonDown(1))
            {
                block = true;
                animator.SetTrigger("Block");
                animator.SetBool("IdleBlock", true);
                AudioManager.Instance.PlaySFX("PlayerBlock");
            }

            else if (Input.GetMouseButtonUp(1))
            {
                block = false;
                animator.SetBool("IdleBlock", false);
            }

            if (currentHealthPlayer <= 0 || Input.GetKeyDown(KeyCode.R))
            {
                deathPlayer = true;
                Die();
                AudioManager.Instance.PlaySFX("PlayerDie");
            }
        }
    }
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackController.transform.position, radioAttack);
    }
}
