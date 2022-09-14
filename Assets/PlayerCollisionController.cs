using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionController : MonoBehaviour
{
    private PlayerRingController ringController;
    private GhostRingActivator ghostRing;


    private void Start()
    {
        ringController = gameObject.GetComponent<PlayerRingController>();
        ghostRing = ringController.ghostRing;
    }
    private void OnTriggerEnter(Collider other)
    {
        ghostRing.ActivateGhostObject(other.GetComponent<RingType>());    
        if (other.gameObject.tag == "Ring")
        {
            var ringObject = other.GetComponent<RingMovement>();

            if (ringObject.gameObject.GetComponent<RingType>().ringData.color == ringController.bodyColor || ringController.bodyColor == COLOR.COLORFUL)
            {
                ringObject.gameObject.transform.parent = ringController.ringFolder.transform;
                ringObject.targetPosition = ringController.SetDestination();
                ringObject.defaultPosition = ringController.SetDefaultRingPosition();
            }
        }  
    }

    private void OnTriggerExit(Collider other)
    {
        ghostRing.DeactivateGhostObject();
    }
}
