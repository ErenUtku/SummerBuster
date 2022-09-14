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
    
    public Vector3 oldDefault;
    public Vector3 oldTarget;
    
    public bool doMoveActive=true;

    public PlayerRingController oldRingController;
    private GhostRingActivator _ghostRing;
    private RingType ringType;

    private Tween firstTween;
    
    private void Start()
    {
        oldRingController = GetComponentInParent<PlayerRingController>();
        ringType = GetComponent<RingType>();

        Invoke(nameof(SetPosition), 0.1f);
        _ghostRing = oldRingController.ghostRing;
    }

    private void OnMouseDown()
    {
        if (!ringType.isActive) return;

        oldRingController.RemoveRing(ringType);
        
        if (doMoveActive == true)
        {
            doMoveActive = false;
            _ghostRing.isSelected = true;

            mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
            mOffset = gameObject.transform.position - GetMouseWorldPos();
            mOffset.z = -3;

            firstTween=
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
        if (!doMoveActive) return;
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

    private void  OnMouseUp()
    {
        if (!ringType.isActive) return;
        
        DOTween.KillAll();
        
        oldRingController.FindBodyColor();
        var newRingController = GetComponentInParent<PlayerRingController>();
        oldRingController = newRingController;
        
        var distanceCalculation = Vector3.Distance(transform.position, targetPosition);

        doMoveActive = false;
        
        transform.DOMove(targetPosition, (distanceCalculation / 50)).SetEase(Ease.Linear).OnComplete(() =>
        {
            transform.DOMove(defaultPosition, 0.5f).SetEase(Ease.OutBounce).OnComplete(() =>
            {
                _ghostRing.isSelected = false;
                LevelEndController.Instance.CheckLevelEnd();
                
                newRingController.AddRing(ringType);
                newRingController.FindBodyColor();
                
                oldTarget = targetPosition;
                oldDefault= defaultPosition;
                
                Invoke(nameof(DelayDoMoveActive),2f);
            });
        });
    }

    private void SetTransformZ(float n)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, n);
    }

    private void SetPosition()
    {
        defaultPosition = transform.position;
        oldDefault = defaultPosition;
        targetPosition = oldRingController.SetDestination();
        oldTarget = targetPosition;
    }

    private void DelayDoMoveActive()
    {
        doMoveActive = true;
    }
}
