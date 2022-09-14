using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionController : MonoBehaviour
{
    private GhostRingActivator ghostRing;

    private void Start()
    {
        ghostRing = gameObject.GetComponent<PlayerRingController>().ghostRing;
    }
    private void OnTriggerEnter(Collider other)
    {
        ghostRing.ActivateGhostObject(other.GetComponent<RingType>());
    }

    private void OnTriggerExit(Collider other)
    {
        ghostRing.DeactivateGhostObject();
    }
}
