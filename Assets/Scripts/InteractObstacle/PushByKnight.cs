using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushByKnight : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private bool isTouchingKnight = false;
    private bool isTouchingGround = false;

    // Knight ���̾� ����
    [SerializeField] private string knightLayerName = "Knight"; // Inspector���� Knight ���̾� �̸� ����
    private int knightLayerNumber; // Knight ���̾� ��ȣ

    // Ground ���̾� ����
    [SerializeField] private string groundLayerName = "Ground"; // Inspector���� Ground ���̾� �̸� ����
    private int groundLayerNumber; // Ground ���̾� ��ȣ

    [SerializeField] private float pushForce = 3.0f; // �и��� �� ����
    [SerializeField] private float gravityScale = 1.0f; // �߷� ũ��

    private Transform knightTransform; // �浹�� Knight�� Transform
    private float checkGroundRadius = 0.2f; // �ٴ� üũ �ݰ�

    [SerializeField] private Transform groundCheck; // �ٴ� üũ ��ġ (Inspector���� ����)

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.bodyType = RigidbodyType2D.Kinematic;

        // Knight ���̾� ��ȣ ��������
        knightLayerNumber = LayerMask.NameToLayer(knightLayerName);

        // Ground ���̾� ��ȣ ��������
        groundLayerNumber = LayerMask.NameToLayer(groundLayerName);

        // groundCheck�� ������ ���� ������Ʈ�� ��ġ���� �ణ �Ʒ��� ����
        if (groundCheck == null)
        {
            GameObject checkObj = new GameObject("GroundCheck");
            checkObj.transform.parent = transform;
            checkObj.transform.localPosition = new Vector3(0, -0.5f, 0); // ������Ʈ �ϴܿ� ��ġ
            groundCheck = checkObj.transform;
            Debug.Log("GroundCheck ��ü �ڵ� ������");
        }

        // ���� �� ����� ���� ���
        Debug.Log($"PushByKnight �ʱ�ȭ��. ��ü �̸�: {gameObject.name}, ���̾�: {gameObject.layer}");
        Debug.Log($"Knight ���̾� �̸�: {knightLayerName}, ��ȣ: {knightLayerNumber}");
        Debug.Log($"Ground ���̾� �̸�: {groundLayerName}, ��ȣ: {groundLayerNumber}");
    }

    private void FixedUpdate()
    {
        // �ٴ� üũ
        CheckGround();

        // Knight�� �浹 ���̸� ���� ����
        if (isTouchingKnight && knightTransform != null)
        {
            // �浹 �߿��� Dynamic���� ����
            if (_rigidbody.bodyType != RigidbodyType2D.Dynamic)
            {
                _rigidbody.bodyType = RigidbodyType2D.Dynamic;
                _rigidbody.gravityScale = gravityScale; // �߷� ����
                Debug.Log("Rigidbody Ÿ�� ����: Dynamic (Knight �浹)");
            }

            // Knight�� ��ġ���� ������Ʈ���� ���� ���� ���
            Vector2 pushDirection = (transform.position - knightTransform.position).normalized;

            // �� ����
            _rigidbody.AddForce(pushDirection * pushForce, ForceMode2D.Force);
            Debug.Log($"�б� �� ����: {pushDirection * pushForce}, ���� �ӵ�: {_rigidbody.velocity}");
        }
        else if (!isTouchingKnight)
        {
            // Knight�� �浹�� ������
            if (_rigidbody.bodyType == RigidbodyType2D.Kinematic)
            {
                // �̹� Kinematic�̸� �ƹ��͵� ���� ����
                return;
            }

            if (isTouchingGround)
            {
                // �ٴڰ� ��������� Kinematic���� ����
                _rigidbody.velocity = Vector2.zero;
                _rigidbody.angularVelocity = 0f;
                _rigidbody.bodyType = RigidbodyType2D.Kinematic;
                Debug.Log("Rigidbody Ÿ�� ����: Kinematic (�ٴ� ����)");
            }
            else
            {
                // �ٴڰ� ������� ������ �߷� ����
                _rigidbody.bodyType = RigidbodyType2D.Dynamic;
                _rigidbody.gravityScale = gravityScale;
                Debug.Log("�߷� ���� �� (���߿� ����)");
            }
        }
    }

    private void CheckGround()
    {
        // �ٴ� üũ�� ���� ���� ���� �˻�
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, checkGroundRadius);

        // ���� ���� ����
        bool wasGrounded = isTouchingGround;
        isTouchingGround = false;

        // ����� ��� �ݶ��̴��� Ȯ��
        foreach (Collider2D col in colliders)
        {
            if (col.gameObject != gameObject &&
                (col.gameObject.layer == groundLayerNumber || col.gameObject.CompareTag("Ground")))
            {
                isTouchingGround = true;
                break;
            }
        }

        // �ٴ� ���� ��ȭ �� �α� ���
        if (wasGrounded != isTouchingGround)
        {
            Debug.Log($"�ٴ� ���� ���� ����: {isTouchingGround}");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"�浹 �߻�: {collision.gameObject.name}, ���̾�: {collision.gameObject.layer}");

        // Knight Ȯ��
        if (collision.gameObject.name.Contains("Knight") ||
            collision.gameObject.layer == knightLayerNumber)
        {
            Debug.Log("Knight�� �浹 ����!");
            isTouchingKnight = true;
            knightTransform = collision.transform;
        }

        // Ground Ȯ��
        if (collision.gameObject.name.Contains("Ground") ||
            collision.gameObject.layer == groundLayerNumber ||
            collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Ground�� �浹 ����!");
            isTouchingGround = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // Knight Ȯ��
        if (collision.gameObject.name.Contains("Knight") ||
            collision.gameObject.layer == knightLayerNumber)
        {
            isTouchingKnight = true;
            knightTransform = collision.transform;
        }

        // Ground Ȯ��
        if (collision.gameObject.name.Contains("Ground") ||
            collision.gameObject.layer == groundLayerNumber ||
            collision.gameObject.CompareTag("Ground"))
        {
            isTouchingGround = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log($"�浹 ����: {collision.gameObject.name}");

        // Knight Ȯ��
        if (collision.gameObject.name.Contains("Knight") ||
            collision.gameObject.layer == knightLayerNumber)
        {
            Debug.Log("Knight�� �浹 ����!");
            isTouchingKnight = false;
            knightTransform = null;
        }

        // Ground Ȯ��
        if (collision.gameObject.name.Contains("Ground") ||
            collision.gameObject.layer == groundLayerNumber ||
            collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Ground�� �浹 ����!");
            // ���� �������� �ʰ� CheckGround���� ó���ϵ��� ��
            // isTouchingGround = false;
        }
    }

    // Gizmos�� �ٴ� üũ ���� �ð�ȭ
    private void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = isTouchingGround ? Color.green : Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, checkGroundRadius);
        }
    }
}