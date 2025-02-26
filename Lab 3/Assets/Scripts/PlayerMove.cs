using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D rigidbody2D;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    float horizontal;

    public float runSpeed = 5f;
    private bool m_Grounded = true;

    public UnityEvent OnLandEvent;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        if (OnLandEvent == null)
        {
            OnLandEvent = new UnityEvent();
        }
        OnLandEvent.AddListener(Landed);
        previousY = transform.position.y;
        wasFalling = false;
    }

    // Update is called once per frame
    private float previousY;
    private bool wasFalling;
    void Update()
    {

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
        if (Input.GetKeyDown("space") && !animator.GetBool("IsJumping"))
        {
            rigidbody2D.AddForce(Vector2.up * 300 * rigidbody2D.mass);
            animator.SetBool("IsJumping", true);
        }

        // //determine if falling
        // float distance = Math.Abs(Math.Abs(previousY) - Math.Abs(transform.position.y));
        // Debug.Log("Distance: " + distance);
        // if (distance > .1 && wasFalling == false)
        // {
        //     animator.SetBool("IsJumping", true);
        //     Debug.Log("Falling");
        //     wasFalling = true;
        // }
        // previousY = transform.position.y;

    }

    void FixedUpdate()
    {
        rigidbody2D.linearVelocity = new Vector2(horizontal * runSpeed, rigidbody2D.linearVelocity.y);

        bool wasGrounded = m_Grounded;


        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.2f, LayerMask.GetMask("Jumpable Ground"));
        if (wasGrounded && colliders.Length == 0)
        {
            m_Grounded = false;
        }
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                Debug.Log("Hit Ground!");
                m_Grounded = true;
                if (!wasGrounded)
                {
                    OnLandEvent.Invoke();
                    Debug.Log("Land Event Invoked!");
                }
            }
        }
        if (!m_Grounded && wasGrounded)
        {
            Debug.Log("Called");
            animator.SetBool("IsJumping", true);

        }
    }

    public void Landed()
    {
        animator.SetBool("IsJumping", false);
        m_Grounded = true;
    }

}