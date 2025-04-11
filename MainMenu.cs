using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Rules()
    {
        SceneManager.LoadSceneAsync("Rules");
    }
    public void Back()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }
}
