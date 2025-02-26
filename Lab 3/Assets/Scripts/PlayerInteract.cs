using UnityEngine;

public class PlayerInteract : MonoBehaviour
{

    private float interactDistance = 2.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    void Interact()
    {
        Debug.Log("Interact");
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, interactDistance, Vector2.right, 0, LayerMask.GetMask("Interact"));
        if (hit)
        {
            Debug.Log("Hit Something");
            if (hit.collider.gameObject.name == "Carrot")
            {
                print("Hit Carrot");
                Destroy(hit.collider.gameObject);
            }
        }

    }
}
