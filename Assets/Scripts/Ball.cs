using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
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
        //파티클의 부모를 없앤다. -> ball에서 나오겠지
        explosionParticle.transform.parent = null;

        explosionParticle.Play();
        explosionAudio.Play();

        //특수효과 duration(재생시간) 다 재생되면 삭제
        Destroy(explosionParticle.gameObject,explosionParticle.duration);
        Destroy(gameObject);
    }
}
