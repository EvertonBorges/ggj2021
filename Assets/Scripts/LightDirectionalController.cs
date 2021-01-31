using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDirectionalController : MonoBehaviour
{
    
    [SerializeField] private Light _directionLight = null;

    [Range(0.5f, 2f)] [SerializeField] private float _timeTransition = 1f;

    [Range(0.5f, 2f)] [SerializeField] private float _minIntensity = 0.5f;
    [Range(0.5f, 2f)] [SerializeField] private float _maxIntensity = 2f;

    void Awake()
    {
        StartCoroutine(Fade(true));
    }

    IEnumerator Fade(bool normal)
    {
        float currentTime = 0f;

        float minIntensity = normal ? _minIntensity : _maxIntensity;
        float maxIntensity = normal ? _maxIntensity : _minIntensity;

        while (currentTime < _timeTransition)
        {
            currentTime += Time.deltaTime;
            float proportion = currentTime / _timeTransition;
            float intensity = Mathf.Lerp(minIntensity, maxIntensity, proportion);
            _directionLight.intensity = intensity;

            yield return null;
        }

        _directionLight.intensity = maxIntensity;
        yield return null;

        yield return new WaitForSeconds(4f);

        StartCoroutine(Fade(!normal));
    }

}
