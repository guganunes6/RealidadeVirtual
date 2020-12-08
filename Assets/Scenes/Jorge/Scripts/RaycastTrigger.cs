using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider col) 
    {
        if (OVRPlayerControllerJorge.objHit == null)
        {
            OVRPlayerControllerJorge.colCount++;
            OVRPlayerControllerJorge.objHit = col.gameObject;
        }
        else if (OVRPlayerControllerJorge.objHit.GetComponent<Outline>().enabled)
        {
            OVRPlayerControllerJorge.colCount++;
            OVRPlayerControllerJorge.objHit = col.gameObject;
        }
    }

    private void OnTriggerExit(Collider col) 
    {
        if (OVRPlayerControllerJorge.colCount > 0) 
        {
            OVRPlayerControllerJorge.colCount--;
        }
    }
}
