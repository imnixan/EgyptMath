using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneControl : MonoBehaviour
{
    public void RestartGame()
    {
        SceneManager.LoadScene("Pyramids");
    }

    public void ToMenu()
    {
        SceneManager.LoadSceneAsync("Menu");
    }
}
