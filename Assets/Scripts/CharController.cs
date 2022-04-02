using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharController : MonoBehaviour

{
    //************************************************************************
    [Header("Character Controller")]
    public CharacterController controller;

    public Transform cam;
    public float speed = 12f;
    public float turnSmoothTime = 0.1f;
    [SerializeField] float turnSmoothVelocity;

    //************************************************************************
    [Header("Ground Check")]

    public float gravity = -10f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    Vector3 velocity;
    bool isGrounded;
    //************************************************************************
    [Header("Jump")]
    public float jumpHeight = 3f;
    public AudioSource CoinAudioSource;

    //************************************************************************
    // private GameManager gameManager;
    [Header("Spawner")]
    public GameObject Player;

    private GameObject PlayerInstance;
    public int Playerlives ;
    public Transform srtpos;
    [SerializeField] Transform respawnPoint;
    public Text PlayerLives;
    public Text Score;
    public bool win=false;
    private void Start()
    {

        Cursor.lockState = CursorLockMode.Locked;
        srtpos = GetComponent<Transform>();

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "coin")
        {
            Debug.Log(other.tag);
            CoinAudioSource.Play();
            Destroy(other.gameObject);
        }

        if (other.tag == "Lava")
        {
            if (Playerlives >= 0)
            {
              Playerlives--;
            srtpos.transform.position = respawnPoint.transform.position;
            }
        }
        if(other.tag=="Win" )
        {
     PlayerLives.text = "Winner "+  Score.text;
      Destroy(other.gameObject);
        win=true;
        }
    }
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        PlayerSideMovement();
        PlayerJump();
        PlayerHealth();

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
            velocity.y = -10f;
        }
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

    }
    void PlayerHealth()
    {
        if(win==false){
        if (Playerlives >= 0)
        {
            PlayerLives.text = "Lives left- " + Playerlives;
        }
        else
        {
            PlayerLives.text = "Lives - 0";
        }
        if (Playerlives < 0)
        {
            PlayerLives.text = "GameOver";
        }
        }
    }

}











