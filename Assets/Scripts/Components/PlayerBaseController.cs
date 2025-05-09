using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseController : MonoBehaviour
{

    [Header("Movement Settings")]
    
    //기본이동속도
    [SerializeField] protected float moveSpeed = 5f;

    protected Rigidbody2D _rigidbody;// 캐릭터 이동 및 물리 처리를 위한 Rigidbody2D
    protected Animator animator;// 애니메이터 컴포넌트 (애니메이션 제어)
    protected Vector2 moveInput;// 입력으로 설정된 이동 벡터 (x축만 사용)

    protected virtual void Awake()
    {
        _rigidbody= GetComponent<Rigidbody2D>();
        animator= GetComponent<Animator>();
    }

    protected virtual void Update()/// 매 프레임 호출: 이동 처리 및 애니메이션 상태 업데이트
    {
        Move();
        HandleAnimation();
    }
    public virtual void Move()
    {
        if(_rigidbody!=null)// X축 속도 = 입력값 * 이동속도, Y축 속도는 기존 속도 유지
        {
            _rigidbody.velocity = new Vector2(moveInput.x*moveSpeed,_rigidbody.velocity.y);
        }
    }


    public virtual void Jump()
    {
        animator.SetTrigger("Jump");
    }
    public virtual void Attack()
    {
        if(animator!=null)
        {
            animator.SetTrigger("Attack");
        }
    }

    public virtual void Die()
    {
        if(animator!=null)
        {
            animator.SetTrigger("Die");
        }
    }

    protected virtual void HandleAnimation()
    {
        if(animator!=null)
        {

            bool isMoving = moveInput.x != 0f;// 입력값이 0이 아니면 이동 중
            animator.SetBool("IsMoving",isMoving);// 애니메이터에 전달
        }
    }
 
}
