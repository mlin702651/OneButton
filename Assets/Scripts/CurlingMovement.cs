using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class CurlingMovement : MonoBehaviour
{
    public PlayerInput controls;
    private Vector3 StartPoint = new Vector3(0,0,0);
    private Vector3 StartRotation = new Vector3(0,0,0);
    private float StartSpeed = 0f;
    private bool ifRotatePressed = false;
    private bool ifAddSpeedPressed = false;

    [SerializeField] private GameObject brush;
    
    private Rigidbody m_Rigidbody;
    
    
    
    [SerializeField] private float rotateSpeed = 5.0f;
    [SerializeField] private float fowardSpeed = 10.0f;
    [SerializeField] private float decreaseSpeed = 0.01f;
    [SerializeField] private float brushAddSpeed = 0.02f;
    [SerializeField] private float brushDuration = 0.05f;
    [SerializeField] private Ease brushEase = Ease.Linear;
    private Vector3 brushCurrentPosition = new Vector3(0,0,0);
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
        StartPoint = transform.position;
        StartSpeed = fowardSpeed;
        StartRotation = transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        if(ifRotatePressed){
            transform.Rotate(0,rotateSpeed*Time.deltaTime,0);
        }
        else m_Rigidbody.velocity = transform.forward * fowardSpeed;

        if(ifAddSpeedPressed){
            ifAddSpeedPressed = false;
            if(fowardSpeed<=4.0f && !ifRotatePressed){
                brushCurrentPosition = brush.transform.position;
                DOTween.Sequence()
                .Append(brush.transform.DOLocalMove(new Vector3(0.7f,0,1),brushDuration).SetEase(brushEase))
                .Append(brush.transform.DOLocalMove(-new Vector3(0.7f,0,-1),brushDuration).SetEase(brushEase));
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
    }
    void EndRotate(){
        Debug.Log("Stop rotate!");
        ifRotatePressed = false;
    }

    void StartAddSpeed(){
        ifAddSpeedPressed = true;
    }
}
