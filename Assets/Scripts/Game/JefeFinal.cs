using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using static UnityEngine.GraphicsBuffer;

public class JefeFinal : MonoBehaviour
{
    [SerializeField] GameObject attackController;
    [SerializeField] int maxHealth;
    [SerializeField] float followDistance = 5;
    [SerializeField] float attackDistance = 2;
    [SerializeField] Transform player;

    Animator animator;
    Rigidbody2D body2d;
    private float delayToIdle = 0.0f;

    //bool death = false;
    bool playerBlock;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        body2d = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if ((player.transform.position.x >= transform.position.x - followDistance && player.transform.position.x <= transform.position.x - attackDistance) ||
            (player.transform.position.x <= transform.position.x + followDistance && player.transform.position.x >= transform.position.x + attackDistance))
        {
            delayToIdle = 0.5f;
            animator.SetInteger("AnimState", 1);
        }
        else
        {
            delayToIdle -= Time.deltaTime;
            if (delayToIdle < 0)
                animator.SetInteger("AnimState", 0);
        }

        if (player.transform.position.x < transform.position.x)
            GetComponent<SpriteRenderer>().flipX = false;
        else if (player.transform.position.x > transform.position.x)
            GetComponent<SpriteRenderer>().flipX = true;
    }
}
