using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardForWizard : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player")) 
        {
            if (other.TryGetComponent<WizardController>(out var wizard))
            {
                if (gameObject.CompareTag("HazardForWizard_tag"))
                {
                    Debug.Log("�� �����縸 �װ� �ϴ� �����忡 �ε���");
                    wizard.Die();
                }
            }
        }
    }

}
