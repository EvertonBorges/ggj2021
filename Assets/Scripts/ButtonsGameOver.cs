using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsGameOver : MonoBehaviour
{

    [SerializeField] private FadeEffect _uiBg = null;

    public void MainMenu()
    {
        UiSfxController.Instance?.UiSelect(transform.position);
        _uiBg.Effect(true, () => {
            SceneManager.LoadScene(Scenes.MainMenu.ToString());
        });
    }

    public void Restart()
    {
        UiSfxController.Instance?.UiSelect(transform.position);
        _uiBg.Effect(true, () => {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        });
    }

    public void Exit()
    {
        UiSfxController.Instance?.UiSelect(transform.position);
        _uiBg.Effect(true, () => {
            StartCoroutine(WaitToExit());

            IEnumerator WaitToExit()
            {
                yield return new WaitForSeconds(0.1f);
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
            }
        });
    }

}
