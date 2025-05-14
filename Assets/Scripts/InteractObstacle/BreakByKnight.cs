using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class BreakByKnight : MonoBehaviour, IDamageable
{
    public void TakeDamage(int damage)
    {
        Debug.Log("bbk Ãâ·Â");
        Destroy(gameObject);
    }
}
