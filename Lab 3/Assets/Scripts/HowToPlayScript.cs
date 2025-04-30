using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HowToPlayScript : MonoBehaviour
{
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static HowToPlayScript Instance { get; private set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject player;
    public TextMeshProUGUI text;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x > -1 && player.transform.position.x < 2)
        {
            text.text = "Press Space to Jump";
        }
        else if (player.transform.position.x > 2)
        {
            text.text = "Press E to eat the Carrrot";
        }
    }

    public void eatCarrot(GameObject player, Animator animator, GameObject carrot)
    {
        animator.SetBool("IsEating", true);
        player.GetComponent<PlayerMovement>().disableMovement();
        ParticleSystem p = carrot.GetComponent<CarrotScript>().particleSystem;
        p.Play();
        StartCoroutine(eatCarrotRoutine(player, animator, carrot));
    }

    IEnumerator eatCarrotRoutine(GameObject player, Animator animator, GameObject carrot)
    {
        yield return new WaitForSeconds(5);
        animator.SetBool("IsEating", false);
        Destroy(carrot);

        SceneManager.LoadScene("Main Menu");

    }

}
