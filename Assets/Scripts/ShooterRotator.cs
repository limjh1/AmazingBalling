using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterRotator : MonoBehaviour
{
    private enum RotateState
    {
        Idle,Vertical,Horizontal,Ready
    }
    private RotateState state = RotateState.Idle;
    public float verticalRotateSpeed = 360f;
    public float horizontalRotateSpeed = 360f;
    
    public BallShooter ballShooter;

    void Update() {
        if(state == RotateState.Idle){
            if(Input.GetButtonDown("Fire1")){
                state = RotateState.Horizontal;
            }            
        } else if(state == RotateState.Horizontal){
            if(Input.GetButton("Fire1")){
                transform.Rotate(new Vector3(0,horizontalRotateSpeed * Time.deltaTime,0));
            } else if(Input.GetButtonUp("Fire1")){
            state = RotateState.Vertical;    
            }    
        } else if(state == RotateState.Vertical){
            if(Input.GetButton("Fire1")){
                transform.Rotate(new Vector3(-verticalRotateSpeed * Time.deltaTime,0,0));
            }
            else if(Input.GetButtonUp("Fire1")){
                state = RotateState.Ready;
                ballShooter.enabled = true; //레디가 되었을 때 볼 슈터가 트루가 된다
            }
        }
    }

    private void OnEnable() {
        transform.rotation = Quaternion.identity; //0도 0도 0도로 회전각도 리셋
        state = RotateState.Idle;
        ballShooter.enabled = false; //초기화 될 때 꺼버림
    }
}
