using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class SettingUIController : MonoBehaviour
{
    public GameObject settingsPanel;
    public Button settingButton;  // ���� ��ư ���� ���� �߰�

    private void Awake()
    {
        Debug.Log("SettingUIController Awake ȣ���");

        // ��ư ã�� �õ� (���� ���)
        if (settingButton == null)
        {
            settingButton = GameObject.Find("SettingButton")?.GetComponent<Button>();
            if (settingButton != null)
                Debug.Log("SettingButton�� ã�ҽ��ϴ�");
            else
                Debug.LogError("SettingButton�� ã�� �� �����ϴ�!");
        }

        // �г� ã�� �õ� (���� ���)
        if (settingsPanel == null)
        {
            settingsPanel = GameObject.Find("Setting Panel");
            if (settingsPanel != null)
                Debug.Log("Setting Panel�� ã�ҽ��ϴ�");
            else
                Debug.LogError("Setting Panel�� ã�� �� �����ϴ�!");
        }
    }

    private void Start()
    {
        Debug.Log("SettingUIController Start ȣ���");

        // �г� �ʱ� ���� ����
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
            Debug.Log("Setting Panel�� ��Ȱ��ȭ�߽��ϴ�");
        }

        // ��ư�� ������ ���� ����
        if (settingButton != null)
        {
            // ���� ������ ����
            settingButton.onClick.RemoveAllListeners();

            // �� ������ �߰�
            settingButton.onClick.AddListener(() =>
            {
                Debug.Log("��ư Ŭ�� �̺�Ʈ �߻�!");
                ToggleSettingsPanel();
            });
            Debug.Log("SettingButton�� �����ʸ� �����߽��ϴ�");
        }
    }

    public void ToggleSettingsPanel()
    {
        Debug.Log("ToggleSettingsPanel �Լ��� ȣ��Ǿ����ϴ�");

        if (settingsPanel != null)
        {
            bool newState = !settingsPanel.activeSelf;
            Debug.Log("�г� ���� ��ȯ: " + (!newState) + " -> " + newState);
            settingsPanel.SetActive(newState);
            Debug.Log("�г� ���°� ����Ǿ����ϴ�: " + (newState ? "ǥ��" : "����"));
        }
        else
        {
            Debug.LogError("settingsPanel ������ �����ϴ�! Inspector���� �������ּ���!");
        }
    }

    public void GoToMainTitle()
    {
        Debug.Log("���� Ÿ��Ʋ�� ���ư��ϴ�");
        SceneManager.LoadScene("StartScene");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape Ű�� ���Ƚ��ϴ�");
            ToggleSettingsPanel();
        }
    }
}