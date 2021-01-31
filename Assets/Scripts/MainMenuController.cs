using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenuController : MonoBehaviour
{

    [SerializeField] private FadeEffect _uiBg = null;
    [SerializeField] private FadeEffect _uiCredits = null;

    private StandaloneInputModule _inputModule = null;

    void Awake()
    {
        _inputModule = GameObject.FindObjectOfType<StandaloneInputModule>();
        _inputModule.enabled = false;
    }

    void Start()
    {
        _uiBg.Effect(false, () => {
            _inputModule.enabled = true;
        });
    }

    public void Play()
    {
        _uiBg.Effect(true, () => {
            UiSfxController.Instance?.UiSelect(transform.position);
            SceneManager.LoadScene(Scenes.Gameplay.ToString());
        });
    }
    
    public void ShowCredits()
    {
        UiSfxController.Instance?.UiSelect(transform.position);
        _uiCredits.Effect(true);
    }

    public void HideCredits()
    {
        UiSfxController.Instance?.UiSelect(transform.position);
        _uiCredits.Effect(false);
    }

    public void Exit()
    {
        UiSfxController.Instance?.UiSelect(transform.position);
        StartCoroutine(WaitToExit());

        IEnumerator WaitToExit()
        {
            yield return new WaitForSeconds(0.25f);
    #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
    #else
            Application.Quit();
    #endif
        }
    }


}
