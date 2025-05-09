using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightController : PlayerBaseController 
{

    public static KnightController Instance { get; private set; }

    [SerializeField] private Collider2D attackHitbox;
    [SerializeField] private LayerMask attackableLayers;

    [SerializeField] private float attackDuration = 0.1f;
    private void Awake()
    {
        base.Awake();
        Instance = this;
        attackHitbox.enabled = false;

    }

    void Update()
    {
     
        moveInput = InputManager.Instance.GetKnightMovement();

        if (InputManager.Instance.GetKnightJump())
            Jump();

        if (InputManager.Instance.GetKnightAttack())
        {
            Attack();
           StartCoroutine(EnableHitbox());
        }

        base.Update();
    }
    private IEnumerator EnableHitbox()
    {
        attackHitbox.enabled = true;
        yield return new WaitForSeconds(attackDuration);
        attackHitbox.enabled = false;
    }

    // Trigger 방식으로 충돌 감지
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 레이어 필터링
        if (((1 << other.gameObject.layer) & attackableLayers) != 0)
        {
            if (other.TryGetComponent<IDamageable>(out var dmg))
            {
                dmg.TakeDamage(1);
            }
        }
    }
}
