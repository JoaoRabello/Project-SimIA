using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{
    public void StartRandomWorld()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitSimulator()
    {
        Application.Quit();
    }
}
