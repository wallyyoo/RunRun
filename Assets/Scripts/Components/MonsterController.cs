using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour, IDamageable
{
  public void TakeDamage(int damage)
    {
        Die();

    }
    private void Die()
    {
        GameManager.Instance.EnemyDied();
        Destroy(gameObject);
    }
}
