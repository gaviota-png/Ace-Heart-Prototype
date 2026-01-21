using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.Rendering;


public class CharacterMovement : MonoBehaviour
{
    [Header("Components")]
    private CharacterController controller;
    private Animator animator;
    [SerializeField] AnimationCurve rollCurve;

    [Header("Variables")]
    [SerializeField] float gravity = 9.81f;
    [SerializeField] float rollSpeed = 1.0f;
    float rollTime = 1.0f;
    float rollStop = 0.1f;

    float currentRollTime;
    float newHeight = 1.12f;
    Vector3 newCenter = new Vector3(0f, 0.1f, 0.6f);
    float ogHeight;
    Vector3 ogCenter;

    private float vertVeloc;
    
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

        currentRollTime = rollTime;

        ogHeight = controller.height;
        ogCenter = controller.center;
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

        atkInput = Input.GetKey(KeyCode.K);

        if (atkInput)
        {
            animator.SetBool("isAttacking", true);

        }
        else
        {

            animator.SetBool("isAttacking", false);
        }


    }

    #region Animation Events
    public void AnimationCollider()
    {
     // Debug.Log("Collider change");
        controller.height = newHeight;
        controller.center = newCenter;
    }

    public void ColliderReset()
    {
      //Debug.Log("Collider reset");
        controller.height = ogHeight;
        controller.center = ogCenter;
    }

    public void ThrowCard()
    {
        Debug.Log("Card Throw");
    }

    #endregion

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
    }
}
