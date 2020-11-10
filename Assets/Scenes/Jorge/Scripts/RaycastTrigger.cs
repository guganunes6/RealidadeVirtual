using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider col) 
    {
        Debug.Log("Enter");
        if (OVRPlayerControllerJorge.objHit == null)
        {
            OVRPlayerControllerJorge.colCount++;
            OVRPlayerControllerJorge.objHit = col.gameObject;
        }
    }

    private void OnTriggerExit(Collider col) 
    {
        Debug.Log("Exit");
        if (OVRPlayerControllerJorge.colCount > 0) 
        {
            OVRPlayerControllerJorge.colCount--;
        }
    }
}
