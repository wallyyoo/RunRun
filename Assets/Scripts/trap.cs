using UnityEngine;

public class Trap2D : MonoBehaviour
{
    // �÷��̾�(�׽�Ʈ�� ĳ����)�� Ʈ���ſ� ������ ��
    private void OnTriggerEnter2D(Collider2D Trap)
    {
        // Tag�� Player�� �ص״ٸ�
        if (Trap.CompareTag("Player"))
        {
            Debug.Log("�׾����ϴ�");
        }
    }
}
