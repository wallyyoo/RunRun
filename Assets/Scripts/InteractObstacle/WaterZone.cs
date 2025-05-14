using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WaterZone : MonoBehaviour
{
    [SerializeField] protected LayerMask groundLayer;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"[WaterZone] �浹�� ��ü: {other.name}, Layer: {LayerMask.LayerToName(other.gameObject.layer)}");
        if (other.TryGetComponent<WizardController>(out var wizard))
      {      Debug.Log("�� ������ϱ� ����");
            wizard.SetGroundedManually();
            return;
        }

        if (other.TryGetComponent<KnightController>(out var knight))
        {
            Debug.Log("[WaterZone] ��� �߰�, ���� ���� ����.");
            knight.FallintoWater();
        }
      

    }
    private void Start()
    {
        Debug.Log($"[WaterZone] �� ���̾��: {LayerMask.LayerToName(gameObject.layer)}");
    }
}
