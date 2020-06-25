using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    
    public void LoadSceneByNumber(int _sceneNumber)
    {
        Application.LoadLevel(_sceneNumber);
    }

    public void QuitGame()
    {
        Debug.Log("quitting game");
        Application.Quit();
    }
}
