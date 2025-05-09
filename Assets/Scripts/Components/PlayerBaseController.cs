using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseController : MonoBehaviour
{

    [Header("Movement Settings")]
    
    //기본이동속도
    [SerializeField] protected float moveSpeed = 5f;

    protected Rigidbody2D _rigidbody;
    protected Animator animator;
    protected Vector2 moveInput;

    protected virtual void Awake()
    {
        _rigidbody= GetComponent<Rigidbody2D>();
        animator= GetComponent<Animator>();
    }

    protected virtual void Update()
    {
        Move();
        HandleAnimation();
    }
    public virtual void Move()
    {
        if(_rigidbody!=null)
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

            bool isMoving = moveInput.x != 0f;
            animator.SetBool("IsMoving",isMoving);
        }
    }
 
}
