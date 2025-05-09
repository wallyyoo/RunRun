using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;

public class WizardController : PlayerBaseController
{
    public static WizardController Instance { get; private set; }
   [SerializeField]private GameObject projectilePrefab;
    [SerializeField] private LayerMask projectileHitLayers;

    private void Awake()
    {
        base.Awake();
        Instance = this;
    }

    private void Update()
    {
        moveInput = InputManager.Instance.GetWizardMovement();

        if (InputManager.Instance.GetWizardJump())
            Jump();

        if(InputManager.Instance.GetWizardAttack())
          {
           
            FireProjectile();
        }
        base.Update();
    }

    private void FireProjectile()
    {
        Attack();

        var proj = Instantiate(projectilePrefab, transform.position, transform.rotation);

        var magic = proj.GetComponent<MagicProjectile>();
        if (magic != null) 
            magic.hitLayers = projectileHitLayers;
    }



}
