using UnityEngine;

public class Trap2D : MonoBehaviour
{
    // �÷��̾�(�׽�Ʈ�� ĳ����)�� Ʈ���ſ� ������ ��
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Tag�� Player�� �ص״ٸ�
        if (other.CompareTag("Player"))
        {
            Debug.Log("�׾����ϴ�");
        }
    }
}
