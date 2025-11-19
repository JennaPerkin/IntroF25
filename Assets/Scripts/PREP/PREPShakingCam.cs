using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PREPShakingCam : MonoBehaviour
{
    public static PREPShakingCam instance;

    private Vector3 originPos;

    public bool Shaking;

    private void Start()
    {
        instance = this;
    }

    private void Update()
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

        transform.localPosition = originPos;
    }
}
