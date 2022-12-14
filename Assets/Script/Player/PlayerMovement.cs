using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //---------------------- PROPIEDADES SERIALIZADAS ----------------------
    [SerializeField][Range(1f, 2000f)] private float movementForce = 3f;
    [SerializeField][Range(1f, 2000f)] private float jumpForce = 40f;

    [Range(1f, 200f)] private float velocidadMaxima = 5f;
    [Range(0f, 100f)] public float sensibilidadEnX = 90f;
    [Range(0f, 100f)] public float sensibilidadEnY = 30f;

    private float pivotvelocidadMaxima;
    private float pivotsensibilidadX;
    private float pivotsensibilidadY;


    [SerializeField][Range(1f, 200f)] private float jumpDelay = 1f;
    [SerializeField][Range(1f, 5f)] public float velMult = 1.5f;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private GameObject _1P_target;
    [SerializeField] private GameObject _3P_target;

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

    private bool pause = false;

    private void OnEnable()
    {
        UIManager.Pause += OnPause;
    }

    private void OnDisable()
    {
        UIManager.Pause -= OnPause;
    }
    void Start()
    {
        pivotvelocidadMaxima = velocidadMaxima;
        pivotsensibilidadX = sensibilidadEnX;
        pivotsensibilidadY = sensibilidadEnY;
        playerrb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        if (!pause)
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
                //stepSound.PlayDelayed(0.033f);
            }

            /*if (Input.GetKeyDown(KeyCode.W))
            {
                //stepSound.PlayOneShot(stepSound.clip, 0.7F);
                //stepSound.PlayDelayed(0.033f);
            } 
            
            if (Input.GetKeyUp(KeyCode.W)) 
            { 
                //stepSound.Pause(); 
            }*/

            if (Input.GetKey(KeyCode.S))
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

            playerrb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
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
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, sensibilidadEnX * Time.deltaTime);

        CameraAxisY = Input.GetAxis("Mouse Y");
        _1P_target.transform.position += new Vector3(0, CameraAxisY * sensibilidadEnY, 0);
    }

    public void OnPause(bool pauses)
    {
        pause = pauses;
        // if (pauses)
        // {
        //     velocidadMaxima = 0;
        //     sensibilidad = 0;
        // }
        // else
        // {
        //     velocidadMaxima = pivotvelocidadMaxima;
        //     sensibilidad = pivotsensibilidad;
        // }
    }
}
