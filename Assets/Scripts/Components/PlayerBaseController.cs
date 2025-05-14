using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] protected float moveSpeed = 5f;
    [SerializeField] protected float jumpForce = 10f;

    protected Rigidbody2D _rigidbody;
    protected Animator animator;
    protected Vector2 moveInput;
    protected SpriteRenderer _spriteRenderer;
    [SerializeField] protected LayerMask groundLayer;
    protected bool isGrounded;

    protected WizardAnimationHandler wizardAnimationHandler;

    [Header("Audio Settings")]
    [SerializeField] protected AudioClip attackSFX;
    protected AudioSource audioSource;

    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        wizardAnimationHandler = GetComponentInChildren<WizardAnimationHandler>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    protected virtual void Update()
    {
        if (isDead) return;

        if (animator.GetBool("IsAttack"))
        {
            _rigidbody.velocity = Vector2.zero;
        }
        else
        {
            Move();
        }

        HandleAnimation();
    }

    public virtual void Move()
    {
        if (_rigidbody != null)
        {
            if (isDead) return;

            moveSpeed = 5;
            _rigidbody.velocity = new Vector2(moveInput.x * moveSpeed, _rigidbody.velocity.y);

            if (moveInput.x > 0)
                _spriteRenderer.flipX = false;
            else if (moveInput.x < 0)
                _spriteRenderer.flipX = true;
        }

        if (wizardAnimationHandler != null)
            wizardAnimationHandler.Move(moveInput);
    }

    public virtual void Jump()
    {
        if (isGrounded)
        {
            Debug.Log("Jump");
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, jumpForce);

            if (wizardAnimationHandler != null)
            {
                wizardAnimationHandler._Jump();
                Debug.Log("Start Jump");
            }
            else if (animator != null)
            {
                animator.SetBool("IsJump", false);
            }
        }
        else
        {
            if (wizardAnimationHandler != null)
            {
                Debug.Log("Start Idle");
                wizardAnimationHandler.Idle();
            }
            else if (animator != null)
            {
                animator.SetBool("IsIdle", false);
            }
        }
    }

    public virtual void Attack()
    {
        if (animator != null)
        {
            if (animator.GetBool("IsAttack")) return;

            if (wizardAnimationHandler != null)
                wizardAnimationHandler.Attack();
            else if (animator != null)
                animator.SetTrigger("IsAttack");
        }

        if (AudioManager.instance != null)
            AudioManager.instance.PlaySFX(attackSFX);
    }

    protected virtual void HandleAnimation()
    {
        if (animator == null || isDead) return;

        bool IsRun = moveInput.x != 0f;
        animator.SetBool("IsRun", IsRun);

        animator.SetBool("IsJump", !isGrounded);

        float vSpeed = _rigidbody.velocity.y;
        animator.SetFloat("VerticalSpeed", vSpeed);

        bool isFalling = !isGrounded && vSpeed < -0.1f;
        animator.SetBool("IsFall", isFalling);

        bool isIdle = isGrounded && !IsRun && !animator.GetBool("IsAttack") && !isFalling;
        animator.SetBool("IsIdle", isIdle);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"충돌한 레이어: {LayerMask.LayerToName(collision.gameObject.layer)}");
        if (((1 << collision.gameObject.layer) & groundLayer.value) != 0)
        {
            isGrounded = true;
            animator.SetBool("IsJump", false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & groundLayer.value) != 0)
        {
            isGrounded = false;
            Debug.Log("isGrounded false ");
        }
    }

    public bool IsGrounded() => isGrounded;
    public void SetMoveInput(Vector2 input) => moveInput = input;
    public void SetGroundedManually() => isGrounded = true;

    protected bool isDead = false;
    public virtual void Die()
    {
        if (isDead) return;

        isDead = true;
        animator?.SetTrigger("IsDie");

        _rigidbody.velocity = Vector2.zero;
        _rigidbody.bodyType = RigidbodyType2D.Static;
    }
}
