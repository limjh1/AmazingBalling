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

    //OnEnable�� ������Ʈ�� �����ִٰ� ������ ��Ȳ�̶�� �ڵ����� �� �� �ߵ�, ���� ù ���࿡���� �ߵ�
    private void OnEnable() {
        //�Ź� ���� ���忡�� �ʱ�ȭ
        currentForce = minForce;
        powerSlider.value = minForce;
        fired = false;
    }

    private void Start() {
        chargeSpeed = (maxForce - minForce) / chargingTime; //�Ÿ� = �ӵ� * �ð�        
    }

    private void Update() {

        if(fired){
            return; //�ѹ� �߻��ϸ� ��
        }

        powerSlider.value = minForce; // �� ������ �ּҰ����� �Ҵ� �� ���� �ڵ�� ������ �ٽ� �ּҰ����� �ʱ�ȭ

        //���� ����� ���Ƽ� �߻縦 ������ �ؾ��ϴ� ����
        if(currentForce >= maxForce && !fired){
            currentForce = maxForce;
            //�߻� ó��
            Fire();
        }
        //�߻��ư�� �� ���� ����
        else if(Input.GetButtonDown("Fire1")){
            //fired = false; �� �ϸ� ���� ����
            currentForce = minForce;

            shootingAudio.clip = chargingClip;
            shootingAudio.Play();
        }        
        //������ ����
        else if(Input.GetButton("Fire1") && !fired){
            currentForce = currentForce + chargeSpeed * Time.deltaTime;

            powerSlider.value = currentForce;
        }
        //����� ����
        else if(Input.GetButtonUp("Fire1") && !fired){
            //�߻� ó��
            Fire();
        }
    }

    void Fire(){
        fired = true;

        Rigidbody ballInstance = Instantiate(ball, firePos.position, firePos.rotation); //���� ���� ��

        ballInstance.velocity = currentForce * firePos.forward; // firopos�� �� ��������

        shootingAudio.clip = fireClip;
        shootingAudio.Play();

        currentForce = minForce;

        cam.SetTarget(ballInstance.transform,CamFollow.State.Tracking);
    }

}
