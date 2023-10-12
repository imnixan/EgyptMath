using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class Settings : MonoBehaviour
{
    public TextMeshProUGUI sound, vibro;
    void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        UpdateText();
    }

    private void UpdateText(){
        sound.text = PlayerPrefs.GetInt("sound") == 0? "Sound on" : "Sound off";
        vibro.text = PlayerPrefs.GetInt("vibro") == 0? "Vibro on" : "Vibro off";
    }

   public void Menu(){
        SceneManager.LoadScene("Menu");
   }

   public void Change(string changed){
        PlayerPrefs.SetInt(changed, PlayerPrefs.GetInt(changed) == 1? 0 : 1);
        PlayerPrefs.Save();
        UpdateText();
   }
}
