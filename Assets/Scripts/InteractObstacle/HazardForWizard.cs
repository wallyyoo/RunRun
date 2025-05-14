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
                    Debug.Log("▶ 마법사만 죽게 하는 해저드에 부딪힘");
                    wizard.Die();
                }
            }
        }
    }

}
