using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{    
    public LayerMask whatIsProp; //������ �ʴ� �༮���� ���͸��ؼ� ���ϴ� �༮�鸸 ������ ���� �� = layerMask
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
        //������ ���� �׷��� ��ġ�� �ݶ��̴� ��������(���� �׸��� ��ġ, ���� �׸��� ������, ���̾� ����ũ�� �ش������ �ִ� �� ������)
        Collider[] colliders = Physics.OverlapSphere(transform.position,explosionRadius, whatIsProp);

        for (int i = 0; i < colliders.Length; i++)
        {
            Rigidbody targetRigidbody = colliders[i].GetComponent<Rigidbody>();

            //� ������ ���� ��ġ, ���߷� �׸��� �ݰ��� �������ָ� �� �ڽ��� ��ġ�� ������ �������� ���� ��ŭ �������ִ��� ����ؼ� ������ ƨ�ܳ����� �ۿ�
            targetRigidbody.AddExplosionForce(explosionForce, transform.position, explosionRadius);

            Prop targetProp = colliders[i].GetComponent<Prop>();

            float damage = CalculateDamage(colliders[i].transform.position);

            targetProp.TakeDamage(damage);
        }

        //��ƼŬ�� �θ� ���ش�. -> ball���� ��������
        explosionParticle.transform.parent = null;

        explosionParticle.Play();
        explosionAudio.Play();

        //Ư��ȿ�� duration(����ð�) �� ����Ǹ� ����
        Destroy(explosionParticle.gameObject,explosionParticle.duration);
        Destroy(gameObject);
    }

    //�� ��ġ���� �Ÿ� ����Ͽ� ���� ������
    private float CalculateDamage(Vector3 targetPosition){
        //�� ��ġ���� Ÿ�ٱ����� �Ÿ�
        Vector3 explosionToTarget = targetPosition - transform.position;
        //magnitude�� ������ ����(��Ÿ��󽺷� ���� �ϳ��� ���� ����), ���� ��ǥ�� ������ �Ÿ�
        float distance = explosionToTarget.magnitude;
        
        float edgeToCenterDistance = explosionRadius - distance;
        //�������� ��ŭ ������ ������ ����
        float percentage = edgeToCenterDistance/explosionRadius;

        float damage = maxDamage * percentage;
        //�ݶ��̴� ���Ƕ����� Ȥ�ö� ���̳ʽ� ���� ���� ��� damage�� 0���� ũ�� �׳� �Ҵ�, ������ 0�� �Ҵ�
        damage = Mathf.Max(0, damage);

        return damage;
    }
}
