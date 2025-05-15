using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    public GameObject buttonToShow;

    private void OnDestroy()
    {
        if (buttonToShow != null)
        {
            buttonToShow.SetActive(true);
        }
    }
}

