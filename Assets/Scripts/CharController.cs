using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour

{
    //************************************************************************
    [Header("Character Controller")]
    public CharacterController controller;

    public Transform cam;
    public float speed = 6f;
    public float turnSmoothTime = 0.1f;
    [SerializeField] float turnSmoothVelocity;

    //************************************************************************
    [Header("Ground Check")]

    public float gravity = -2f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    Vector3 velocity;
    bool isGrounded;
    //************************************************************************
    [Header("Jump")]
    public float jumpHeight;
    public AudioSource CoinAudioSource;

    //************************************************************************
    // private GameManager gameManager;
    // [Header("Spawner")]
    public GameObject Player;
    public Transform srtpos;
    private GameObject PlayerInstance;
    public int Playerlives;
    [SerializeField] Transform respawnPoint;
    private void Start()
    {

        Cursor.lockState = CursorLockMode.Locked;
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "coin")
        {
            Debug.Log("true");
            CoinAudioSource.Play();
            Destroy(other.gameObject);

        }

    }
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        
        PlayerSideMovement();
        PlayerJump();
    }
    void PlayerSideMovement()
    {
        Cursor.lockState = CursorLockMode.Locked;
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;
        if (direction.magnitude >= 0.1f)
        {

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float smoothangle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, smoothangle, 0f);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
    }
    void PlayerJump()
    {

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }


}











