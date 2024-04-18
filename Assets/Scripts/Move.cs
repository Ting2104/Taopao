using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour
{

    [SerializeField] float speed = 4.0f;
    [SerializeField] float jumpForce = 7.5f;
    [SerializeField] float rollForce = 6.0f;
    [SerializeField] bool noBlood = false;
    [SerializeField] GameObject slideDust;

    private Animator animator;
    private Rigidbody2D body2d;
    private PlayerSensor floorSensor;
    private PlayerSensor wallSensorR1;
    private PlayerSensor wallSensorR2;
    private PlayerSensor wallSensorL1;
    private PlayerSensor wallSensorL2;
    private bool isWallSliding = false;
    public bool onTheFloor = false;
    private bool rolling = false;
    private int facingDirection = 1;
    private int currentAttack = 0;
    private float timeSinceAttack = 0.0f;
    private float delayToIdle = 0.0f;
    public float rollDuration = 8.0f / 14.0f;
    private float rollCurrentTime;


    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        body2d = GetComponent<Rigidbody2D>();

        //Sensores de los brazos, las piernas y del suelo
        floorSensor = transform.Find("FloorSensor").GetComponent<PlayerSensor>();
        wallSensorR1 = transform.Find("WallSensor_R1").GetComponent<PlayerSensor>();
        wallSensorR2 = transform.Find("WallSensor_R2").GetComponent<PlayerSensor>();
        wallSensorL1 = transform.Find("WallSensor_L1").GetComponent<PlayerSensor>();
        wallSensorL2 = transform.Find("WallSensor_L2").GetComponent<PlayerSensor>();
    }

    // Update is called once per frame
    void Update()
    {
        //Aumentar el tiempo del combo de ataque y el tiempo de hacer volteretas 
        timeSinceAttack += Time.deltaTime;
        if (rolling)
            rollCurrentTime += Time.deltaTime;

        //Dejar de rodar si el tiempo se pasa del tiempo puesto (rollDuration)
        if (rollCurrentTime > rollDuration)
            rolling = false;

        //Comprovar si el personaje esta EN EL SUELO o si esta CAYENDO
        if (!onTheFloor && floorSensor.State())
        {
            onTheFloor = true;
            animator.SetBool("Grounded", onTheFloor);
        }
        if (onTheFloor && !floorSensor.State())
        {
            onTheFloor = false;
            animator.SetBool("Grounded", onTheFloor);
        }

        /* *** ENTRADA Y MOVIMIENTO *** */
        float inputX = Input.GetAxis("Horizontal");

        //Cambiar la dirección del personaje
        if (inputX > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            facingDirection = 1;
        }

        else if (inputX < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            facingDirection = -1;
        }

        //Mover
        if (!rolling)
            body2d.velocity = new Vector2(inputX * speed, body2d.velocity.y);

        //Establecer AirSpeed en animator
        animator.SetFloat("AirSpeedY", body2d.velocity.y);

        /* *** ANIMACIONES *** */
        //Personaje cayendo
        isWallSliding = (wallSensorR1.State() && wallSensorR2.State()) || (wallSensorL1.State() && wallSensorL2.State());
        animator.SetBool("WallSlide", isWallSliding);

        //Muere el personaje
        if (Input.GetKeyDown("e") && !rolling)
        {
            animator.SetBool("noBlood", noBlood);
            animator.SetTrigger("Death");
        }

        //Recibe un ataque
        else if (Input.GetKeyDown("q") && !rolling)
            animator.SetTrigger("Hurt");

        //Atacar
        else if (Input.GetMouseButtonDown(0) && timeSinceAttack > 0.25f && !rolling)
        {
            /*Hay tres animaciones de ataque*/
            currentAttack++;

            //Repetidor de las animaciones (cuando llega al 3, vuelve al 1)
            if (currentAttack > 3)
                currentAttack = 1;

            //Reiniciar combo ataque cuando se pasa del tiempo 
            if (timeSinceAttack > 1.0f)
                currentAttack = 1;

            //Llamar uno de las tres animaciones de atacar
            animator.SetTrigger("Attack" + currentAttack);

            //Reiniciar tiempo de ataque
            timeSinceAttack = 0.0f;
        }

        //Bloquear el ataque de los enemigos
        else if (Input.GetMouseButtonDown(1) && !rolling)
        {
            animator.SetTrigger("Block");
            animator.SetBool("IdleBlock", true);
        }

        else if (Input.GetMouseButtonUp(1))
            animator.SetBool("IdleBlock", false);

        //Voltereta
        else if (Input.GetKeyDown("left shift") && !rolling && !isWallSliding)
        {
            rolling = true;
            animator.SetTrigger("Roll");
            body2d.velocity = new Vector2(facingDirection * rollForce, body2d.velocity.y);
        }


        //Saltar
        else if ((Input.GetKeyDown("up") || Input.GetKeyDown("w")) && onTheFloor && !rolling)
        {
            animator.SetTrigger("Jump");
            onTheFloor = false;
            animator.SetBool("Grounded", onTheFloor);
            body2d.velocity = new Vector2(body2d.velocity.x, jumpForce);
            floorSensor.Disable(0.2f);
        }

        //Correr
        else if (Mathf.Abs(inputX) > Mathf.Epsilon)
        {
            delayToIdle = 0.05f;
            animator.SetInteger("AnimState", 1);
        }

        //Idle
        else
        {
            //Evitar parpadeo de las transiciones 
            delayToIdle -= Time.deltaTime;
            if (delayToIdle < 0)
                animator.SetInteger("AnimState", 0);
        }
    }

    /* *** EVENTOS DE LA ANIMACIÓN *** */
    void AE_SlideDust()
    {
        Vector3 spawnPosition;

        if (facingDirection == 1)
            spawnPosition = wallSensorR2.transform.position;
        else
            spawnPosition = wallSensorL2.transform.position;

        if (slideDust != null)
        {
            //Ajustar la posición correcta de la flecha
            GameObject dust = Instantiate(slideDust, spawnPosition, gameObject.transform.localRotation) as GameObject;
            //Girar la flecha en la dirección correcta
            dust.transform.localScale = new Vector3(facingDirection, 1, 1);
        }
    }
}
