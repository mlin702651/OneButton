using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class CurlingMovement : MonoBehaviour
{
    public PlayerInput controls;
    private int curlingState = 0;//0:normal 1:sand 2:water 3:disable
    private bool curlingRotateclockwise = true;//true順時針 false逆時針
    private Vector3 StartPoint = new Vector3(0,0,0);
    private Vector3 StartRotation = new Vector3(0,0,0);
    private float StartSpeed = 0f;
    private bool ifRotatePressed = false;
    private bool ifAddSpeedPressed = false;

    [SerializeField] private GameObject brush;
    [SerializeField] private GameObject curling;
    
    private Rigidbody m_Rigidbody;
    
    
    
    [SerializeField] private float rotateSpeed = 5.0f;
    [SerializeField] private float fowardSpeed = 10.0f;
    [SerializeField] private float decreaseSpeed = 0.01f;
    [SerializeField] private float brushAddSpeed = 0.02f;
    [SerializeField] private float brushDuration = 0.05f;
    [SerializeField] private Ease brushEase = Ease.Linear;
    private Vector3 brushCurrentPosition = new Vector3(0,0,0);
    private Vector3 newCurlingDirection = new Vector3(0,0,0);
    void Awake()
    {
        controls = new PlayerInput();
        controls.Player.CurlingRotate.performed += ctx => StartRotate();
        controls.Player.CurlingRotate.canceled += ctx => EndRotate();

        controls.Player.BrushAddSpeed.performed += ctx => StartAddSpeed();
    }
    
    private void OnEnable(){
        controls.Enable();
    }
    private void OnDisable(){
        controls.Disable();
        
    }
    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        newCurlingDirection = transform.forward;
        StartPoint = transform.position;
        StartSpeed = fowardSpeed;
        //StartRotation = transform.eulerAngles;
        StartRotation = curling.transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        if(ifRotatePressed&&curlingRotateclockwise){
            //transform.Rotate(0,rotateSpeed*Time.deltaTime,0);
            curling.transform.Rotate(0,rotateSpeed*Time.deltaTime,0);
        }
        else if(ifRotatePressed&&!curlingRotateclockwise)curling.transform.Rotate(0,-rotateSpeed*Time.deltaTime,0);
        else if(curlingState==0) m_Rigidbody.velocity = newCurlingDirection * fowardSpeed;
        else if(curlingState==1) m_Rigidbody.velocity = newCurlingDirection * fowardSpeed * 0.3f;
        else if(curlingState==2) m_Rigidbody.velocity = newCurlingDirection * fowardSpeed * 1.3f;
        else if(curlingState==3) m_Rigidbody.velocity = newCurlingDirection * fowardSpeed * 0.1f;
        // else if(curlingState==0) m_Rigidbody.velocity = transform.forward * fowardSpeed;
        // else if(curlingState==1) m_Rigidbody.velocity = transform.forward * fowardSpeed * 0.3f;
        // else if(curlingState==2) m_Rigidbody.velocity = transform.forward * fowardSpeed * 1.3f;
        // else if(curlingState==3) m_Rigidbody.velocity = transform.forward * fowardSpeed * 0.1f;

        if(ifAddSpeedPressed){
            ifAddSpeedPressed = false;
            if(fowardSpeed<=4.0f && !ifRotatePressed){
                brushCurrentPosition = brush.transform.position;
                brush.GetComponentInChildren<brushMovement>().Brush(brushDuration);
                // DOTween.Sequence()
                // .Append(brush.transform.DOLocalMove(brush.transform.localPosition + new Vector3(0.7f,0,0),brushDuration).SetEase(brushEase))
                // .Append(brush.transform.DOLocalMove(brush.transform.localPosition - new Vector3(0.7f,0,0),brushDuration).SetEase(brushEase));
                // .Append(brush.transform.DOLocalMove(new Vector3(0.7f,0,1),brushDuration).SetEase(brushEase))
                // .Append(brush.transform.DOLocalMove(-new Vector3(0.7f,0,-1),brushDuration).SetEase(brushEase));
                fowardSpeed += brushAddSpeed;

            }
            Debug.Log("Add speed!");
        }
        if(fowardSpeed>=0.4f)fowardSpeed -= decreaseSpeed;
        else {

            Debug.Log("Restart!");
            transform.position = StartPoint;
            fowardSpeed = StartSpeed;
            transform.eulerAngles = StartRotation;
            
        }

        

        
    }
    void StartRotate(){
        Debug.Log("Start rotate!");
        ifRotatePressed = true;
        curlingRotateclockwise = !curlingRotateclockwise;
    }
    void EndRotate(){
        Debug.Log("Stop rotate!");
        ifRotatePressed = false;
        newCurlingDirection = curling.transform.forward;
        brush.transform.position = transform.position + curling.transform.forward;
        brush.transform.eulerAngles = curling.transform.eulerAngles;
    }

    void StartAddSpeed(){
        ifAddSpeedPressed = true;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag=="NormalFloor"){
            Debug.Log("EnterNormal");
            //curlingState = 0;
        }
        else if(other.tag=="SandFloor"){
            Debug.Log("EnterSandFloor");
            //curlingState = 1;
        }
        else if(other.tag=="WetFloor"){
            Debug.Log("EnterWetFloor");
            //curlingState = 2;
        }
    }

    private void OnTriggerStay(Collider other) {
        if(other.tag=="NormalFloor"){
            Debug.Log("stay normal!");
            curlingState = 0;
        }
        else if(other.tag=="SandFloor"){
            Debug.Log("stay sand!");
            curlingState = 1;
        }
        else if(other.tag=="WetFloor"){
            Debug.Log("stay wet!");
            curlingState = 2;
        }
    }
    private void OnTriggerExit(Collider other) {
        Debug.Log("Enter disable Area!");
        curlingState=3;
    }
}
