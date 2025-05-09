using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightController : PlayerBaseController 
{

    public static KnightController Instance { get; private set; }

    [SerializeField] private Collider2D attackHitbox;// 근접 공격 범위용 Trigger Collider
    [SerializeField] private LayerMask attackableLayers;// 공격할 대상 레이어(Obstacle_Knight, Enemy 등)

    [SerializeField] private float attackDuration = 0.1f;// 히트박스 활성 시간(초)
    private void Awake()
    {
        base.Awake();// PlayerBaseController의 Awake 실행
        Instance = this;
        attackHitbox.enabled = false;// 초깃값으로 히트박스 비활성화

    }

    void Update() /// 매 프레임 호출: 입력 감지 → 이동, 점프, 공격 처리 → 부모 Update 호출로 애니메이션 갱신
    {

        moveInput = InputManager.Instance.GetKnightMovement();

        if (InputManager.Instance.GetKnightJump())
            Jump();

        if (InputManager.Instance.GetKnightAttack()) //공격 입력 시 애니메이션 트리거 후 히트박스 활성화
        {
            Attack();
           StartCoroutine(EnableHitbox());
        }

        base.Update(); //  PlayerBaseController.Update() 실행 (Move(), HandleAnimation())
    }
    private IEnumerator EnableHitbox()/// 일정 시간 동안 히트박스를 활성화한 뒤 비활성화합니다.
    {
        attackHitbox.enabled = true;
        yield return new WaitForSeconds(attackDuration);
        attackHitbox.enabled = false;
    }

    // Trigger 방식으로 충돌 감지
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 레이어 필터링
        if (((1 << other.gameObject.layer) & attackableLayers) != 0)// 레이어 필터링: attackableLayers에 포함된 레이어인지 확인
        {
            if (other.TryGetComponent<IDamageable>(out var dmg))// IDamageable 구현체가 있으면 즉시 데미지 입히기
            {
                dmg.TakeDamage(1);
            }
        }
    }
}
