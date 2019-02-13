using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    //config
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2(25f, 25f);

    //state
    [SerializeField] bool isAlive = true;

    //Cache
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    BoxCollider2D myFeetCollider;
    CapsuleCollider2D myBodyCollider;
    float gravityScale;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        gravityScale = myRigidbody.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive) return;

        Run();
        FlipSprite();
        Jump();
        ClimbLadder();
        Die();
    }

    void Run()
    {
        var controlThrow = CrossPlatformInputManager.GetAxis("Horizontal");

        var playerVelocity = new Vector2(controlThrow * runSpeed, myRigidbody.velocity.y);

        myRigidbody.velocity = playerVelocity;

        var playerHorizantalMove = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

        myAnimator.SetBool("Running", playerHorizantalMove);
    }

    void Jump()
    {
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }

        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            var jumpVelocityToAdd = new Vector2(0f, jumpSpeed);

            myRigidbody.velocity += jumpVelocityToAdd;
        }
    }

    void ClimbLadder()
    {
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            myAnimator.SetBool("Climbing", false);

            myRigidbody.gravityScale = gravityScale;
            return;
        }

        var controlThrow = CrossPlatformInputManager.GetAxis("Vertical");

        var playerVelocity = new Vector2(myRigidbody.velocity.x, controlThrow * climbSpeed);

        myRigidbody.velocity = playerVelocity;
        myRigidbody.gravityScale = 0f;

        var playerVerticalMove = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;

        myAnimator.SetBool("Climbing", playerVerticalMove);
    }

    void Die()
    {
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy","Hazard")))
        {
            isAlive = false;
            myAnimator.SetTrigger("Dying");
            myRigidbody.velocity = deathKick;

            var gameSession = FindObjectOfType<GameSession>();
            gameSession.ProcessPlayerDeath();
        }
    }

    void FlipSprite()
    {
        var playerHorizantalMove = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

        if (playerHorizantalMove)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1f);
        }
    }
}
