using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;
    float horizontal;
    public float runSpeed = 5f;
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
    }

    void FixedUpdate()
    {
        rigidbody2D.linearVelocity = new Vector2(horizontal * runSpeed, rigidbody2D.linearVelocity.y);
    }
}
