using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {

    public void Shake(float duration, float intensity)
    {
        StartCoroutine(ScreenShake(duration, intensity));
    }

    private IEnumerator ScreenShake(float duration, float intensity)
    {
        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            yield return null;

            transform.position += new Vector3(Random.Range(-0.05f, 0.05f), Random.Range(-0.05f, 0.05f), 0f) * intensity;
        }
    }
}
