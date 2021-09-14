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
    private const float trackingZoomSize = 10f; //const�� ���� �ȹٲ۴ٴ� ���, �ȹٲ���

    private float lastZoomSpeed;


    void Awake() {
        cam = GetComponentInChildren<Camera>(); //Rig�� �ڽ��� �� ������
        state = State.idle; // state�Լ����� state.idle ����       
        
    }

    private void Move(){
        targetPosition = target.transform.position;
        //(���� ��ġ ��, ���� ���� ��ġ ��, ������������ �ӵ� ��, ���� �ð�) , ref�� ���� ����Ǹ� ���� �޾Ƽ� ������ ��(����)
        Vector3 smoothPosition = Vector3.SmoothDamp(transform.position,targetPosition, ref lastMovingVelocity,smoothTime); //ī�޶� �ѹ��� �̵��ϴ� ���� �ƴ� �ð��� ���� �������ϰ� �̾��ִ� ���� 

        transform.position = targetPosition;
    }

    private void Zoom(){
        float smoothZoomSize = Mathf.SmoothDamp(cam.orthographicSize,targetZoomSize,ref lastZoomSpeed,smoothTime);

        cam.orthographicSize = smoothZoomSize;
    }

    //fixed�� ��Ȯ�� �ð� �������� ������Ʈ �׳��� �� �����Ӹ���
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
