using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;

public class WizardController : PlayerBaseController
{
    public static WizardController Instance { get; private set; }
   [SerializeField]private GameObject projectilePrefab;// �߻��� ����ü ������
    [SerializeField] private LayerMask projectileHitLayers;   // ����ü�� �浹�� ���̾�(Obstacle_Wizard, Enemy ��)

    private void Awake()
    {
        base.Awake();
        Instance = this;
    }

    private void Update()    /// �� ������ ȣ��: �Է� ���� �� �̵�, ����, ���� ó�� �� �θ� Update ȣ��� �ִϸ��̼� ����
    {
        moveInput = InputManager.Instance.GetWizardMovement();// 1) �̵� �Է� �޾� moveInput ����

        if (InputManager.Instance.GetWizardJump())// 2) ���� �Է� �� Jump() ȣ�� (�ִϸ��̼� Ʈ����)
            Jump();

        if(InputManager.Instance.GetWizardAttack())// 3) ���� �Է� �� ����ü �߻�
        {
           
            FireProjectile();
        }
        base.Update();// 4) PlayerBaseController.Update() ���� (Move(), HandleAnimation())
    }

    private void FireProjectile()
    {
        Attack();

        // �ٶ󺸴� ���� ��� (scale.x�� ������ ����, ����� ������)
        float direction = transform.localScale.x > 0 ? 1f : -1f;

        // �߻�ü ����
        var proj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        // ���� ����
        var magic = proj.GetComponent<MagicProjectile>();
        if (magic != null)
        {
            magic.hitLayers = projectileHitLayers;
            magic.SetDirection(direction); // �ٶ󺸴� ���� �Ѱ��ֱ�
        }
    }



}
