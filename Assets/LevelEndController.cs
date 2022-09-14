using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class LevelEndController : MonoBehaviour
{
    [SerializeField] PlayerRingController[] players;
    
    public bool checkLevelEnd;
    public static LevelEndController Instance;
    private void Awake()
    {
        Instance = this;
    }

    public void CheckLevelEnd()
    {
        int checkPlayerCounter=0;
        foreach (var player in players)
        {
            if(player.CheckColorOfList(out checkLevelEnd)==true)
            {
                checkPlayerCounter++;
                if (checkPlayerCounter == players.Length && CheckPlayerHasNothing())
                {
                    Debug.Log("Level End");
                }
            }
        }
    }

    private bool CheckPlayerHasNothing()
    {
        foreach (var player in players)
        {
            if (player.bodyColor == COLOR.COLORFUL)
            {
                return true;
            }
        }
        return false;
    }
}
