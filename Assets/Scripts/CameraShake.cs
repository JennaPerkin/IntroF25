using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance;
    private Vector3 originPos;
    private Vector3 returnPos;
    public bool Shaking;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        originPos = transform.localPosition;
    }

    public void ShakeCamera(float duration, float intensity)
    {
        StartCoroutine(CamShake(duration, intensity));
    }

    IEnumerator CamShake(float duration, float intensity)
    {
        float elapsed = 0;
        returnPos = transform.localPosition;
        Debug.Log("START POS:");
        Debug.Log(originPos);

        while (elapsed < duration)
        {
            float xPos = Random.Range(-intensity, intensity);
            float yPos = Random.Range(-intensity, intensity);

            transform.localPosition = originPos + new Vector3(xPos, yPos, 0);

            elapsed += Time.deltaTime;

            Shaking = true;

            yield return new WaitForEndOfFrame();
        }

        Shaking = false;
        transform.localPosition = returnPos;
        Debug.Log("END POS:");
        Debug.Log(originPos);
    }
}
