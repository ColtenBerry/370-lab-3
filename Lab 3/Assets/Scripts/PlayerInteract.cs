using UnityEngine;

public class PlayerInteract : MonoBehaviour
{

    private float interactDistance = 2.0f;
    bool inConversation;

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

        if (inConversation)
        {
            Debug.Log("Skipping Line");
            GameManager.Instance.SkipLine();
        }
        else
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


            RaycastHit2D npc_interact_hit = Physics2D.CircleCast(transform.position, interactDistance, Vector2.right, 0, LayerMask.GetMask("NPC"));
            if (npc_interact_hit)
            {
                if (npc_interact_hit.collider.gameObject.TryGetComponent(out NPC npc))
                {
                    Debug.Log("Hit something (NPC)");
                    if (npc_interact_hit.collider.gameObject.name == "NPC")
                    {
                        print("Hit NPC");
                        GameManager.Instance.StartDialogue(npc.dialogueAsset.dialogue, npc.StartPosition, npc.npcName, 2);
                    }
                }
            }

        }
    }

    void JoinConversation()
    {
        inConversation = true;
    }

    void LeaveConversation()
    {
        inConversation = false;
    }

    private void OnEnable()
    {
        GameManager.OnDialogueStarted += JoinConversation;
        GameManager.OnDialogueEnded += LeaveConversation;
    }

    private void OnDisable()
    {
        GameManager.OnDialogueStarted -= JoinConversation;
        GameManager.OnDialogueEnded -= LeaveConversation;
    }
}
