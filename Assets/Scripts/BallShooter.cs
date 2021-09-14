using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallShooter : MonoBehaviour
{
    public CamFollow cam;
    public Rigidbody ball;
    public Transform firePos;
    public Slider powerSlider;
    public AudioSource shootingAudio;
    public AudioClip fireClip;
    public AudioClip chargingClip;
    public float minForce = 15f;
    public float maxForce = 30f;
    public float chargingTime = 0.75f;

    private float currentForce;
    private float chargeSpeed;
    private bool fired;

    //OnEnable은 컴포넌트가 꺼져있다가 켜지는 상황이라면 자동으로 한 번 발동, 물론 첫 실행에서도 발동
    private void OnEnable() {
        //매번 다음 라운드에서 초기화
        currentForce = minForce;
        powerSlider.value = minForce;
        fired = false;
    }

    private void Start() {
        chargeSpeed = (maxForce - minForce) / chargingTime; //거리 = 속도 * 시간        
    }

    private void Update() {

        if(fired){
            return; //한번 발사하면 끝
        }

        powerSlider.value = minForce; // 매 프레임 최소값으로 할당 후 밑의 코드로 덮어씌우고 다시 최소값으로 초기화

        //힘이 충분히 많아서 발사를 무조건 해야하는 순간
        if(currentForce >= maxForce && !fired){
            currentForce = maxForce;
            //발사 처리
            Fire();
        }
        //발사버튼을 막 누른 순간
        else if(Input.GetButtonDown("Fire1")){
            //fired = false; 로 하면 연사 가능
            currentForce = minForce;

            shootingAudio.clip = chargingClip;
            shootingAudio.Play();
        }        
        //누르는 동안
        else if(Input.GetButton("Fire1") && !fired){
            currentForce = currentForce + chargeSpeed * Time.deltaTime;

            powerSlider.value = currentForce;
        }
        //떼어내는 순간
        else if(Input.GetButtonUp("Fire1") && !fired){
            //발사 처리
            Fire();
        }
    }

    void Fire(){
        fired = true;

        Rigidbody ballInstance = Instantiate(ball, firePos.position, firePos.rotation); //공을 새로 찍어냄

        ballInstance.velocity = currentForce * firePos.forward; // firopos의 앞 방향으로

        shootingAudio.clip = fireClip;
        shootingAudio.Play();

        currentForce = minForce;

        cam.SetTarget(ballInstance.transform,CamFollow.State.Tracking);
    }

}
