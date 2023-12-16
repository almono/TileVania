using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 7f;
    [SerializeField] float jumpPower = 15f;

    Vector2 moveInput;
    Rigidbody2D playerBody;
    Animator playerAnimation;
    CapsuleCollider2D capsuleCollider;

    // Start is called before the first frame update
    void Start()
    {
        playerBody = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        FlipSprite();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        // if player is not touching the ground then dont allow jump
        if(!capsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }

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
}
