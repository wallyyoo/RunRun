using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightController : PlayerBaseController
{
    [Header("Attack Settings")]
    [SerializeField] private Collider2D attackHitbox;
    [SerializeField] private LayerMask attackableLayers;
    [SerializeField] private float attackDuration = 0.1f;

    private KnightAnimationHandler knightAnimationHandler;
    private KnightAttack knightAttack;

    protected override void Awake()
    {
        base.Awake();

        // KnightAttack 스크립트 자동 할당
        knightAttack = GetComponent<KnightAttack>();
        if (knightAttack == null)
            Debug.LogError("KnightAttack 스크립트가 Knight 오브젝트에 없습니다!");

        // 애니메이션 핸들러 자동 할당
        knightAnimationHandler = GetComponent<KnightAnimationHandler>();
        if (knightAnimationHandler == null)
            Debug.LogError(" KnightAnimationHandler 스크립트가 Knight 오브젝트에 없습니다!");

        if (attackHitbox != null)
            attackHitbox.enabled = false;
    }

    protected override void Update()
    {
        moveInput = InputManager.Instance.GetKnightMovement();

        if (InputManager.Instance.GetKnightJump() && isGrounded)
        {
            Jump();
        }

        if (InputManager.Instance.GetKnightAttack())
        {
            if (isGrounded)
            {
                Attack();

                if (knightAttack != null)
                    knightAttack.Attack();

                StartCoroutine(EnableHitbox());
            }
        }

        Move();
        base.Update();
    }

    public override void Move()
    {
        base.Move();
        knightAnimationHandler?.Move(moveInput);
    }

    public override void Jump()
    {
        Debug.Log("점프 호출됨");
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, jumpForce);
        knightAnimationHandler?.Jump();
    }

    public override void Die()
    {
        knightAnimationHandler?.Die();
        Destroy(gameObject);
    }

    private IEnumerator EnableHitbox()
    {
        if (attackHitbox != null)
        {
            attackHitbox.enabled = true;
            yield return new WaitForSeconds(attackDuration);
            attackHitbox.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        int otherLayer = other.gameObject.layer;
        string otherTag = other.tag;

        if (((1 << otherLayer) & attackableLayers) != 0 && otherTag == "Enemy")
        {
            if (other.TryGetComponent<IDamageable>(out var dmg))
            {
                dmg.TakeDamage(1);
            }
        }
        else if (otherTag == "Obstacle")
        {
            if (other.attachedRigidbody != null)
            {
                Vector2 pushDir = (other.transform.position - transform.position).normalized;
                other.attachedRigidbody.AddForce(pushDir * 200f);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & groundLayer.value) != 0)
        {
            isGrounded = true;
            animator.SetBool("IsJump", false);
        }
    }

    public float GetCurrentSpeed()
    {
        return Mathf.Abs(_rigidbody.velocity.x);
    }

    public float GetMoveDirection()
    {
        return Mathf.Sign(_rigidbody.velocity.x);
    }
}
