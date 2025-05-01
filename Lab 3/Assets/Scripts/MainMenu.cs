using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void openGameScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void openTutorialScene()
    {
        SceneManager.LoadScene("HowToPlay");
    }
    public void openCreditsScene()
    {
        SceneManager.LoadScene("Credits"); 
    }
    public void openMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
    public void quitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

}
