using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGenerator : MonoBehaviour
{
    public GameObject[] propPrefabs;
    private BoxCollider area;
    public int count = 100; //100개의 프롭

    //프롭들 여기에 넣고 라운드 순회할 때마다 위치 재설정
    private List<GameObject> props = new List<GameObject>();
    
    void Start()
    {
        area = GetComponent<BoxCollider>();

        for (int i = 0; i < count; i++)
        {
            //생성용 함수
            Spawn();
        }

        area.enabled = false; //오브젝트의 box collider를 꺼줌 충돌 방해 할 수도 있어서
    }

    private void Spawn(){
        int selection = Random.Range(0,propPrefabs.Length); //MAX값은 빼서 측정, 정수면 정수만 소수면 소수만 반환함

        GameObject selectedPrefab = propPrefabs[selection];

        Vector3 spawnPos= GetRandomPosition();

        GameObject instance = Instantiate(selectedPrefab,spawnPos,Quaternion.identity); // (원본, 위치, 회전(identitiy = 0,0,0))

        props.Add(instance);        
    }

    private Vector3 GetRandomPosition(){
        Vector3 basePosition = transform.position; //트렌스폼 위치
        Vector3 size = area.size; //박스 콜라이더의 사이즈

        float posX = basePosition.x + Random.Range(-size.x/2f,size.x/2f); //나 자신의 사이즈의 절반 값 마이너스~플러스
        float posY = basePosition.y + Random.Range(-size.y/2f,size.y/2f);
        float poxZ = basePosition.z + Random.Range(-size.z/2f,size.z/2f);

        Vector3 spawnPos = new Vector3(posX,posY,poxZ);

        return spawnPos;   
    }

    public void Reset(){
        for (int i = 0; i < props.Count; i++)
        {
            props[i].transform.position = GetRandomPosition();
            props[i].SetActive(true);
        }
    }
}
