using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightAnimationHandler : MonoBehaviour
{
    private static readonly int IsAttack = Animator.StringToHash("IsAttack");
    private static readonly int IsJump = Animator.StringToHash("IsJump");
    private static readonly int IsDie = Animator.StringToHash("IsDie");
    private static readonly int IsRun = Animator.StringToHash("IsRun");
    private static readonly int IsIdle = Animator.StringToHash("IsIdle");

    protected Animator animator;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
            animator = GetComponentInChildren<Animator>();
    }

    public void Move(Vector2 input)
    {
        animator.SetBool(IsRun, input.magnitude > 0.1f);
    }

    public void Attack()
    {
        animator.SetBool(IsAttack, true);
    }

    public void OnAttackAnimationEnd()
    {
        animator.SetBool(IsAttack, false);
    }

    public void Jump()
    {
        animator.SetBool(IsJump, true);
    }

    public void Die()
    {
        animator.SetTrigger("IsDie");
    }

    public void Idle()
    {
        animator.SetBool(IsIdle, true);
    }
}

