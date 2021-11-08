using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerExample : MonoBehaviour
{
    protected void OnTriggerEnter(Collider collider)
    {
        Debug.Log("trigger: " + collider.gameObject.name);
    }
}
