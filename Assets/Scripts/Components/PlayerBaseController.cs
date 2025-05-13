using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseController : MonoBehaviour
{


    [Header("Movement Settings")]

    //�⺻�̵��ӵ�
    [SerializeField] protected float moveSpeed = 5f;
    [SerializeField] protected float jumpForce = 10f;

    protected Rigidbody2D _rigidbody;// ĳ���� �̵� �� ���� ó���� ���� Rigidbody2D
    protected Animator animator;// �ִϸ����� ������Ʈ (�ִϸ��̼� ����)
    protected Vector2 moveInput;// �Է����� ������ �̵� ���� (x�ุ ���)
    protected SpriteRenderer _spriteRenderer;
    [SerializeField] protected LayerMask groundLayer;
    protected bool isGrounded;

    protected WizardAnimationHandler wizardAnimationHandler;
    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        wizardAnimationHandler = GetComponentInChildren<WizardAnimationHandler>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    protected virtual void Update()/// �� ������ ȣ��: �̵� ó�� �� �ִϸ��̼� ���� ������Ʈ
    {
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

        if (_rigidbody != null)// X�� �ӵ� = �Է°� * �̵��ӵ�, Y�� �ӵ��� ���� �ӵ� ����
        {
            moveSpeed = 5;
            _rigidbody.velocity = new Vector2(moveInput.x * moveSpeed, _rigidbody.velocity.y);
            if (moveInput.x > 0)
            {
                _spriteRenderer.flipX = false;
            }
            else if (moveInput.x < 0)
            {
                _spriteRenderer.flipX = true;
            }
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
                animator.SetBool("IsJump", false);

            //isGrounded = false;

        }
        else
        {
            if (wizardAnimationHandler != null)
            {
                Debug.Log("Start Idle");
                wizardAnimationHandler.Idle();
            }
            else if (animator != null)
                animator.SetBool("IsIdle", false);
        }


    }
    public virtual void Attack()
    {
        if (animator != null)
        {
            if (wizardAnimationHandler != null)
                wizardAnimationHandler.Attack();
            else if (animator != null)
                animator.SetTrigger("IsAttack");
        }
    }


    public virtual void Die()
    {
        if (animator != null)
        {
            animator.SetBool("IsDie", false);
        }
    }

    protected virtual void HandleAnimation()
    {
        if (animator == null)
            return;

        // �̵� ������
        bool IsRun = moveInput.x != 0f;
        animator.SetBool("IsRun", IsRun);

        // ���߿� �ִ��� ���� (����)
        animator.SetBool("IsJump", !isGrounded);

        // ���� �ӵ�
        float vSpeed = _rigidbody.velocity.y;
        animator.SetFloat("VerticalSpeed", vSpeed);

        // ���� ����: ���߿� �ְ�, �Ʒ��� �������� �ִ� ���
        bool isFalling = !isGrounded && vSpeed < -0.1f;
        animator.SetBool("IsFall", isFalling);

        // �ٴڿ� ������ ������ ��� Idle ó�� (���� ��Ȯ�� üũ)
        bool isIdle = isGrounded && !IsRun && !animator.GetBool("IsAttack") && !isFalling;
        animator.SetBool("IsIdle", isIdle);
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
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
    public bool IsGrounded() // �ڽ��� �� �� �ְ�.
    {
        return isGrounded;
    }
    public void SetMoveInput(Vector2 input)
    {
        moveInput = input;
    }
}
