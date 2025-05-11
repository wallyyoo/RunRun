using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class WizardAnimationHandler : MonoBehaviour
{
    private static readonly int IsAttack = Animator.StringToHash("IsAttack");
    private static readonly int IsJump = Animator.StringToHash("IsJump");
    private static readonly int IsDie = Animator.StringToHash("IsDie");
    private static readonly int IsRun = Animator.StringToHash("IsRun");
    private static readonly int IsIdle = Animator.StringToHash("IsIdle");


    protected Animator animator;
    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }


    public void Move(Vector2 obj)
    {
        animator.SetBool(IsRun, obj.magnitude > .5f);
    }

    public void Attack()
    {
        animator.SetBool(IsAttack, true);
    }
    public void _Jump()
    {
        animator.SetBool(IsJump, true);
    }
    public void Die()
    {
        animator.SetBool(IsDie, true);
    }
    public void Running()
    {
        animator.SetBool(IsRun, true);
    }

    public void Idle()
    {
        Debug.Log("Set Idle ");
        animator.SetBool(IsIdle, true);
    }

    public void OnAttackAnimationEnd()
    {
        animator.SetBool(IsAttack, false);
    }
}
