using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{    
    public LayerMask whatIsProp; //원하지 않는 녀석들을 필터링해서 원하는 녀석들만 가지고 오는 것 = layerMask
    public ParticleSystem explosionParticle;
    public AudioSource explosionAudio;

    public float maxDamage = 100f;
    public float explosionForce = 1000f;
    public float lifeTIme = 10f;
    public float explosionRadius = 20f;

    void Start() {
        Destroy(gameObject,lifeTIme);
    }

    private void OnTriggerEnter(Collider other) {
        //가상의 구를 그려서 겹치는 콜라이더 가져오기(구를 그리는 위치, 구를 그리는 반지름, 레이어 마스크로 해당사항이 있는 놈만 가져옴)
        Collider[] colliders = Physics.OverlapSphere(transform.position,explosionRadius, whatIsProp);

        for (int i = 0; i < colliders.Length; i++)
        {
            Rigidbody targetRigidbody = colliders[i].GetComponent<Rigidbody>();

            //어떤 지점의 폭발 위치, 폭발력 그리고 반경을 지정해주면 나 자신의 위치가 폭발의 원점으로 부터 얼만큼 떨어져있는지 계산해서 스스로 튕겨나가는 작용
            targetRigidbody.AddExplosionForce(explosionForce, transform.position, explosionRadius);

            Prop targetProp = colliders[i].GetComponent<Prop>();

            float damage = CalculateDamage(colliders[i].transform.position);

            targetProp.TakeDamage(damage);
        }

        //파티클의 부모를 없앤다. -> ball에서 나오겠지
        explosionParticle.transform.parent = null;

        explosionParticle.Play();
        explosionAudio.Play();

        //특수효과 duration(재생시간) 다 재생되면 삭제
        Destroy(explosionParticle.gameObject,explosionParticle.duration);
        Destroy(gameObject);
    }

    //내 위치에서 거리 계산하여 차등 데미지
    private float CalculateDamage(Vector3 targetPosition){
        //내 위치에서 타겟까지의 거리
        Vector3 explosionToTarget = targetPosition - transform.position;
        //magnitude는 벡터의 길이(피타고라스로 실제 하나의 수로 뽑음), 나와 목표물 사이의 거리
        float distance = explosionToTarget.magnitude;
        
        float edgeToCenterDistance = explosionRadius - distance;
        //안쪽으로 얼만큼 들어갔는지 정도를 구함
        float percentage = edgeToCenterDistance/explosionRadius;

        float damage = maxDamage * percentage;
        //콜라이더 부피때문에 혹시라도 마이너스 값이 나올 경우 damage가 0보다 크면 그냥 할당, 작으면 0이 할당
        damage = Mathf.Max(0, damage);

        return damage;
    }
}
