using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
public class SettingUIController : MonoBehaviour
{
    public GameObject settingsPanel;

    public void ToggleSettingsPanel()
    {
        Debug.Log("Setting 버튼 눌림");
        if (settingsPanel != null)
            settingsPanel.SetActive(!settingsPanel.activeSelf);
        else
            Debug.LogWarning("settingsPanel이 연결되지 않았습니다!");
    }

    public void GoToMainTitle()
    {
        SceneManager.LoadScene("MainScene");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleSettingsPanel();
        }
    }
}

