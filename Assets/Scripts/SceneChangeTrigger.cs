using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeTrigger : MonoBehaviour

// ������ ���� ������ �Ѿ�� ��ũ��Ʈ
{
    [SerializeField] private string nextSceneName = "Tutorial_2";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Tag �� Player �� ������� �۵� 
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}

