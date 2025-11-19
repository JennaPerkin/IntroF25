using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UITracking : MonoBehaviour
{
    public Projectiles projectileScript;
    [SerializeField] TextMeshProUGUI projectileCounter;

    public RigiPlayerMovement collectables;
    [SerializeField] TextMeshProUGUI collectablesCounter;

    // Update is called once per frame
    void Update()
    {
        projectileCounter.text = ("Shots: " + projectileScript.totalShots.ToString());
        collectablesCounter.text = ("Orbs: " + collectables.collectables.ToString());
    }
}
