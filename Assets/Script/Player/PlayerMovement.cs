using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //---------------------- PROPIEDADES SERIALIZADAS ----------------------
    [SerializeField]
    [Range(1f, 2000f)]
    private float movementForce = 3f;

    [SerializeField]
    [Range(1f, 2000f)]
    private float jumpForce = 40f;

    [SerializeField]
    [Range(1f, 200f)]
    private float velocidadMaxima = 5f;

    [SerializeField]
    [Range(1f, 200f)]
    private float jumpDelay = 1f;
    [SerializeField]
    [Range(30f, 100f)]
    public float sensibilidad = 30f;

    [SerializeField]
    [Range(1f, 5f)]
    public float velMult = 1.5f;
    [SerializeField]
    private Animator playerAnimator;

    //---------------------- PROPIEDADES PUBLICAS ----------------------

    public bool CanJump { get => canJump; set => canJump = value; }
    public Rigidbody playerrb { get => myRigidbody; set => myRigidbody = value; }
    public float VelocidadMaxima { get => velocidadMaxima; set => velocidadMaxima = value; }
    
    //---------------------- PROPIEDADES PRIVADAS ----------------------
    private bool canJump = true;
    private bool inDelayJump = false;
    private float CameraAxisX = 0f;
    private float CameraAxisY = 0f;

    private Vector3 playerDirection;
    private Rigidbody myRigidbody;

    void Start()
    {
        playerrb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        RotatePlayer();

        bool forward = Input.GetKeyDown(KeyCode.W);
        bool back = Input.GetKeyDown(KeyCode.S);
        bool left = Input.GetKeyDown(KeyCode.A);
        bool right = Input.GetKeyDown(KeyCode.D);

        if (forward) playerAnimator.SetTrigger("adelante");
        if (back) playerAnimator.SetTrigger("atras");
        if (left) playerAnimator.SetTrigger("izquierda");
        if (right) playerAnimator.SetTrigger("derecha");

        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            if (!IsAnimation("Reposo")) playerAnimator.SetTrigger("Reposo");
        }

        playerDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            playerDirection += Vector3.forward;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            playerDirection += Vector3.back;
        }

        if (Input.GetKey(KeyCode.D))
        {
            playerDirection += Vector3.right;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            playerDirection += Vector3.left;
        }

        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            canJump = false;

        }
    }

    private void FixedUpdate()
    {
        if (playerDirection != Vector3.zero && playerrb.velocity.magnitude < VelocidadMaxima && !Input.GetKey(KeyCode.LeftShift))
        {

            playerrb.AddForce(transform.TransformDirection(playerDirection) * movementForce, ForceMode.Force);

        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            playerrb.AddForce(transform.TransformDirection(playerDirection) * movementForce * velMult, ForceMode.Force);
        }


        if (!canJump && !inDelayJump)
        {

            playerrb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            inDelayJump = true;
            Invoke("DelayNextJump", jumpDelay);
        }
    }

    private void DelayNextJump()
    {
        inDelayJump = false;
        canJump = true;
    }

    private bool IsAnimation(string animName)
    {
        return playerAnimator.GetCurrentAnimatorStateInfo(0).IsName(animName);
    }

    public void RotatePlayer()
    {
        CameraAxisX += Input.GetAxis("Mouse X");
        Quaternion newRotation = Quaternion.Euler(0, CameraAxisX, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, sensibilidad * Time.deltaTime);
    }
}
