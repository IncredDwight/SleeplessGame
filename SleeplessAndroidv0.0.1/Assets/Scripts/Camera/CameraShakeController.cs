using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeController : MonoBehaviour
{
    public static CameraShakeController instance;
    private float shakeTimeRemaining;
    private float shakePower;
    private float shakeFadeTime;
    private float rotationMultiplier = 4.5f;
    private float shakeRotation;

    private void Start()
    {
        instance = this;
    }

    private void LateUpdate()
    {
        Shake();
    }

    private void Shake()
    {
        if(shakeTimeRemaining > 0)
        {
            shakeTimeRemaining -= Time.deltaTime;
            float shakeX = Random.Range(-1f, 1f) * shakePower;
            float shakeY = Random.Range(-1f, 1f) * shakePower;
            transform.position += new Vector3(shakeX, shakeY);

            shakePower = Mathf.MoveTowards(shakePower, 0, shakeFadeTime * Time.deltaTime);
            shakeRotation = Mathf.MoveTowards(shakeRotation, 0, shakeFadeTime * Time.deltaTime * rotationMultiplier);
        }

        transform.rotation = Quaternion.Euler(0, 0, shakeRotation * Random.Range(-1f, 1f));
    }

    public void StartShake(float duration, float power)
    {
        shakeTimeRemaining = duration;
        shakePower = power;
        shakeFadeTime = power / duration;

        shakeRotation = power * rotationMultiplier;

    }
}
