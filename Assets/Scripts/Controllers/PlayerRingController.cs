using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRingController : MonoBehaviour
{
    [Header("Playground Values")]

    [SerializeField] private List<RingType> allRings;

    public GhostRingActivator ghostRing;
    public GameObject ringFolder;

    [Space]

    [Header("Player Data Values")]
    [SerializeField] private Animator animator;
    public COLOR bodyColor;
    
 
    private Vector3 _ringPosition;
    
    void Awake()
    {
        FindBodyColor();
        ghostRing.SetTransformGhostRingActivator(allRings.Count);

        var position = transform.position;
        _ringPosition = new Vector3(position.x, 9, position.z);
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
        ghostRing.SetTransformGhostRingActivator(allRings.Count);
        ghostRing.DeactivateGhostObject();
    }

    public void RemoveRing(RingType oldRing)
    {
        allRings.Remove(oldRing);
        ghostRing.SetTransformGhostRingActivator(allRings.Count);
    }

    public void DeactivateAllRing()
    {
        foreach (var ring in allRings)
        {
            ring.isActive = false;
        }
    }

    public Vector3  SetDestination()
    {
        return _ringPosition;
    }

    public Vector3 SetDefaultRingPosition()
    {
        var target = (allRings.Count) * 1.5f;
        var position = transform.position;
        return new Vector3(position.x, target, position.z);
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
}
