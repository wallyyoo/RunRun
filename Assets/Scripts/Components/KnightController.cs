using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class KnightController : PlayerBaseController
{
    [SerializeField] private Collider2D attackHitbox;
    [SerializeField] private LayerMask attackableLayers;
    [SerializeField] private float attackDuration = 0.1f;

    private KnightAnimationHandler knightAnimationHandler;
    private KnightAttack knightAttack;

    private int jumpCount = 0;
    [SerializeField] private int maxJumps = 2;

    protected override void Awake()
    {
        base.Awake();
        knightAttack = GetComponent<KnightAttack>();
        knightAnimationHandler = GetComponentInChildren<KnightAnimationHandler>();
        

        if (attackHitbox != null)
            attackHitbox.enabled = false;
    }

    protected override void Update()
    {
        moveInput = InputManager.Instance.GetKnightMovement();

        if (InputManager.Instance.GetKnightJump() && jumpCount < maxJumps)
        {
            Jump();
        }

        if (InputManager.Instance.GetKnightAttack())
        {
            if (isGrounded)
            {
                Attack();
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
        Debug.Log("점프 호출됨 / count: " + jumpCount);
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, jumpForce);
        knightAnimationHandler?.Jump();
        jumpCount++;
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
            jumpCount = 0;
            animator.SetBool("IsJump", false);
        }
    }

    // 박스 밀기 관련한 로직

  public float GetCurrentSpeed()
    {
        return Mathf.Abs(_rigidbody.velocity.x);
    }

    public float GetMoveDirection()
    {
        return Mathf.Sign(_rigidbody.velocity.x); // or moveInput if preferred
    }
}


