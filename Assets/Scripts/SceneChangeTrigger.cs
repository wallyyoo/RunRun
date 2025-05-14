using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeTrigger : MonoBehaviour

// 닿으면 다음 씬으로 넘어가는 스크립트
{
    [SerializeField] private string nextSceneName = "Tutorial_2";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Tag 가 Player 인 사람에게 작동 
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}

