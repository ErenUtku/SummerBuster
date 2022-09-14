using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostRingCollisionController : MonoBehaviour
{
    private GhostRingActivator _ghostRing;

    private void Start()
    {
        _ghostRing = gameObject.GetComponent<GhostRingActivator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        _ghostRing.ActivateGhostObject(other.GetComponent<RingType>());
    }

    private void OnTriggerExit(Collider other)
    {
        _ghostRing.DeactivateGhostObject();
    }
}