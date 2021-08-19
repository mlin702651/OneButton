using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class brushMovement : MonoBehaviour
{
    private Vector3 brushOriginalPosition = new Vector3(0,0,0);
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Brush(float brushDuration){
        brushOriginalPosition = transform.localPosition;
        DOTween.Sequence()
            .Append(transform.DOLocalMove(brushOriginalPosition + new Vector3(0.7f,0f,0),brushDuration))
            .Append(transform.DOLocalMove(brushOriginalPosition,brushDuration));
    }
}
