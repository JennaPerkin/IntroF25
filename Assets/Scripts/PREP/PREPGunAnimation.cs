using UnityEngine;
using System.Collections;

public class ExampleClass : MonoBehaviour
{
    public Animation anim;

    void Start()
    {
        anim = GetComponent<Animation>();
    }
}