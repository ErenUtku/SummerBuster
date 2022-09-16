using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LevelEndController : MonoBehaviour
{
    [SerializeField] private GameObject levelEndObject;
    [SerializeField] private PlayerRingController[] players;
    
    private PlayerRingController _freePlayer;
    private bool checkLevelEnd;

    public static LevelEndController instance;
    private void Awake()
    {
        instance = this;
        levelEndObject.GetComponent<Button>().onClick.AddListener(() => LevelRestart());
        levelEndObject.SetActive(false);
    }

    public void CheckLevelEnd()
    {
        var checkPlayerCounter = 0;
        
        foreach (var player in players)
        {
            if (player.bodyColor == COLOR.COLORFUL)
            {
                _freePlayer = player;
            }

            if(player.CheckColorOfList(out checkLevelEnd)==true)
            {
                checkPlayerCounter++;
                if (checkPlayerCounter == players.Length && CheckPlayerHasNothing())
                {

                    //Player Dance
                    _freePlayer.gameObject.GetComponent<PlayerAnimationController>().PlayAnimation();
                    
                    //UI ACTIVATION
                    levelEndObject.SetActive(true);
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

    private void LevelRestart()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}
