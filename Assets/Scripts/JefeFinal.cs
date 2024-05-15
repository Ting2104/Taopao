using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class JefeFinal : MonoBehaviour
{
    Animator animator;
    Rigidbody2D body2d;
    public GameObject player;
    private float distance = 5;

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
            animator.Play("FB_Walk");
        }
        else if (player.transform.position.x > transform.position.x - 2 && player.transform.position.x < transform.position.x + 2)
        {
            animator.Play("FB_Attack");
        }
        else
        {
            animator.Play("FB_Idle");
        }
    }
}
