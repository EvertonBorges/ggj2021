using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class UiSfxController : Singleton<UiSfxController>
{
    
    private AudioSource _audioSource = null;
    
    [SerializeField] private AudioClip _uiSelect = null;

    protected override void Init()
    {
        DontDestroyOnLoad(gameObject);
        _audioSource = GetComponent<AudioSource>();
    }

    public void UiSelect(Vector3 position)
    {
        transform.position = position;
        PlayAudio(_uiSelect);
    }

    private void PlayAudio(AudioClip _audio)
    {
        _audioSource.Stop();
        _audioSource.clip = _audio;
        _audioSource.Play();
    }

}
