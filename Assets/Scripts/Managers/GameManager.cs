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
            // DontDestroyOnLoad(this.gameObject); // ���û���
        }
        else
        {
            Destroy(gameObject); // �ߺ� ����
        }
    }

    public void GameOver()
    {
        Debug.Log("���� ����!");
    }

    public void LoadNextLevel()
    {
        Debug.Log("���� ���� �ε�!");
    }

    public void EnemyDied()
    {
        Debug.Log("�� ��� ó��!");
    }
}
