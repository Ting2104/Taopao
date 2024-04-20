using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class Spawn : MonoBehaviour
{
    //Este código aún no se ha aplicado en el juego
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
        while(!passTheDoor)
        {
            /* *** ANIMACIONES PUERTA *** */
            //Aún no ha pasado. Puerta abierta
            if (player.transform.position.x < body2d.transform.position.x + spawnDistance)
            {
                doorClose = false;
                while (doorClose == false)
                    animator.Play("OpenDoor", 0);
            }
            //Paso la spawnDistance de la puerta. Puerta cerrando
            else
            {
                doorClose = true;
                animator.Play("MoveDoor", 0);
                passTheDoor = true;
            }
        }
        //Cuando la puerta está cerrada, para que siga en la misma posición
        while(doorClose)
        {
            //Paso y se fue. Puerta cerrada
            animator.Play("CloseDoor", 0);
        }
    }
}
