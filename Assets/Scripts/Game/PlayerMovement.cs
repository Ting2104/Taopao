using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] GameObject slideDust;
    [SerializeField] float nextAttack;
    [SerializeField] float timeSinceAttack;
    [SerializeField] float speed;
    [SerializeField] float jumpForce;

    private PlayerCombat combat;
    private Animator animator;
    private Rigidbody2D body2d;
    private PlayerSensor floorSensor;
    private PlayerSensor wallSensorR1;
    private PlayerSensor wallSensorR2;
    private PlayerSensor wallSensorL1;
    private PlayerSensor wallSensorL2;
    private bool onTheFloor = false;
    private bool isWallSliding = false;
    private int facingDirection = 1;
    private float delayToIdle = 0.0f;

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

        combat = FindAnyObjectByType<PlayerCombat>();
    }
    // Update is called once per frame
    void Update()
    {
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

        if (!combat.deathPlayer)
        {
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

            body2d.velocity = new Vector2(inputX * speed, body2d.velocity.y);

            animator.SetFloat("AirSpeedY", body2d.velocity.y);

            /* *** ANIMACIONES *** */
            isWallSliding = (wallSensorR1.State() && wallSensorR2.State()) || (wallSensorL1.State() && wallSensorL2.State());
            animator.SetBool("WallSlide", isWallSliding);


            if ((Input.GetKeyDown("up") || Input.GetKeyDown("w")) && onTheFloor)
            {
                animator.SetTrigger("Jump");
                onTheFloor = false;
                animator.SetBool("Grounded", onTheFloor);
                body2d.velocity = new Vector2(body2d.velocity.x, jumpForce);
                floorSensor.Disable(0.2f);
            }

            else if (Mathf.Abs(inputX) > Mathf.Epsilon)
            {
                delayToIdle = 0.05f;
                animator.SetInteger("AnimState", 1);
            }

            else
            {
                delayToIdle -= Time.deltaTime;
                if (delayToIdle < 0)
                    animator.SetInteger("AnimState", 0);
            }
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
            GameObject dust = Instantiate(slideDust, spawnPosition, gameObject.transform.localRotation) as GameObject;
            dust.transform.localScale = new Vector3(facingDirection, 1, 1);
        }
    }
}
