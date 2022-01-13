using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioStart : MonoBehaviour
{
    [SerializeField]
    private AudioClip _clip;

    private void Start()
    {
        AudioManager.Instance.SetAudio(_clip);
        StartCoroutine(AudioManager.Instance.AudioFadeIn(4));
    }
}
