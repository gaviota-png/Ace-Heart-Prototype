using Unity.VisualScripting;
using UnityEngine;


public class CharacterMovement : MonoBehaviour
{
    [Header("Components")]
    private CharacterController controller;
    private Animator animator;


    [Header("Variables")]
    [SerializeField] private float gravity = 9.81f;

    public float vertVeloc;

    [Header("Player Input")]

    private float moveInput;
    private float turnInput;
    private bool atkInput;
    private bool rollInput;
    private bool tpInput;


    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    private void PlayerMovement()
    {
        GroundMovement();//movimiento de personaje
        //teleport
        //ataque + proyectil
    }

    //calculo de gravedad
    private float GravityCalc()
    {
        if (controller.isGrounded)
        {
            vertVeloc = -1f;
        }
        else { vertVeloc -= gravity * Time.deltaTime;}
        return vertVeloc;
    }
    private void GroundMovement()
    {
        
        moveInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");

        Vector3 movement = new Vector3(turnInput, 0, moveInput);        

        //Si hay input, moverse en direccion y quedarse mirando a la ult si no hay.
        if (moveInput != 0 || turnInput != 0)
        {
            transform.rotation = Quaternion.LookRotation(movement);
        }

        movement.y = GravityCalc();
        controller.Move(movement / 8);

        //Inputs para animator
        if (moveInput != 0 || turnInput != 0){
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning",false);
        }

        rollInput = Input.GetKey(KeyCode.Space);

        if (rollInput)
        {
            animator.SetBool("isRolling", true);
        }
        else
        {
            animator.SetBool("isRolling", false);
        }    
        
    }
    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
    }
}
