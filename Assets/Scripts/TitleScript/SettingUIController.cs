using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class SettingUIController : MonoBehaviour
{
    public GameObject settingsPanel;
    public Button settingButton;  // 설정 버튼 직접 참조 추가

    private void Awake()
    {
        Debug.Log("SettingUIController Awake 호출됨");

        // 버튼 찾기 시도 (없을 경우)
        if (settingButton == null)
        {
            settingButton = GameObject.Find("SettingButton")?.GetComponent<Button>();
            if (settingButton != null)
                Debug.Log("SettingButton을 찾았습니다");
            else
                Debug.LogError("SettingButton을 찾을 수 없습니다!");
        }

        // 패널 찾기 시도 (없을 경우)
        if (settingsPanel == null)
        {
            settingsPanel = GameObject.Find("Setting Panel");
            if (settingsPanel != null)
                Debug.Log("Setting Panel을 찾았습니다");
            else
                Debug.LogError("Setting Panel을 찾을 수 없습니다!");
        }
    }

    private void Start()
    {
        Debug.Log("SettingUIController Start 호출됨");

        // 패널 초기 상태 설정
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
            Debug.Log("Setting Panel을 비활성화했습니다");
        }

        // 버튼에 리스너 직접 연결
        if (settingButton != null)
        {
            // 기존 리스너 제거
            settingButton.onClick.RemoveAllListeners();

            // 새 리스너 추가
            settingButton.onClick.AddListener(() =>
            {
                Debug.Log("버튼 클릭 이벤트 발생!");
                ToggleSettingsPanel();
            });
            Debug.Log("SettingButton에 리스너를 연결했습니다");
        }
    }

    public void ToggleSettingsPanel()
    {
        Debug.Log("ToggleSettingsPanel 함수가 호출되었습니다");

        if (settingsPanel != null)
        {
            bool newState = !settingsPanel.activeSelf;
            Debug.Log("패널 상태 전환: " + (!newState) + " -> " + newState);
            settingsPanel.SetActive(newState);
            Debug.Log("패널 상태가 변경되었습니다: " + (newState ? "표시" : "숨김"));
        }
        else
        {
            Debug.LogError("settingsPanel 참조가 없습니다! Inspector에서 설정해주세요!");
        }
    }

    public void GoToMainTitle()
    {
        Debug.Log("메인 타이틀로 돌아갑니다");
        SceneManager.LoadScene("StartScene");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape 키가 눌렸습니다");
            ToggleSettingsPanel();
        }
    }
}