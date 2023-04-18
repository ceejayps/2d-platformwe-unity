using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class playerMovement : MonoBehaviour

{
[SerializeField] private float speed;
[SerializeField] private float jumpForce;
 private float moveInput;
[SerializeField] private bool facingRight = true;

 [SerializeField]private bool isGrounded;
 public Transform groundCheck;
 public float checkRadius;
public LayerMask whatIsGround;

[SerializeField] private int extraJump;
[SerializeField] private int extraJumpValue;


[SerializeField] private float dashTime;
[SerializeField] private float MaxDashTime;

[SerializeField] private bool canDashLeft;
[SerializeField] private bool canDashRight;
[SerializeField] private float canDashTimer;




public float lastDirectionalKeyPressTime = 0f;
public KeyCode lastDirectionalKeyCode = KeyCode.None;




public float dashSpeed = 10f; // the speed of the dash
    public float dashDuration = 0.5f; // how long the dash lasts
    public float dashCooldown = 1f; // how long the dash cooldown lasts
    public KeyCode dashKey = KeyCode.Space; // the key to trigger the dash
    private float dashTimer; // keeps track of how long the dash has been active
    private float cooldownTimer; // keeps track of how long until the dash can be used again
    private Vector2 dashDirection; // the direction of the dash
    


private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    
    {
        rb = GetComponent<Rigidbody2D>();
        dashTime = MaxDashTime;
    }

    // Update is called once per frame
    void FixedUpdate()
{
    if(!Input.GetKey(dashKey) ){
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        moveInput = Input.GetAxis("Horizontal");
        rb.velocity =new Vector2(moveInput*speed, rb.velocity.y);

        if(facingRight == false && moveInput > 0){
            flip();
        } else if(facingRight == true && moveInput < 0){
            flip();
        }
    }
    
    dash();
}

void dash(){
    // If the dash isn't currently active and the cooldown is over
    if (dashTimer <= 0 && cooldownTimer <= 0)
    {
        // Get the input direction
        float inputDirection = Input.GetAxisRaw("Horizontal");

        // Set the dash direction to the player's facing direction
        dashDirection = new Vector2(inputDirection, 0).normalized;

        // If the dash key is pressed and there's an input direction
        if (Input.GetKeyDown(dashKey) && dashDirection.magnitude > 0)
        {
            // Start the dash
            dashTimer = dashDuration;
            cooldownTimer = dashCooldown;

            // Disable the Rigidbody2D's gravity while dashing
            rb.gravityScale = 0;

            // Apply a force in the dash direction
            rb.AddForce(dashDirection * dashSpeed, ForceMode2D.Impulse);
        }
    }
    else
    {
        // If the dash is active, decrement the timer
        if (dashTimer > 0)
        {
            dashTimer -= Time.deltaTime;

            // If the dash timer is over, re-enable gravity
            if (dashTimer <= 0)
            {
                rb.gravityScale = 1;
            }
        }

        // If the cooldown is active, decrement the timer
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }
    }
}



    void flip(){
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    
    

    void Update()
    {

        if(isGrounded == true){
            extraJump = extraJumpValue;
        }
        if(Input.GetKeyDown(KeyCode.UpArrow) && extraJump > 0 || Input.GetKeyDown(KeyCode.W)    && extraJump > 0){
            rb.velocity = Vector2.up * jumpForce;
            extraJump --;
        } else if(Input.GetKeyDown(KeyCode.UpArrow) && extraJump == 0 && isGrounded == true || Input.GetKeyDown(KeyCode.W) && extraJump == 0 && isGrounded == true){
            rb.velocity = Vector2.up * jumpForce;
        }

        if(Input.GetKeyUp(KeyCode.LeftArrow)|| Input.GetKeyUp(KeyCode.A) ){
            canDashLeft = true;
            canDashRight = false;
            canDashTimer = 1;
        }

        if(Input.GetKeyUp(KeyCode.RightArrow)|| Input.GetKeyUp(KeyCode.D) ){
            canDashRight = true;
            canDashLeft = false;
            canDashTimer = 1;
            
        }

        if(canDashTimer >0){
canDashTimer -= Time.deltaTime;
        }


        dash();

    }

}
