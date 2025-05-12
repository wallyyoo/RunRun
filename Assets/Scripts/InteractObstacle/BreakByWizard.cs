using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakByWizard : MonoBehaviour, IDamageable
{
public void TakeDamage(int damage)
    {
        Destroy(gameObject);
    }    
}
