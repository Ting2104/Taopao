using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows;

public class Spawn : MonoBehaviour
{
    private Rigidbody2D body2d;
    public GameObject player;
    private Animator animator;

    private bool passTheDoor;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        body2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x < body2d.transform.position.x)
        {
            passTheDoor = false;
            animator.SetBool("Pass", passTheDoor);
            animator.Play("OpenDoor");
        }
        if (player.transform.position.x > body2d.transform.position.x)
        {
            passTheDoor = true;
            animator.SetBool("Pass", passTheDoor);
            animator.Play("CloseDoor");
        }
    }
}
