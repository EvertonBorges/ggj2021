using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinTrigger : MonoBehaviour
{

    private const string TIME_RECORD = "TimeRecord";

    [SerializeField] private FadeEffect _uiWin = null;
    [SerializeField] private GameObject _uiNewRecord = null;

    [SerializeField] private Text _txtTime = null;
    [SerializeField] private Text _txtBestTime = null;

    [SerializeField] private Button _firstSelection = null;
    
    private void Win()
    {
        SaveRecord();
        ShowWinScreen();
        Destroy(gameObject);
    }

    private void SaveRecord()
    {
        _uiNewRecord.gameObject.SetActive(false);
        var timeLevel = Time.time - GameManager.Instance.StartTime;
        if (PlayerPrefs.HasKey(TIME_RECORD))
        {
            var timeRecord = PlayerPrefs.GetFloat(TIME_RECORD);

            if (timeLevel < timeRecord)
            {
                _uiNewRecord.gameObject.SetActive(true);
                timeRecord = timeLevel;
                PlayerPrefs.SetFloat(TIME_RECORD, timeLevel);
            }

            _txtTime.text = timeLevel.ToString("N2");
            _txtBestTime.text = timeRecord.ToString("N2");
        }
        else
            PlayerPrefs.SetFloat(TIME_RECORD, timeLevel);
    }

    private void ShowWinScreen()
    {
        FirstPersonMovement.Instance.SetBlockMovement(true);
        _uiWin.Effect(true, () => {
            _firstSelection.Select();
        });
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.Player.ToString()))
            Win();
    }

}
