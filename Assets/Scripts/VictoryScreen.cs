using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScreen : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Victory();
        }
    }


    // Start is called before the first frame update
    public void Victory()
    {
        GUI_Controller.Instance.FadeIn();
        GUI_Controller.Instance.fadeInCallbackEvent += LoadVictoryScene;
    }

    void LoadVictoryScene()
    {
        SceneManager.LoadScene("WinMenu");
    }
}
