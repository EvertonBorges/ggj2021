using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Key : MonoBehaviour
{
    
    [SerializeField] private Image _interactImage = null;

    private bool _isShowing = false;

    void Awake() 
    {
        _interactImage.gameObject.SetActive(false);
    }

    public void Show()
    {
        if (!_isShowing)
        {
            _isShowing = true;
            _interactImage.transform.position = transform.position + Vector3.up * 0.175f + Vector3.left * 0.05f;
            _interactImage.gameObject.SetActive(_isShowing);
        }
    }

    public void Hide()
    {
        if (_isShowing)
        {
            _isShowing = false;
            _interactImage.gameObject.SetActive(_isShowing);
        }
    }

}
