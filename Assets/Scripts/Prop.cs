using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop : MonoBehaviour
{
    public int score = 5;
    public ParticleSystem explosionParticle;
    public float hp = 10;

    public void TakeDamage(float damage){
        hp -= damage;

        if(hp <= 0){
            //Instantiate에 원본을 넣으면 원본 게임오브젝트를 복사해줌, 원본, 위치, 회전 값을 줄 수도 있음
            ParticleSystem instance = Instantiate(explosionParticle,transform.position,transform.rotation);

            AudioSource explosionAudio = instance.GetComponent<AudioSource>();
            explosionAudio.Play();

            //인스턴스 삭제해줘 인스턴스 듀레이션 지나면
            Destroy(instance.gameObject, instance.duration);
            gameObject.SetActive(false);
        }
    }
}
