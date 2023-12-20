using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 7f;
    [SerializeField] float jumpPower = 20f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] Vector2 deathKnockback = new Vector2(10f, 10f);

    private float baseGravity = 8f;
    [SerializeField] bool isAlive = true;

    Vector2 moveInput;
    Rigidbody2D playerBody;
    Animator playerAnimation;
    CapsuleCollider2D bodyCollider;
    BoxCollider2D feetCollider;

    // Start is called before the first frame update
    void Start()
    {
        playerBody = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<Animator>();
        bodyCollider = GetComponent<CapsuleCollider2D>();
        feetCollider = GetComponent<BoxCollider2D>();

        playerBody.gravityScale = baseGravity;
    }

    // Update is called once per frame
    void Update()
    {
        if(isAlive)
        {
            Run();
            FlipSprite();
            ClimbLadder();
            Die();
        }
    }

    void OnMove(InputValue value)
    {
        if(!isAlive) { return; }
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (!isAlive) { return; }

        // if player is not touching the ground then dont allow jump
        if (!feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }

        if(value.isPressed)
        {
            playerBody.velocity += new Vector2(playerBody.velocity.x, jumpPower);
        }
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, playerBody.velocity.y);
        playerBody.velocity = playerVelocity;

        // change animation to running
        bool playerIsMovingHorizontally = Mathf.Abs(playerBody.velocity.x) > Mathf.Epsilon;
        playerAnimation.SetBool("isRunning", playerIsMovingHorizontally);
    }

    // moving left or right
    void FlipSprite()
    {
        bool playerIsMovingHorizontally = Mathf.Abs(playerBody.velocity.x) > Mathf.Epsilon; // epsilion is basically 0

        if(playerIsMovingHorizontally)
        {
            transform.localScale = new Vector2(Mathf.Sign(playerBody.velocity.x), 1f);
        }
    }

    void ClimbLadder()
    {
        // if player is not touching the climbable then dont allow climbing
        if (!feetCollider.IsTouchingLayers(LayerMask.GetMask("Climb"))) 
        {
            playerBody.gravityScale = baseGravity;
            playerAnimation.SetBool("isClimbing", false);
            return; 
        }

        Vector2 climbVelocity = new Vector2(playerBody.velocity.x, moveInput.y * climbSpeed);
        playerBody.velocity = climbVelocity;
        playerBody.gravityScale = 0f;

        bool playerIsMovingVertically = Mathf.Abs(playerBody.velocity.y) > Mathf.Epsilon;
        playerAnimation.SetBool("isClimbing", playerIsMovingVertically);
    }

    void Die()
    {
        if(bodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy")))
        {
            isAlive = false;
            playerAnimation.SetTrigger("isKilled");
            playerBody.velocity = deathKnockback;
        }
    }
}
