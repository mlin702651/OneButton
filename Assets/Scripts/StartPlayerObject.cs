using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class StartPlayerObject : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject curling;
    [SerializeField] private GameObject pole;
    [SerializeField] private Ease poleEase = Ease.Linear;
    [SerializeField] private Slider Player1Slider;
    [SerializeField] private Slider Player2Slider;
    [SerializeField] private RectTransform buttonA;
    [SerializeField] private RectTransform buttonL;
    [SerializeField] private Ease ButtonAEase = Ease.Linear;
    [SerializeField] private Ease ButtonLEase = Ease.Linear;


    private Vector3 poleOriginPosition = new Vector3(0,0,0);

    public PlayerInput controls;
    private bool ifAPress = false;
    private bool ifLPress = false;

    void Awake()
    {
        controls = new PlayerInput();
        controls.Player.CurlingRotate.performed += ctx => StartAKey();
        controls.Player.CurlingRotate.canceled += ctx => EndAKey();

        controls.Player.BrushAddSpeed.performed += ctx => StartLKey();

        
    }
    
    void Start()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append( curling.transform.DORotate(new Vector3(0,10f,0), 0.1f ).SetEase(Ease.Linear) );
        sequence.SetLoops(-1, LoopType.Incremental);

        // poleOriginPosition = pole.transform.position;
        // Sequence sequence2 = DOTween.Sequence();
        // sequence2.Append( pole.transform.DOMove(poleOriginPosition + new Vector3(2.5f,0,0), 0.5f ).SetEase(poleEase) );
        // sequence2.Append( pole.transform.DOMove(poleOriginPosition, 0.5f ).SetEase(poleEase) );
        // sequence2.SetLoops(-1, LoopType.Restart);

        Sequence sequence2 = DOTween.Sequence();
        sequence2.Append( pole.transform.DORotate(new Vector3(0,-10f,0), 0.1f ).SetEase(Ease.Linear) );
        sequence2.SetLoops(-1, LoopType.Incremental);

        Sequence sequence3 = DOTween.Sequence();
        sequence3.Append( buttonA.DOAnchorPosY(-340, 0.5f ).SetEase(ButtonAEase) );
        sequence3.Append( buttonA.DOAnchorPosY(-340, 2.0f ).SetEase(ButtonAEase) );
        sequence3.Append( buttonA.DOAnchorPosY(-300, 0.5f ).SetEase(ButtonAEase) );
        sequence3.SetLoops(-1, LoopType.Restart);

        Sequence sequence4 = DOTween.Sequence();
        sequence4.Append( buttonL.DOAnchorPosY(-340, 0.5f ).SetEase(ButtonLEase));
        sequence4.Append( buttonL.DOAnchorPosY(-300, 0.5f ).SetEase(ButtonLEase));
        sequence4.SetLoops(-1, LoopType.Restart);

        Player1Slider.value = 1;
        Player2Slider.value = 1;

    }
    private void OnEnable(){
        controls.Enable();
    }
    private void OnDisable(){
        controls.Disable();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(ifAPress){
            Player1Slider.value-= 0.05f;
        }
        if(ifLPress){
            ifLPress = false;
            Player2Slider.value-= 0.15f;
        }

        if(Player1Slider.value<=0&&Player2Slider.value<=0) {
            Debug.Log("Start");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        Player1Slider.value+= 0.03f;
        Player2Slider.value+= 0.01f;


    }

    void StartAKey(){
        ifAPress  = true;
    }
    void EndAKey(){
        ifAPress = false;
    }

    void StartLKey(){
        ifLPress = true;
    }
}
