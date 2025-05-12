using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnController : MonoBehaviour
{
    public GameObject doorObject;

    void OnTriggerStay2D(Collider2D collision)
    {
        doorObject.SetActive(false); 
    }

    
    void OnTriggerExit2D(Collider2D collision)
    {
        doorObject.SetActive(true);
    }
}