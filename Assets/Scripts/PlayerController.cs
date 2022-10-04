 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Variables del movimiento del personaje
    public float jumpForce = 6f;
    public Rigidbody2D rigidBody;
    Animator animator;


    //Son las variables que creamos dentro del animator de Unity del personaje principal
    const string STATE_ALIVE = "IsAlive";
    const string STATE_ON_THE_GROUND = "isOnTheGround";

    //Es donde pisará el personaje (Suelo)
    public LayerMask groundMask;

    //Variables de Joystick
    float horizontalMove = 0;
    float verticalMove = 0;

    public float runSpeedHorizontal = 3;
    public float runSpeedVertical = 3;
    public float runSpeed = 0;

    public Joystick joystickPlayer;



    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //Cuando inicia el juego, indicamos que el personaje está vivo y que aún no toca el piso
        animator.SetBool(STATE_ALIVE, true);
        animator.SetBool(STATE_ON_THE_GROUND, false);
    }

    // Update is called once per frame
    void Update()
    {
        //Ejecutará Jump() cuando se presione la tecla de Espacio, Click Derecho y/o Flecha arriba
        /*|| Input.GetMouseButtonDown(0)*/

        if (Input.GetKeyDown(KeyCode.Space)  || Input.GetKeyDown(KeyCode.UpArrow))
        {

            Jump();
        }
        Move();


        animator.SetBool(STATE_ON_THE_GROUND, IsTouchingTheGround());

        //Creamos una linea para que nos ayude a ver la altura del personaje
        Debug.DrawRay(this.transform.position, Vector2.down*1.4f, Color.red);
    }

    //Configuramos el Joystick para que se mueva
    void Move()
    {
        verticalMove = joystickPlayer.Vertical * runSpeedVertical;
        horizontalMove = joystickPlayer.Horizontal * runSpeedHorizontal;

        transform.position += new Vector3(horizontalMove, verticalMove, 0) * Time.deltaTime * runSpeed;
    }

    void Jump()
    {
        if (IsTouchingTheGround())
        {
            rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        

    }

    //Nos indica s el personaje toca el suelo
    bool IsTouchingTheGround() 
    { 
        if(Physics2D.Raycast(this.transform.position,
            Vector2.down,
            1.4f,
            groundMask))
        {
            //TODO: Programar lógica de contacto con el suelo
            animator.enabled = true;//Se activa la animación de salto
            return true;
        }
        else
        {
            //TODO: Programar lógica de no contacto
            animator.enabled = false;//Se desactiva la animación de salto
            return false;
        }
            
    }
}
