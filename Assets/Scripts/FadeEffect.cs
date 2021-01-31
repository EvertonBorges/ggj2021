using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class FadeEffect : MonoBehaviour
{

    [Header("Configuration")]
    [SerializeField]
    private bool startActive = false;
    
    [Header("Fade Controls")]
    [Range(0.25f, 5f)] [SerializeField] private float fadeInTime = 1f;
    [Range(0.25f, 5f)] [SerializeField] private float fadeOutTime = 1f;

    [Header("Alpha Controls")]
    [SerializeField, Range(0f, 1f)] private float alphaStart = 0f;
    [SerializeField, Range(0f, 1f)] private float alphaEnd = 1f;

    private bool _isShowing = false;
    private bool _isFading = false;
    public bool IsShowing => _isShowing;
    
    private CanvasGroup canvas = null;
    private Coroutine _effectCoroutine = null;

    void Awake() 
    {
        canvas = GetComponent<CanvasGroup>();

        canvas.alpha = startActive ? alphaEnd : alphaStart;
        _isShowing = startActive;
        gameObject.SetActive(startActive);
    }

    public void Effect(bool fadeIn = true, Action callback = null)
    {
        if (!_isFading)
        {
            if (canvas == null)
                canvas = GetComponent<CanvasGroup>();

            if (_effectCoroutine != null) 
                StopCoroutine(_effectCoroutine);

            gameObject.SetActive(true);
            _effectCoroutine = StartCoroutine(EffectCoroutine(fadeIn, callback));
        }
    }

    private IEnumerator EffectCoroutine(bool fadeIn, Action callback = null)
    {
        _isFading = true;
        canvas.interactable = true;

        float startAlpha = fadeIn ? alphaStart : alphaEnd;
        float endAlpha = fadeIn ? alphaEnd : alphaStart;

        if (canvas.alpha > 0f && canvas.alpha < 1f)
            startAlpha = canvas.alpha;
        
        float animationTime = fadeIn ? fadeInTime : fadeOutTime;
        float currentTime = 0f;

        while(currentTime < animationTime)
        {
            currentTime += Time.deltaTime;
            float proportionTime = currentTime / animationTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, proportionTime);
            canvas.alpha = alpha;

            yield return null;
        }

        if (!fadeIn) 
        {
            canvas.interactable = false;
            gameObject.SetActive(false);
        }
        canvas.alpha = endAlpha;
        _isShowing = fadeIn;
        _isFading = false;
        callback?.Invoke();
        yield return null;
    }

}
