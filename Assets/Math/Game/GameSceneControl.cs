using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneControl : MonoBehaviour
{
    private void Awake()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Pyramids");
    }

    public void ToMenu()
    {
        SceneManager.LoadSceneAsync("Menu");
    }
}
