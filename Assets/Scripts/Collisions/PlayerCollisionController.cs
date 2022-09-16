using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionController : MonoBehaviour
{
    private PlayerRingController _ringController;
    private GhostRingActivator _ghostRing;

    #region SET VARIABLES
    private void Start()
    {
        _ringController = gameObject.GetComponent<PlayerRingController>();
        _ghostRing = _ringController.ghostRing;
    }
    #endregion

    #region COLLISION CHECKERS
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ring"))
        {
            var ringObject = other.GetComponent<RingMovement>();

            if (ringObject.gameObject.GetComponent<RingType>().ringData.color == _ringController.bodyColor || _ringController.bodyColor == COLOR.COLORFUL)
            {
                //Activate Ghost
                _ghostRing.ActivateGhostObject(other.GetComponent<RingType>());

                //Change Parent of Ring Object
                ringObject.ChangeParent(_ringController.ringFolder.transform);
                
                //Set Target And Default Position of Ring Object
                ringObject.targetPosition = _ringController.SetDestination();
                ringObject.defaultPosition = _ringController.SetDefaultRingPosition();                                           
            }
        }  
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ring"))
        {
            var ringObject = other.GetComponent<RingMovement>();

            //Set Target And Default Position of Ring Object to Default
            ringObject.targetPosition = ringObject.oldTarget;
            ringObject.defaultPosition = ringObject.oldDefault;

            //Change Parent of Ring Object to Default
            ringObject.gameObject.transform.parent = ringObject.oldRingController.ringFolder.transform;
        }     

        _ghostRing.DeactivateGhostObject();

    }
    #endregion
}
