using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostRingActivator : MonoBehaviour
{
    public RingType[] ringType;

    private void Start()
    {
        DeactivateGhostObject();
    }

    public void ActivateGhostObject(RingType selectedRing)
    {
        foreach (var ring in ringType)
        {
            if (ring.ringData.color == selectedRing.ringData.color)
            {
                ring.gameObject.SetActive(true);
            }
        }
    }

    public void DeactivateGhostObject()
    {
        foreach (var ring in ringType)
        {
            ring.gameObject.SetActive(false);
        }
    }
}
