using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using DG.Tweening;

public class CurlingMovement : MonoBehaviour
{
    public PlayerInput controls;
    private int curlingState = 0;//0:normal 1:sand 2:water 3:disable
    private bool curlingRotateclockwise = true;//true順時針 false逆時針
    private Vector3 StartPoint = new Vector3(0,0,0);
    private Vector3 StartRotation = new Vector3(0,0,0);
    private Vector3 StartPoleRotation = new Vector3(0,0,0);
    private Vector3 StartCurlingRotation = new Vector3(0,0,0);
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
    //[SerializeField] private Ease brushEase = Ease.Linear;
    private Vector3 brushCurrentPosition = new Vector3(0,0,0);
    private Vector3 newCurlingDirection = new Vector3(0,0,0);

    [SerializeField]private Slider speedSlider;
    [SerializeField]private Text timerText;
    [SerializeField]private float timer = 60;
    private float originalTimer;

    [SerializeField] private GameObject curlingArrow;

    [SerializeField] private GameObject countDownCanvas;
    [SerializeField] private Text countDownText;
    private float countDownTimer = 4;
    private bool isStart = false;

    //music
    [SerializeField]public AudioSource bgmSource;
    [SerializeField]public AudioSource sandSource;
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
        StartRotation = transform.eulerAngles;
        StartPoleRotation = brush.transform.eulerAngles;
        StartCurlingRotation = curling.transform.eulerAngles;
        originalTimer = timer;
        curlingArrow.SetActive(false);

        //FindObjectOfType<AudioManager>().Play("bgm");
        bgmSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
        countDownTimer -= Time.deltaTime;
        int countDownSecond = Mathf.FloorToInt(countDownTimer);
        countDownText.text = countDownSecond.ToString("0");
        if(countDownSecond==0) countDownText.text = "START";
        if(countDownTimer<=0){
            isStart = true;
            countDownCanvas.SetActive(false);
        }
        if(!isStart) return;
        if(ifRotatePressed&&curlingRotateclockwise){
            //transform.Rotate(0,rotateSpeed*Time.deltaTime,0);
            curling.transform.Rotate(0,rotateSpeed*Time.deltaTime,0);
            
        }
        else if(ifRotatePressed&&!curlingRotateclockwise)curling.transform.Rotate(0,-rotateSpeed*Time.deltaTime,0);
        
        if(curlingState==0) m_Rigidbody.velocity = newCurlingDirection * fowardSpeed;
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
                speedSlider.value = fowardSpeed;

            }
            Debug.Log("Add speed!");
        }
        if(fowardSpeed>=0.4f){
            fowardSpeed -= decreaseSpeed;
            speedSlider.value = fowardSpeed;
        } 
        else {
            Debug.Log("Restart!");
            Restart();
        }

        if(timer<=0){
            Restart();
        }

        timer -= Time.deltaTime;
        int minutes = Mathf.FloorToInt(timer / 60F);
        int seconds = Mathf.FloorToInt(timer % 60F);
        timerText.text = minutes.ToString ("00") + ":" + seconds.ToString ("00");

        if(timer<=10){
            timerText.color = new Color32(185,84,80,255);
            //timerText.color = Color.red;
        }

        
    }
    void StartRotate(){
        Debug.Log("Start rotate!");
        ifRotatePressed = true;
        curlingRotateclockwise = !curlingRotateclockwise;
        curlingArrow.SetActive(true);
    }
    void EndRotate(){
        Debug.Log("Stop rotate!");
        ifRotatePressed = false;
        newCurlingDirection = curling.transform.forward;
        brush.transform.position = transform.position + curling.transform.forward;
        brush.transform.eulerAngles = curling.transform.eulerAngles;
        curlingArrow.SetActive(false);
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
            sandSource.Stop();
        }
        else if(other.tag=="SandFloor"){
            Debug.Log("stay sand!");
            curlingState = 1;
            sandSource.Play();
        }
        else if(other.tag=="WetFloor"){
            Debug.Log("stay wet!");
            curlingState = 2;
            sandSource.Stop();

        }
    }
    private void OnTriggerExit(Collider other) {
        Debug.Log("Enter disable Area!");
        curlingState=3;
            sandSource.Stop();

    }

    private void Restart(){
        transform.position = StartPoint;
        fowardSpeed = StartSpeed;
        curlingState=0;
        newCurlingDirection = transform.forward;
        //brush.transform.localEulerAngles = StartPoleRotation;
        brush.transform.localEulerAngles = new Vector3(0,-180,0);
        brush.transform.localPosition = new Vector3(-0.5f,0,1);
        //curling.transform.localEulerAngles = StartCurlingRotation;
        curling.transform.localEulerAngles = new Vector3(0,0,0);
        curling.transform.localPosition = new Vector3(0,0,0);
        transform.eulerAngles = StartRotation;
        timer = originalTimer;
        timerText.color = new Color32(23,122,129,255);
        
    }
}
