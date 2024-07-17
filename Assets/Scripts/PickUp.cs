using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PickUp : MonoBehaviour
{

    public static PickUp instance;

    private void Awake()
    {
        instance = this;
        gameObject.SetActive(false);
    }
}
