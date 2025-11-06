using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PREPEnemyHealthDisplay : MonoBehaviour
{
    public PREPEnemyAI script;
    [SerializeField] TextMeshPro healthDisplay;
    public Transform displayDirection;
    public Transform cam;

    private void Update()
    {
        healthDisplay.text = script.health.ToString();

        displayDirection.rotation = cam.rotation;
    }
}
