using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyHealthDisplay : MonoBehaviour
{

    public EnemyAI script;
    [SerializeField] TextMeshPro healthDisplay;
    public Transform displayDirection;
    public Transform cam;

    // Update is called once per frame
    void Update()
    {
        healthDisplay.text = script.health.ToString();

        displayDirection.rotation = cam.rotation;
    }
}
