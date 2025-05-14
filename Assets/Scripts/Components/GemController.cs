using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemController : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 90f;

    private void Update()
    {
        // ���� ȸ�� ����
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Player�� �浹���� ���� �۵�
        if (other.CompareTag("Player"))
        {
            if (UIManager.Instance != null)
            {
                UIManager.Instance.AddScore(100);
            }

            Destroy(gameObject); // �ڱ� �ڽ� ����
        }
    }
}

