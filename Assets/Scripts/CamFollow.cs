using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public enum State{
        idle,Ready,Tracking
    }
    private State state{
        set{
            switch(value){
                case State.idle:
                targetZoomSize = roundReadyZoomSize;
                break;
                case State.Ready:
                targetZoomSize = readyShotZoomSize;
                break;
                case State.Tracking:
                targetZoomSize = trackingZoomSize;
                break;
            }
        }
    }

    public Transform target;
    public float smoothTime = 0.2f;
    private Vector3 lastMovingVelocity;
    private Vector3 targetPosition;

    private Camera cam;
    private float targetZoomSize = 5;
    private const float roundReadyZoomSize = 14.5f;
    private const float readyShotZoomSize = 5f;
    private const float trackingZoomSize = 10f; //const는 절대 안바꾼다는 약속, 안바꿔짐

    private float lastZoomSpeed;


    void Awake() {
        cam = GetComponentInChildren<Camera>(); //Rig의 자식을 다 뒤져서
        state = State.idle; // state함수에서 state.idle 실행       
        
    }

    private void Move(){
        targetPosition = target.transform.position;
        //(현재 위치 값, 가고 싶은 위치 값, 마지막순간의 속도 값, 지연 시간) , ref는 값이 변경되면 고대로 받아서 나오는 것(갱신)
        Vector3 smoothPosition = Vector3.SmoothDamp(transform.position,targetPosition, ref lastMovingVelocity,smoothTime); //카메라가 한번에 이동하는 것이 아닌 시간에 따라서 스무스하게 이어주는 역할 

        transform.position = targetPosition;
    }

    private void Zoom(){
        float smoothZoomSize = Mathf.SmoothDamp(cam.orthographicSize,targetZoomSize,ref lastZoomSpeed,smoothTime);

        cam.orthographicSize = smoothZoomSize;
    }

    //fixed는 정확한 시간 간격으로 업데이트 그냥은 매 프레임마다
    private void FixedUpdate(){
        if(target != null)
        {
            Move();
            Zoom();
        }
    }

    public void Reset() {
        state = State.idle;
    }

    public void SetTarget(Transform newTarget, State newState){
        target = newTarget;
        state = newState;
    }
}
