using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostRingActivator : MonoBehaviour
{
    public RingType[] ringType;
    public bool isSelected;
  
    private void Start()
    {
        Invoke(nameof(DeactivateGhostObject),0.1f);       
    }

    public void ActivateGhostObject(RingType selectedRing)
    {
        if (!isSelected)
        {
            foreach (var ring in ringType)
            {
                if (ring.ringData.color == selectedRing.ringData.color)
                {
                    ring.gameObject.SetActive(true);
                }
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

    public void SetTransformGhostRingActivator(float value)
    {
        transform.position = new Vector3(transform.position.x, 1.5f*value, transform.position.z);
    }
    
}
