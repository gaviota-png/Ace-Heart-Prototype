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
    private CardSpawner cSpawn;
    [SerializeField] private ParticleSystem dashCloud;
    [SerializeField] private Transform cloudSpawn;
    

    [Header("Variables")]
    [SerializeField] float gravity = 9.81f;
    [SerializeField] float rollSpeed = 8.0f;
    [SerializeField] float rollDist = 0.25f;
    [SerializeField] float playerSpeed = 5.0f;


    private float vertVeloc;

    private bool isRolling = false;
    private float rollTime;
    private float rollCDT;
    private float rollCD;
    
    private float newHeight = 1.12f;
    private float ogHeight;

    Vector3 movement;
    Vector3 rollMovement;
    Vector3 lastMove;
    Vector3 newCenter = new Vector3(0f, 0.1f, 0.6f);
    Vector3 ogCenter;
     
    [Header("Player Input")]

    private float moveInput;
    private float turnInput;
    private bool rollInput;
    private bool tpInput;


    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        cSpawn = GetComponent<CardSpawner>();
     


        ogHeight = controller.height;
        ogCenter = controller.center;
    }

    private void PlayerMovement()
    {
        GroundMovement();//movimiento de personaje
        //teleport

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

    private void SpawnCloud()
    {
        dashCloud.Play();
    }
    private void GroundMovement()
    {
        
        moveInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");

        movement = new Vector3(turnInput, 0, moveInput).normalized;        

        //Si hay input, moverse en direccion y quedarse mirando a la ult si no hay.
        if (moveInput != 0 || turnInput != 0)
        {
            transform.rotation = Quaternion.LookRotation(movement);
        }

        if(movement != Vector3.zero)
        {
            lastMove = movement;//ultima pos
        }

        movement.y = GravityCalc();

        rollInput = Input.GetKey(KeyCode.Space);

        if (rollInput && !isRolling && rollCDT <= 0f)
        {
            //roll movement
            isRolling = true;
            rollTime = rollDist;
            rollCDT = rollCD;

            rollMovement = lastMove;
            if (rollMovement == Vector3.zero)
            {
                rollMovement = transform.forward;
            }

            animator.SetBool("isRolling", true);

        }

        if (rollCDT > 0f)
        {
            rollCDT -= Time.deltaTime;
        }

        if (isRolling)
        {
            //SpawnCloud();
            dashCloud.Play();
            controller.Move(rollMovement * rollSpeed * Time.deltaTime);
            rollMovement.y = GravityCalc();
            rollTime -= Time.deltaTime;
            if (rollTime <= 0)
            {
                isRolling = false;
                animator.SetBool("isRolling", false);
            }

            return;
        }

        if (!cSpawn.isShooting)
        {
            controller.Move(movement * playerSpeed * Time.deltaTime);          

        }
        
 

        //Inputs para animator
        if (moveInput != 0 || turnInput != 0){
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning",false);
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
        animator.SetBool("isAttacking", cSpawn.isShooting);
        
    }
}
