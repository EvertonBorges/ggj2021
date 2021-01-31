using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SfxContoller : Singleton<SfxContoller>
{

    private AudioSource _audioSource = null;
    
    [SerializeField] private AudioClip _pickKey = null;
    [SerializeField] private AudioClip _doorUnlock = null;

    protected override void Init()
    {
        DontDestroyOnLoad(gameObject);
        _audioSource = GetComponent<AudioSource>();
    }

    public void PickKey(Vector3 position)
    {
        transform.position = position;
        PlayAudio(_pickKey);
    }

    public void DoorUnlock(Vector3 position)
    {
        transform.position = position;
        PlayAudio(_doorUnlock);
    }

    private void PlayAudio(AudioClip _audio)
    {
        _audioSource.Stop();
        _audioSource.clip = _audio;
        _audioSource.Play();
    }

}
