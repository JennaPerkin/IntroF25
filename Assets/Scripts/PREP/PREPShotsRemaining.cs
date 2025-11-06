using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PREPShotsRemaining : MonoBehaviour
{
    public PREPProjectile script;
    [SerializeField] TextMeshProUGUI quantity;

    private void Update()
    {
        quantity.text = script.totalShots.ToString();
    }
}
