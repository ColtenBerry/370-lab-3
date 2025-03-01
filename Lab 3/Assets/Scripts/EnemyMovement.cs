using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 5.0f;
    private Rigidbody2D rigidbody2D;
    private SpriteRenderer spriteRenderer;


    private bool isGoingLeft = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collision!");
        if (collision.collider.gameObject.name == "Player")
        {
            Debug.Log("hit Player!");
            GameManager.Instance.OnDeath();
        }
        else if (collision.collider.gameObject.name != "Ground Layer")
        {
            Debug.Log("Hit " + collision.collider.gameObject.name);
            isGoingLeft = !isGoingLeft;
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }
    }



    void FixedUpdate()
    {
        if (isGoingLeft)
        {
            Debug.Log("going left");
            rigidbody2D.linearVelocity = new Vector2(-1 * speed, rigidbody2D.linearVelocity.y);

        }
        else if (!isGoingLeft)
        {
            Debug.Log("Going right");
            rigidbody2D.linearVelocity = new Vector2(1 * speed, rigidbody2D.linearVelocity.y);

        }
    }
}
