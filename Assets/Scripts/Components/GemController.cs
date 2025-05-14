using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemController : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 90f;

    private void Update()
    {
        // 보석 회전 연출
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Player와 충돌했을 때만 작동
        if (other.CompareTag("Player"))
        {
            if (UIManager.Instance != null)
            {
                UIManager.Instance.AddScore(100);
            }

            Destroy(gameObject); // 자기 자신 삭제
        }
    }
}

