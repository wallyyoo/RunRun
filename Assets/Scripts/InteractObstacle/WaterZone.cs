using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<KnightController>(out var knight))
        {
            knight.Die();
        }     
    }
}
