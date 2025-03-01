using UnityEngine;

public class SpikeBehavior : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.name == "Player")
        {
            Debug.Log("Player Hit Spikes !!!"); //TODO make some kind of hit
            GameManager.Instance.OnDeath();
        }
    }
}
