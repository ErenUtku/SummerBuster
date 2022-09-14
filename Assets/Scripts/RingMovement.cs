using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class RingMovement : MonoBehaviour
{
    private Vector3 mOffset;
    private float mZCoord;

    private Vector3 defaultPosition;
    private bool doMoveActive=true;

    private GhostRingActivator _ghostRing;
    private bool ringActivation;  
    
    private void Start()
    {
        ringActivation = GetComponent<RingType>().isActive;
        defaultPosition = transform.position + new Vector3(0,3,0);

        _ghostRing = GetComponentInParent<PlayerRingController>().ghostRing;
    }

    private void OnMouseDown()
    {
        if (!ringActivation) return;

        _ghostRing.isSelected = true;

        if (doMoveActive)
        {
            doMoveActive = false;

            mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
            mOffset = gameObject.transform.position - GetMouseWorldPos();
            mOffset.z = -3;

            var newVector = defaultPosition.y + 3;
            transform.DOMoveY(newVector, 0.3f).SetEase(Ease.Linear).OnComplete(() =>
             {
                 var newVectorZ = new Vector3( defaultPosition.x, newVector, defaultPosition.z - 2);
                 transform.DOMove(newVectorZ, 0.3f).SetEase(Ease.Linear).OnComplete(() => { doMoveActive = true; });
             });
        }
    }

    private void OnMouseDrag()
    {  
        if (!ringActivation) return;

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
        if (!ringActivation) return;

        DOTween.KillAll();

        _ghostRing.isSelected = false;

        doMoveActive = false;
        var distanceCalculation = Vector3.Distance(transform.position,defaultPosition);
        
        transform.DOMove(defaultPosition, (distanceCalculation/50)).SetEase(Ease.Linear).OnComplete(() => 
        {
            var newVector = defaultPosition.y - 3;
            transform.DOMoveY(newVector, 0.5f).SetEase(Ease.OutBounce).OnComplete(() => { doMoveActive = true; });
        });
    }

    private void SetTransformZ(float n)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, n);
    }
}
