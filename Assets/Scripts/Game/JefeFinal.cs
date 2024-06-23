using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Windows;
using static UnityEngine.GraphicsBuffer;

public class JefeFinal : MonoBehaviour
{
    [SerializeField] GameObject attackController;
    [SerializeField] float followDistance = 5;
    [SerializeField] float attackDistance = 2;
    [SerializeField] Transform player;

    Animator animator;
    Rigidbody2D body2d;
    private float delayToIdle = 0.0f;
    private SeekBehavior seekBehavior;
    Vector3 posObject;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        body2d = GetComponent<Rigidbody2D>();
        seekBehavior = FindAnyObjectByType<SeekBehavior>();
        posObject = Vector3.zero;
    }
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);
        if (distanceToPlayer <= followDistance && distanceToPlayer > attackDistance)
        {
            delayToIdle = 0.5f;
            animator.SetInteger("AnimState", 1);
            posObject = player.position;
        }
        else if (distanceToPlayer <= attackDistance)
        {
            delayToIdle -= Time.deltaTime;
            if (delayToIdle < 0)
                animator.SetInteger("AnimState", 0);
        }
        else
        {
            delayToIdle = 0.5f;
            animator.SetInteger("AnimState", 1);
            posObject = seekBehavior._patrolPoints[seekBehavior._currentPoint].position;
        }

        if (posObject.x < transform.position.x)
            GetComponent<SpriteRenderer>().flipX = false;
        else if (posObject.x > transform.position.x)
            GetComponent<SpriteRenderer>().flipX = true;
    }
}
