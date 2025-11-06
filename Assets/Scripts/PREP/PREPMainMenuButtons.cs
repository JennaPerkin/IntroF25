using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PREPMainMenuButtons : MonoBehaviour
{
    public void StartButton()
    {
        SceneManager.LoadSceneAsync("3D Landscape Prep");
    }
}
