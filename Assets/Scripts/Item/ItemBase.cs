using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ItemBase : MonoBehaviour
{
    public Vector3 targetPos;
    public Transform holder;
    public void doMove()
    {
        transform.parent = holder;
        transform.DOLocalJump(targetPos, 1, 1, 0.1f);
        
    }

    //private void Start()
    //{
    //    transform.DORotate(new Vector3(0, 180, 0), 2f).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
    //}
}
