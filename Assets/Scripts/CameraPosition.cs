using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    [SerializeField]
    GameObject mainCamera;

    public Vector3 moveTo;
    [SerializeField]
    bool isMoving=false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isMoving){
            mainCamera.transform.position+=moveTo;
            isMoving=false;
        }
    }
    void OnTriggerEnter(Collider other)
    {

        if(other.tag=="nextLevel"){
            isMoving=true;   
        }
    }
}
