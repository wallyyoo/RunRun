using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WaterZone : MonoBehaviour
{
    [SerializeField] protected LayerMask groundLayer;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"[WaterZone] 충돌한 객체: {other.name}, Layer: {LayerMask.LayerToName(other.gameObject.layer)}");
        if (other.TryGetComponent<WizardController>(out var wizard))
      {      Debug.Log("▶ 마법사니까 무시");
            wizard.SetGroundedManually();
            return;
        }

        if (other.TryGetComponent<KnightController>(out var knight))
        {
            Debug.Log("[WaterZone] 기사 발견, 낙하 로직 실행.");
            knight.FallintoWater();
        }
      

    }
    private void Start()
    {
        Debug.Log($"[WaterZone] 내 레이어는: {LayerMask.LayerToName(gameObject.layer)}");
    }
}
