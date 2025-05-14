using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class WizardController : PlayerBaseController
{
    public static WizardController Instance { get; private set; }
    [SerializeField] private GameObject projectilePrefab;// �߻��� ����ü ������
    [SerializeField] private LayerMask projectileHitLayers;   // ����ü�� �浹�� ���̾�(Obstacle_Wizard, Enemy ��)
    private float fireCoolDown = 0.5f;
    private float fireTimer = 0f;
    protected override void Awake()
    {
        base.Awake();
        Instance = this;
    }

    protected override void Update()    /// �� ������ ȣ��: �Է� ���� �� �̵�, ����, ���� ó�� �� �θ� Update ȣ��� �ִϸ��̼� ����
    {
        if (isDead) return;

        moveInput = InputManager.Instance.GetWizardMovement();// 1) �̵� �Է� �޾� moveInput ����

        if (InputManager.Instance.GetWizardJump())// 2) ���� �Է� �� Jump() ȣ�� (�ִϸ��̼� Ʈ����)
        {
            if (isGrounded == true)
            {
                Jump();

                base.Update();
            }
        }

        if (InputManager.Instance.GetWizardAttackDown())// 3) ���� �Է� �� ����ü �߻�
        {
            if (isGrounded == true)
            {
                if (fireTimer >= fireCoolDown)
                {
                    FireProjectile();
                    fireTimer = 0f;
                }
            }

        }
        fireTimer += Time.deltaTime;
        base.Update();// 4) PlayerBaseController.Update() ���� (Move(), HandleAnimation())

    }

    // ����ü �߻�Ǵ� Ÿ�̹� ����
    private void FireProjectile()
    {
        Debug.Log("FireProjectile() called");
        Attack();
        StartCoroutine(DelayedFire(0.5f));
    
    }

    private IEnumerator DelayedFire(float delay)
    {
        yield return new WaitForSeconds(delay);
        InstanceProjectile();
    }

    private void InstanceProjectile()
    {
        // �ٶ󺸴� ���� ��� (scale.x�� ������ ����, ����� ������)
        float direction = _spriteRenderer.flipX ? -1f : 1f;

        Vector3 instanciatePosition = new Vector3(transform.position.x + (direction * 2.0f), transform.position.y + 0.5f, transform.position.z);

        // �߻�ü ����
        var proj = Instantiate(projectilePrefab, instanciatePosition, Quaternion.identity);

        // ���� ����
        var magic = proj.GetComponent<MagicProjectile>();
        if (magic != null)
        {
            magic.hitLayers = projectileHitLayers;
            magic.SetDirection(direction); // �ٶ󺸴� ���� �Ѱ��ֱ�
        }
    }
}
