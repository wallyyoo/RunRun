using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class BreakByKnight : MonoBehaviour, IDamageable
{
    public void TakeDamage(int damage)
    {
        Destroy(gameObject);
    }
}
