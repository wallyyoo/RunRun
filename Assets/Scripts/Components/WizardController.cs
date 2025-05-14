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
    [SerializeField] private GameObject projectilePrefab;// 발사할 투사체 프리팹
    [SerializeField] private LayerMask projectileHitLayers;   // 투사체가 충돌할 레이어(Obstacle_Wizard, Enemy 등)
    private float fireCoolDown = 0.5f;
    private float fireTimer = 0f;
    protected override void Awake()
    {
        base.Awake();
        Instance = this;
    }

    protected override void Update()    /// 매 프레임 호출: 입력 감지 → 이동, 점프, 공격 처리 → 부모 Update 호출로 애니메이션 갱신
    {
        if (isDead) return;

        moveInput = InputManager.Instance.GetWizardMovement();// 1) 이동 입력 받아 moveInput 설정

        if (InputManager.Instance.GetWizardJump())// 2) 점프 입력 시 Jump() 호출 (애니메이션 트리거)
        {
            if (isGrounded == true)
            {
                Jump();

                base.Update();
            }
        }

        if (InputManager.Instance.GetWizardAttackDown())// 3) 공격 입력 시 투사체 발사
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
        base.Update();// 4) PlayerBaseController.Update() 실행 (Move(), HandleAnimation())

    }

    // 투사체 발사되는 타이밍 조절
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
        // 바라보는 방향 계산 (scale.x가 음수면 왼쪽, 양수면 오른쪽)
        float direction = _spriteRenderer.flipX ? -1f : 1f;

        Vector3 instanciatePosition = new Vector3(transform.position.x + (direction * 2.0f), transform.position.y + 0.5f, transform.position.z);

        // 발사체 생성
        var proj = Instantiate(projectilePrefab, instanciatePosition, Quaternion.identity);

        // 방향 설정
        var magic = proj.GetComponent<MagicProjectile>();
        if (magic != null)
        {
            magic.hitLayers = projectileHitLayers;
            magic.SetDirection(direction); // 바라보는 방향 넘겨주기
        }
    }
}
