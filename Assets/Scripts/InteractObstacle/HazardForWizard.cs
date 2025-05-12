using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardForWizard : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<WizardController>(out var wizard))
        {
            wizard.Die();
        }
    }
}
