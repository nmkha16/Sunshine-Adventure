using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb2d;
    BoxCollider2D boxCollider;
    public Animator animator;

    [Header("Background")]
    public Renderer backgroundRenderer;

    [Header("Movement")]
    [SerializeField] public float moveSpeed;
    [SerializeField] public float jumpForce;
    public float gravity;
    [SerializeField] private LayerMask groundMask;
    private bool left,right;
    public bool isGrounded;
    private bool canDoubleJump;
    private float lastXPosition; // check if moving in static position - prevent moving background

    public CoinPicker endMenuScript;
    public Transform originLevel;

    private float threshold = 500f;

    void Awake()
    {
        right = true;
        rb2d = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        endMenuScript = GetComponent<CoinPicker>();
        disableCursor(true);
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, .1f, groundMask);
        if (isGrounded) animator.SetBool("isJump", false);

        myInput();

        jump();
    }


    private void myInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            disableCursor(false);
            return;
        }

        // slower speed if in mid-air
        if (!isGrounded)
        {
            //first line is input to run
            //second line is auto endless run
            //rb2d.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed * 0.8f, rb2d.velocity.y);
            rb2d.velocity = new Vector2(1* moveSpeed * 0.8f, rb2d.velocity.y);
        }
        //else rb2d.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, rb2d.velocity.y);
        else rb2d.velocity = new Vector2(1 * moveSpeed * 0.8f, rb2d.velocity.y);
        if (rb2d.velocity.x < 0)
        {
            turnLeft();

            // background follow on player movement
            if (lastXPosition > rb2d.transform.position.x)
            {
                backgroundRenderer.material.mainTextureOffset += new Vector2(rb2d.velocity.x * 0.02f * Time.deltaTime, 0f); //background moving based on player speed
            }
        }
        else if (rb2d.velocity.x > 0)
        {
            turnRight();
            if (lastXPosition < rb2d.transform.position.x)
            {
                backgroundRenderer.material.mainTextureOffset += new Vector2(rb2d.velocity.x * 0.02f * Time.deltaTime, 0f); //background moving based on player speed
            }
        }
        animator.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x));
    }

    private void FixedUpdate()
    {
        lastXPosition = rb2d.transform.position.x; // prevent background moving if player moving in static position



        if (!isGrounded)
        {
            rb2d.AddForce(Physics.gravity*  gravity,ForceMode2D.Force);
        }
        
        if (transform.position.x > threshold)
        {
            spawnOrigin();
        }

        //preventOffscreen();
        fallOffWorld();


    }

    private void jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                animator.SetBool("isJump", true);
                rb2d.velocity = Vector2.up * jumpForce;
                canDoubleJump = true;
            }
            else if (!isGrounded && canDoubleJump)
            {
                rb2d.velocity = Vector2.up * (jumpForce-2f);
                canDoubleJump = false;
            }
        }
    }


    private void preventOffscreen()
    {
        Vector3 boundary = transform.position;
        Vector3 maxScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        if (transform.position.x <0 && transform.position.x <= -maxScreenBounds.x) // left screen width value in position x is negative
        {
            boundary.x = Mathf.Clamp(transform.position.x,-maxScreenBounds.x, -maxScreenBounds.x+1);
            transform.position = boundary;
        }

    }

    private void fallOffWorld()
    {
        
        //Vector3 maxScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        if (transform.position.y < -40f) // bottom screen width value in position y is negative 
                                        // -40f the value taken from debug
        {
            //Debug.Log(maxScreenBounds);
            endMenuScript.EndMenu();
        }
    }


    private void turnLeft()
    {
        if (left) return;
        transform.localScale = new Vector3(-transform.localScale.x,transform.localScale.y,transform.localScale.z);
        left = true;
        right = false;
    }
    private void turnRight()
    {
        if(right) return;
        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        left = false;
        right = true;
    }

    //position X = -17.2 and Position Y = -28.37792
    // must spawn origin to avoid player moving away from center and causing very high floating point may glitch the game
    private void spawnOrigin()
    {
        Transform newOriginLevel = Instantiate(originLevel,new Vector3(0f, -35.7f,0f),Quaternion.identity);
        transform.position = new Vector3(0f, -28.37792f, 0f);
    }

    private void disableCursor(bool condition)
    {
        if (condition)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
    }
}
