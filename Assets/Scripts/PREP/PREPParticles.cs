using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PREPParticles : MonoBehaviour
{
    public ParticleSystem burst;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            burst.Play();
        }
    }
}
