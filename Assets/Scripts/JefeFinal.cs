using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class JefeFinal : MonoBehaviour
{
    //[SerializeField] float speed = 4.0f;
    protected Animator animator;
    protected Rigidbody2D body2d;
    public GameObject player;
    //private int facingDirection = 1;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        body2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (player.transform.position.x + 5 >= transform.position.x)
            animator.Play("F_B_Run");
        else if (player.transform.position.x - 1 == body2d.position.x || player.transform.position.x + 1 == body2d.position.x)
            animator.Play("F_B_Attack");
        else
            animator.Play("F_B_Idle");

    }
}
