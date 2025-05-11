using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // DontDestroyOnLoad(this.gameObject); // 선택사항
        }
        else
        {
            Destroy(gameObject); // 중복 방지
        }
    }

    public void GameOver()
    {
        Debug.Log("게임 오버!");
    }

    public void LoadNextLevel()
    {
        Debug.Log("다음 레벨 로드!");
    }

    public void EnemyDied()
    {
        Debug.Log("적 사망 처리!");
    }
}
