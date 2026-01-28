using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{

    public GameObject player;
    public RigiPlayerMovement collectables;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == player && collectables.collectables >= 5)
        {
            SceneManager.LoadSceneAsync("Main Menu");
        }
    }
}
