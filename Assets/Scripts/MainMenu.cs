using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string sceneName;
    public void StartGame()
    {
        SceneManager.LoadScene(sceneName); 
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void LoadCredits()
    {
        SceneManager.LoadScene("Credits");
    }
}
