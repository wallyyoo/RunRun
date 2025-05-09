using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;

public class WizardController : PlayerBaseController
{
    public static WizardController Instance { get; private set; }
   [SerializeField]private GameObject projectilePrefab;// 발사할 투사체 프리팹
    [SerializeField] private LayerMask projectileHitLayers;   // 투사체가 충돌할 레이어(Obstacle_Wizard, Enemy 등)

    private void Awake()
    {
        base.Awake();
        Instance = this;
    }

    private void Update()    /// 매 프레임 호출: 입력 감지 → 이동, 점프, 공격 처리 → 부모 Update 호출로 애니메이션 갱신
    {
        moveInput = InputManager.Instance.GetWizardMovement();// 1) 이동 입력 받아 moveInput 설정

        if (InputManager.Instance.GetWizardJump())// 2) 점프 입력 시 Jump() 호출 (애니메이션 트리거)
            Jump();

        if(InputManager.Instance.GetWizardAttack())// 3) 공격 입력 시 투사체 발사
        {
           
            FireProjectile();
        }
        base.Update();// 4) PlayerBaseController.Update() 실행 (Move(), HandleAnimation())
    }

    private void FireProjectile()
    {
        Attack();

        // 바라보는 방향 계산 (scale.x가 음수면 왼쪽, 양수면 오른쪽)
        float direction = transform.localScale.x > 0 ? 1f : -1f;

        // 발사체 생성
        var proj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        // 방향 설정
        var magic = proj.GetComponent<MagicProjectile>();
        if (magic != null)
        {
            magic.hitLayers = projectileHitLayers;
            magic.SetDirection(direction); // 바라보는 방향 넘겨주기
        }
    }



}
