using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using static UnityEngine.GraphicsBuffer;

public class Bandido : MonoBehaviour
{
    Animator animator;
    Rigidbody2D body2d;
    public GameObject player;
    float distance = 5;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        body2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (player.transform.position.x >= transform.position.x - distance && player.transform.position.x <= transform.position.x - 2)
        {
            animator.Play("B_Run");
        }
        else if (player.transform.position.x <= transform.position.x + distance && player.transform.position.x >= transform.position.x + 2)
        {
            animator.Play("B_Run");
        }
        else if (player.transform.position.x > transform.position.x - 2 && player.transform.position.x < transform.position.x + 2)
        {
            animator.Play("B_Attack");
        }
        else
        {
            animator.Play("B_Idle");
        }

        if (player.transform.position.x > body2d.position.x)
            GetComponent<SpriteRenderer>().flipX = true;
        else if (player.transform.position.x < body2d.position.x)
            GetComponent<SpriteRenderer>().flipX = false;
    }
}
