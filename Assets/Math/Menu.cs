using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Menu : MonoBehaviour
{
    public TextMeshProUGUI num;
    void Start(){
        num.text = GameRules.digits + "";
        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }
    public void Game(){
        SceneManager.LoadScene("Pyramids");
    }

    public void Settings(){
        SceneManager.LoadScene("Settings");
    }

    public void Exxxit(){
        Application.Quit();
    }

    public void ChangeDiff(){
        GameRules.digits = GameRules.digits == 4? 6 : 4;
        num.text = GameRules.digits + "";
    }
    
}
