using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Key : MonoBehaviour
{
    
    [SerializeField] private Image _interactImage = null;

    private KeyScriptable _scriptable = null;
    public KeyScriptable Scriptable => _scriptable;

    private bool _isShowingCanvas = false;

    void Awake() 
    {
        _interactImage.gameObject.SetActive(false);
    }

    public void Setup(KeyScriptable scriptable)
    {
        _scriptable = scriptable;
    }

    public void Show()
    {
        if (!_isShowingCanvas)
        {
            _isShowingCanvas = true;
            _interactImage.transform.position = transform.position + Vector3.up * 0.175f + Vector3.left * 0.05f;
            _interactImage.gameObject.SetActive(_isShowingCanvas);
        }
    }

    public void Hide()
    {
        if (_isShowingCanvas)
        {
            _isShowingCanvas = false;
            _interactImage.gameObject.SetActive(_isShowingCanvas);
        }
    }

    public void GetKey()
    {
        Hide();
        SfxContoller.Instance?.PickKey(transform.position);
        Destroy(gameObject);
    }

}
