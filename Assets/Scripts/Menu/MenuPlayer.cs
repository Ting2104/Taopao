using UnityEngine;
using System.Collections;

public class MenuPlayer : MonoBehaviour
{
    [SerializeField] GameObject slideDust;
    [SerializeField] GameObject attackController;
    [SerializeField] float nextAttack;
    [SerializeField] float timeSinceAttack;
    [SerializeField] float speed;
    [SerializeField] float jumpForce;

    private Animator animator;
    private Rigidbody2D body2d;
    private PlayerSensor floorSensor;
    private PlayerSensor wallSensorR1;
    private PlayerSensor wallSensorR2;
    private PlayerSensor wallSensorL1;
    private PlayerSensor wallSensorL2;
    private bool onTheFloor = false;
    private int currentAttack = 0;
    private float delayToIdle = 0.0f;
    Vector2 startPos;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        body2d = GetComponent<Rigidbody2D>();
        startPos = transform.position;

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
        body2d.velocity = new Vector2(body2d.velocity.x, body2d.velocity.y);

        animator.SetFloat("AirSpeedY", body2d.velocity.y);

        /* *** ANIMACIONES *** */
        if (nextAttack > 0)
            nextAttack -= Time.deltaTime;
        if (Input.GetMouseButtonDown(0) && nextAttack <= 0)
        {
            /*Hay tres animaciones de ataque*/
            currentAttack++;

            if (currentAttack > 3)
                currentAttack = 1;

            if (timeSinceAttack > 1.0f)
                currentAttack = 1;

            animator.SetTrigger("Attack" + currentAttack);
            AudioManager.Instance.PlaySFX("PlayerAttack");

            nextAttack = timeSinceAttack;
        }


        else if (Input.GetMouseButtonDown(1))
        {
            animator.SetTrigger("Block");
            animator.SetBool("IdleBlock", true);
            AudioManager.Instance.PlaySFX("PlayerBlock");
        }

        else if (Input.GetMouseButtonUp(1))
            animator.SetBool("IdleBlock", false);

        /*else if (Mathf.Abs(inputX) > Mathf.Epsilon)
        {
            delayToIdle = 0.05f;
            animator.SetInteger("AnimState", 1);
        }*/

        else
        {
            delayToIdle -= Time.deltaTime;
            if (delayToIdle < 0)
                animator.SetInteger("AnimState", 0);
        }
        if (Input.GetKeyDown("r"))
        {
            transform.position = startPos;
            animator.Play("Idle");
        }
    }
}
