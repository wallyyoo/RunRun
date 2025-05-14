using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardForKnight : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent<KnightController>(out var knight))
            {
                if (gameObject.CompareTag("HazardForKnight_tag"))
                {
                    Debug.Log("�� ��縸 �״� �±�");
                    knight.Die();
                }
            }
        }
    }

}
