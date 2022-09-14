using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRingController : MonoBehaviour
{
    [SerializeField] private List<RingType> allRings;
    public GameObject ringFolder;
    public GhostRingActivator ghostRing;
    public COLOR bodyColor;
    [SerializeField] Animator animator;
 
    private Vector3 ringPosition;
    
    void Awake()
    {
        FindBodyColor();
        ghostRing.SetTransformGhostRingActivator(allRings.Count);

        ringPosition = new Vector3(transform.position.x, 9, transform.position.z);
    }    

    public void FindBodyColor()
    {
        var lastRingInt = allRings.Count;
        if (lastRingInt == 0)
        {
            bodyColor = COLOR.COLORFUL;
            return;
        }

        foreach (var ring in allRings)
        {
            ring.isActive = false;
        }

        var lastItem = allRings[lastRingInt - 1];
        lastItem.isActive = true;
        bodyColor = lastItem.ringData.color;
    }

    public void AddRing(RingType newRing)
    {
        allRings.Add(newRing);
    }

    public void RemoveRing(RingType oldRing)
    {
        allRings.Remove(oldRing);
    }

    public Vector3  SetDestination()
    {
        return ringPosition;
    }

    public Vector3 SetDefaultRingPosition()
    {
        var target = (allRings.Count) * 1.5f;
        return new Vector3(transform.position.x, target, transform.position.z);
    }
    
    public bool CheckColorOfList(out bool value)
    {
        foreach (var ring in allRings)
        {
            if (ring.ringData.color != bodyColor)
            {
                return value = false;
            }
        }
        return value = true;
    }

    public void PlayAnimation()
    {
        animator.SetBool("LevelEnd", true);
    }
}
