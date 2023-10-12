using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenRotator : MonoBehaviour
{
    void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        StartCoroutine(Rotate());
    }

    IEnumerator Rotate(){
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Menu");
    }
   
}
