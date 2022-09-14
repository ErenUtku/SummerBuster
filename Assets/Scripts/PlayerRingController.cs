using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRingController : MonoBehaviour
{
    [SerializeField] List<RingType> allRings;
    public GhostRingActivator ghostRing;
    public COLOR bodyColor;
    
    void Awake()
    {
        FindBodyColor();
        ghostRing.SetTransformGhostRingActivator(allRings.Count);
    }    

    private void FindBodyColor()
    {
        var lastRingInt = allRings.Count;
        if (lastRingInt == 0)
        {
            bodyColor = COLOR.COLORFUL;
            return;
        }

        var lastItem = allRings[lastRingInt - 1];
        lastItem.isActive = true;
        bodyColor = lastItem.ringData.color;
    }

    public void AddRing(RingType newRing)
    {
        allRings.Add(newRing);
    }

  
}
