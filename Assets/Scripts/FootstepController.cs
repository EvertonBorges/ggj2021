using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FootstepController : MonoBehaviour
{
    
    private AudioSource _audioSource = null;
    [SerializeField] private AudioClip[] _audioFootsteps = null;

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayFootstep()
    {
        if (_audioFootsteps != null && _audioFootsteps.Length > 0 && !_audioSource.isPlaying)
        {
            _audioSource.clip = _audioFootsteps[Random.Range(0, _audioFootsteps.Length)];
            _audioSource.Play();
        }
    }

}
