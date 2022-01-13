using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    private AudioSource _audioSource;

    private void Awake()
    {
        if (!GetComponent<AudioSource>())
            gameObject.AddComponent<AudioSource>();
        _audioSource = GetComponent<AudioSource>();
    }

    public void SetAudio(AudioClip audioClip)
    {
        _audioSource.clip = audioClip;
        _audioSource.Play();
    }

    public IEnumerator AudioFadeOut(float fadeTime)
    {
        float startVolume = _audioSource.volume;

        while (_audioSource.volume > 0)
        {
            _audioSource.volume -= startVolume * Time.deltaTime / fadeTime;

            yield return null;
        }

        //_audioSource.Stop();
        //_audioSource.volume = startVolume;
    }

    public IEnumerator AudioFadeIn(float fadeTime)
    {
        float startVolume = 0;

        while (_audioSource.volume < 0.5f)
        {
            _audioSource.volume += Time.deltaTime / fadeTime;

            yield return null;
        }

        //_audioSource.Stop();
        //_audioSource.volume = startVolume;
    }
}
