using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseController : MonoBehaviour
{

    [Header("Movement Settings")]
    
    //�⺻�̵��ӵ�
    [SerializeField] protected float moveSpeed = 5f;

    protected Rigidbody2D _rigidbody;// ĳ���� �̵� �� ���� ó���� ���� Rigidbody2D
    protected Animator animator;// �ִϸ����� ������Ʈ (�ִϸ��̼� ����)
    protected Vector2 moveInput;// �Է����� ������ �̵� ���� (x�ุ ���)

    protected virtual void Awake()
    {
        _rigidbody= GetComponent<Rigidbody2D>();
        animator= GetComponent<Animator>();
    }

    protected virtual void Update()/// �� ������ ȣ��: �̵� ó�� �� �ִϸ��̼� ���� ������Ʈ
    {
        Move();
        HandleAnimation();
    }
    public virtual void Move()
    {
        if(_rigidbody!=null)// X�� �ӵ� = �Է°� * �̵��ӵ�, Y�� �ӵ��� ���� �ӵ� ����
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

            bool isMoving = moveInput.x != 0f;// �Է°��� 0�� �ƴϸ� �̵� ��
            animator.SetBool("IsMoving",isMoving);// �ִϸ����Ϳ� ����
        }
    }
 
}
