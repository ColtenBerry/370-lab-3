using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;

    public PhysicsMaterial2D smoothMaterial;

    public PhysicsMaterial2D wallJumpMaterial;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    float horizontal;

    public float runSpeed = 5f;
    private bool canMove;
    private bool m_Grounded = true;
    private bool wasGroundedLastFrame = true;  
    private float groundCheckBufferTime = 0.05f; 
    private float lastGroundTime = 0f;

    public UnityEvent OnLandEvent;

    public int carrotsCollected = 0;

    private int jumpsRemaining;

    private bool isTouchingWall;

    private bool canWallJump;

    public LayerMask obstacles;


    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        canMove = true;
        jumpsRemaining = 1;  
        if (OnLandEvent == null)
        {
            OnLandEvent = new UnityEvent();
        }
        OnLandEvent.AddListener(Landed);
    }

    void Update()
    {
        if (isTouchingWall)
        {
            Debug.Log("Touching Wall");
        }
        horizontal = Input.GetAxisRaw("Horizontal");
        if (horizontal < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (horizontal > 0)
        {
            spriteRenderer.flipX = false;
        }
        animator.SetFloat("Horizontal", horizontal);

        if (Input.GetKeyDown("space"))
        {
            if (canWallJump)
            {
                Debug.Log("Wall Jumping!");
                rigidbody2D.AddForce(Vector2.up * 350 * rigidbody2D.mass);
                jumpsRemaining = 1;
                animator.SetBool("IsJumping", true);
                m_Grounded = false;
            }
            else if (jumpsRemaining > 0)
            {
                jumpsRemaining--;
                rigidbody2D.AddForce(Vector2.up * 350 * rigidbody2D.mass);
                animator.SetBool("IsJumping", true);
                m_Grounded = false;  
            }
        }
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            rigidbody2D.linearVelocity = new Vector2(horizontal * runSpeed, rigidbody2D.linearVelocity.y);
        }

        bool wasGrounded = m_Grounded;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.2f, LayerMask.GetMask("Jumpable Ground"));
        if (colliders.Length == 0)
        {
            m_Grounded = false;
            lastGroundTime = Time.time;
        }

        isTouchingWall = Physics2D.OverlapCircle(transform.position + new Vector3(0.3f * (spriteRenderer.flipX ? -1 : 1), 0), 0.2f, LayerMask.GetMask("Jumpable Ground"));

        canWallJump = (carrotsCollected >= 2) && isTouchingWall;


        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {

                if (Time.time - lastGroundTime > groundCheckBufferTime)
                {
                    m_Grounded = true;
                }
            }
        }

        
        if (!m_Grounded && wasGrounded)
        {
            animator.SetBool("IsJumping", true);
        }

        
        if (m_Grounded && !wasGroundedLastFrame)
        {
            OnLandEvent.Invoke();
        }

        wasGroundedLastFrame = m_Grounded; 
    }

    public void enableMovement()
    {
        canMove = true;
    }

    public void disableMovement()
    {
        canMove = false;
    }

    public void Landed()
    {
        animator.SetBool("IsJumping", false);
        Debug.Log("Carrots Collected: " + carrotsCollected);

        if (carrotsCollected >= 1)
        {
            jumpsRemaining = 2;
        }
        else
        {
            jumpsRemaining = 1;
        }
    }

    public void CollectCarrot()
    {
        carrotsCollected++;
        Debug.Log("Collected Carrot! Total: " + carrotsCollected);
        jumpsRemaining = 2;
        if (carrotsCollected == 2)
        {
            Debug.Log("Wall Jump Unlocked");
            GetComponent<Collider2D>().sharedMaterial = wallJumpMaterial;

        }
    }
}