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
}
