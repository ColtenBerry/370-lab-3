using UnityEngine;

public class FollowCam : MonoBehaviour
{

    public GameObject playerSprite;
    public float boundaryPercent;
    public float easing;

    private float leftBound;
    private float rightBound;
    private float upBound;
    private float downBound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        leftBound = boundaryPercent * Camera.main.pixelWidth;
        rightBound = Camera.main.pixelWidth - leftBound;
        downBound = boundaryPercent * Camera.main.pixelHeight;
        upBound = Camera.main.pixelHeight - downBound;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (playerSprite)
        {
            Vector3 spriteLocation = Camera.main.WorldToScreenPoint(playerSprite.transform.position);
            Vector3 pos = transform.position;

            if (spriteLocation.x < leftBound)
            {
                pos.x -= leftBound - spriteLocation.x;

            }

            else if (spriteLocation.x > rightBound)
            {
                pos.x += spriteLocation.x - rightBound;
            }
            if (spriteLocation.y < downBound)
            {
                pos.y -= downBound - spriteLocation.y;
            }
            else if (spriteLocation.y > upBound)
            {
                pos.y += spriteLocation.y - upBound;
            }

            pos = Vector3.Lerp(transform.position, pos, easing);
            transform.position = pos;
        }
    }
}
