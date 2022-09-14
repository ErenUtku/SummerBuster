using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class RingMovement : MonoBehaviour
{
    private Vector3 mOffset;
    private float mZCoord;

    public Vector3 defaultPosition;
    public Vector3 targetPosition;

    private bool doMoveActive=true;

    private PlayerRingController playerRingController;
    private GhostRingActivator _ghostRing;
    private RingType ringType;
    
    private void Start()
    {
        playerRingController = GetComponentInParent<PlayerRingController>();
        ringType = GetComponent<RingType>();
        defaultPosition = transform.position;

        targetPosition = playerRingController.SetDestination();
        _ghostRing = playerRingController.ghostRing;
    }

    private void OnMouseDown()
    {
        if (!ringType.isActive) return;

        _ghostRing.isSelected = true;

        playerRingController.RemoveRing(ringType);

        if (doMoveActive)
        {
            doMoveActive = false;

            mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
            mOffset = gameObject.transform.position - GetMouseWorldPos();
            mOffset.z = -3;
            
            transform.DOMove(targetPosition, 0.3f).SetEase(Ease.Linear).OnComplete(() =>
             {
                 var newVectorZ = new Vector3(targetPosition.x, targetPosition.y, targetPosition.z - 2);
                 transform.DOMove(newVectorZ, 0.3f).SetEase(Ease.Linear).OnComplete(() => { doMoveActive = true; });
             });
        }
    }

    private void OnMouseDrag()
    {  
        if (!ringType.isActive) return;

        if (doMoveActive)
        {
            var newVector = (GetMouseWorldPos() + mOffset);

            transform.position = (GetMouseWorldPos() + mOffset);

            //SetTransformZ(-4);
        }
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;

        mousePoint.z = mZCoord;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private void OnMouseUp()
    {
        if (!ringType.isActive) return;

        DOTween.KillAll();

        doMoveActive = false;

        var distanceCalculation = Vector3.Distance(transform.position,defaultPosition);       

        //if() HICBIR SEKILDE OTURMADIYSA{} targetPOs ve defaultpos degisecek.
        transform.DOMove(targetPosition, (distanceCalculation/50)).SetEase(Ease.Linear).OnComplete(() => 
        {
            transform.DOMove(defaultPosition, 0.5f).SetEase(Ease.OutBounce).OnComplete(() => 
            {
                doMoveActive = true;        
                _ghostRing.isSelected = false;

                var newRingController = GetComponentInParent<PlayerRingController>();
                newRingController.AddRing(ringType);

                playerRingController.FindBodyColor();
                newRingController.FindBodyColor();

                playerRingController = newRingController;

                LevelEndController.Instance.CheckLevelEnd();
            });
        });
    }

    private void SetTransformZ(float n)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, n);
    }
}
