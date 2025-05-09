using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightController : PlayerBaseController 
{

    public static KnightController Instance { get; private set; }

    [SerializeField] private Collider2D attackHitbox;// ���� ���� ������ Trigger Collider
    [SerializeField] private LayerMask attackableLayers;// ������ ��� ���̾�(Obstacle_Knight, Enemy ��)

    [SerializeField] private float attackDuration = 0.1f;// ��Ʈ�ڽ� Ȱ�� �ð�(��)
    private void Awake()
    {
        base.Awake();// PlayerBaseController�� Awake ����
        Instance = this;
        attackHitbox.enabled = false;// �ʱ갪���� ��Ʈ�ڽ� ��Ȱ��ȭ

    }

    void Update() /// �� ������ ȣ��: �Է� ���� �� �̵�, ����, ���� ó�� �� �θ� Update ȣ��� �ִϸ��̼� ����
    {

        moveInput = InputManager.Instance.GetKnightMovement();

        if (InputManager.Instance.GetKnightJump())
            Jump();

        if (InputManager.Instance.GetKnightAttack()) //���� �Է� �� �ִϸ��̼� Ʈ���� �� ��Ʈ�ڽ� Ȱ��ȭ
        {
            Attack();
           StartCoroutine(EnableHitbox());
        }

        base.Update(); //  PlayerBaseController.Update() ���� (Move(), HandleAnimation())
    }
    private IEnumerator EnableHitbox()/// ���� �ð� ���� ��Ʈ�ڽ��� Ȱ��ȭ�� �� ��Ȱ��ȭ�մϴ�.
    {
        attackHitbox.enabled = true;
        yield return new WaitForSeconds(attackDuration);
        attackHitbox.enabled = false;
    }

    // Trigger ������� �浹 ����
    private void OnTriggerEnter2D(Collider2D other)
    {
        // ���̾� ���͸�
        if (((1 << other.gameObject.layer) & attackableLayers) != 0)// ���̾� ���͸�: attackableLayers�� ���Ե� ���̾����� Ȯ��
        {
            if (other.TryGetComponent<IDamageable>(out var dmg))// IDamageable ����ü�� ������ ��� ������ ������
            {
                dmg.TakeDamage(1);
            }
        }
    }
}
