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
            }
        }
    }
}
