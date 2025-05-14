using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
public class SettingUIController : MonoBehaviour
{
    public GameObject settingsPanel;

    public void ToggleSettingsPanel()
    {
        Debug.Log("Setting ��ư ����");
        if (settingsPanel != null)
            settingsPanel.SetActive(!settingsPanel.activeSelf);
        else
            Debug.LogWarning("settingsPanel�� ������� �ʾҽ��ϴ�!");
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

