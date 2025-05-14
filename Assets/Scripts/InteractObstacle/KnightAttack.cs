using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightAttack : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private LayerMask breakable_Knight;

    [SerializeField] private Transform knightTransform;


    public void Attack()
    {
        StartCoroutine(DelayedAttack(0.3f)); // 0.3초 뒤에 공격 실행
    }

    private IEnumerator DelayedAttack(float delay)
    {
        yield return new WaitForSeconds(delay);

        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, breakable_Knight);

        foreach (var hit in hits)
        {
            Debug.Log("Hit object: " + hit.name);
            if (hit.TryGetComponent<IDamageable>(out var target))
            {
                target.TakeDamage(1);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint != null) 
            Gizmos.DrawWireSphere(attackPoint.position,attackRange);
    }
}
