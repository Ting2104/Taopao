using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class Spawn : MonoBehaviour
{
    private Rigidbody2D body2d;
    public GameObject player;
    private Animator animator;

    private bool doorClose = false;
    private bool passTheDoor = false;
    public float spawnDistance;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        body2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Mientras el personaje no haya pasado la puerta
        if (!passTheDoor)
        {
            //Puerta abierta
            if (player.transform.position.x < body2d.transform.position.x)
            {
                doorClose = false;
                animator.Play("OpenDoor");
            }
            //Paso la spawnDistance de la puerta
            else if (player.transform.position.x > body2d.transform.position.x)
            {
                doorClose = true;
                passTheDoor = true;
                //Puerta cerrando
                animator.Play("MoveDoor");
            }
        }
        //Cuando la puerta está cerrada, para que siga en la misma posición
        else if (doorClose)
        {
            //Puerta cerrada
            animator.Play("CloseDoor");
        }
    }
}
