using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene("Main Level Scene");
    }

    public void ExitScene()
    {
        Application.Quit();
    }
}
