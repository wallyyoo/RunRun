using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseController : MonoBehaviour
{


    [Header("Movement Settings")]

    //기본이동속도
    [SerializeField] protected float moveSpeed = 5f;
    [SerializeField] protected float jumpForce = 10f;

    protected Rigidbody2D _rigidbody;// 캐릭터 이동 및 물리 처리를 위한 Rigidbody2D
    protected Animator animator;// 애니메이터 컴포넌트 (애니메이션 제어)
    protected Vector2 moveInput;// 입력으로 설정된 이동 벡터 (x축만 사용)
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

    protected virtual void Update()/// 매 프레임 호출: 이동 처리 및 애니메이션 상태 업데이트
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

        if (_rigidbody != null)// X축 속도 = 입력값 * 이동속도, Y축 속도는 기존 속도 유지
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

        // 이동 중인지
        bool IsRun = moveInput.x != 0f;
        animator.SetBool("IsRun", IsRun);

        // 공중에 있는지 여부 (점프)
        animator.SetBool("IsJump", !isGrounded);

        // 수직 속도
        float vSpeed = _rigidbody.velocity.y;
        animator.SetFloat("VerticalSpeed", vSpeed);

        // 낙하 상태: 공중에 있고, 아래로 떨어지고 있는 경우
        bool isFalling = !isGrounded && vSpeed < -0.1f;
        animator.SetBool("IsFall", isFalling);

        // 바닥에 완전히 착지한 경우 Idle 처리 (조건 정확히 체크)
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
    public bool IsGrounded() // 자식이 쓸 수 있게.
    {
        return isGrounded;
    }
    public void SetMoveInput(Vector2 input)
    {
        moveInput = input;
    }
}
