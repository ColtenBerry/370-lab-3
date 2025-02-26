using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D rigidbody2D;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    float horizontal;

    public float runSpeed = 5f;
    private bool m_Grounded;

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
    }

    // Update is called once per frame
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
        // if (Input.GetKeyDown("space") && !animator.GetBool("jump"))
        if (Input.GetKeyDown("space"))
        {
            rigidbody2D.AddForce(Vector2.up * 500 * rigidbody2D.mass);
            animator.SetBool("IsJumping", true);
        }


    }

    void FixedUpdate()
    {
        rigidbody2D.linearVelocity = new Vector2(horizontal * runSpeed, rigidbody2D.linearVelocity.y);

        bool wasGrounded = m_Grounded;

        m_Grounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.2f);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                Debug.Log("Collision!");
                m_Grounded = true;
                if (!wasGrounded)
                {
                    OnLandEvent.Invoke();
                    Debug.Log("Land Event Invoked!");
                }
            }
        }
    }

    public void Landed()
    {
        animator.SetBool("IsJumping", false);
    }

}