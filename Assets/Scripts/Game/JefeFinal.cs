using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using static UnityEngine.GraphicsBuffer;

public class JefeFinal : MonoBehaviour
{
    [SerializeField] GameObject attackController;
    [SerializeField] GameObject player;
    [SerializeField] int maxHealth;
    [SerializeField] int currentHealth;
    [SerializeField] float timeSinceAttack;
    [SerializeField] float nextAttack;
    [SerializeField] int attackDamage;
    [SerializeField] int radioAttack;
    [SerializeField] float followDistance = 5;
    Animator animator;
    Rigidbody2D body2d;
    Vector3 startPosPlayer;
    Vector2 startPos;
    bool death = false;
    bool playerBlock;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        body2d = GetComponent<Rigidbody2D>();
        startPosPlayer = player.transform.position;
        startPos = transform.position;
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        animator.SetTrigger("FB_Hurt");
        animator.Play("FB_Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }

    }
    void Die()
    {
        animator.SetBool("FB_Death", true);
        death = true;
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }
    void Attack()
    {
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackController.transform.position, radioAttack);

        foreach (Collider2D player in hitPlayer)
        {
            if (player.CompareTag("Player"))
            {
                player.GetComponent<Player>().TakeDamage(attackDamage);
            }

        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(attackController.transform.position, radioAttack);
    }
    void Update()
    {
        if (!death)
        {
            if (nextAttack > 0)
                nextAttack -= Time.deltaTime;
            if (player.transform.position.x >= transform.position.x - followDistance && player.transform.position.x < transform.position.x - radioAttack)
            {
                animator.Play("FB_Walk");
            }
            else if (player.transform.position.x >= transform.position.x - radioAttack && nextAttack <= 0)
            {
                playerBlock = player.GetComponent<Player>().BlockTheAttack();
                nextAttack = timeSinceAttack;
                animator.Play("FB_Attack");
                if (!playerBlock)
                {
                    animator.SetTrigger("FB_Attack");
                    Attack();
                }
            }
            else
            {
                animator.Play("FB_Idle");
            }
        }
        if (player.transform.position == startPosPlayer)
        {
            transform.position = startPos;
            death = false;
            currentHealth = maxHealth;
            animator.Play("FB_Idle");
        }
    }
}
