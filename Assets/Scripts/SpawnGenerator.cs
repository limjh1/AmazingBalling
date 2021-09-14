using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGenerator : MonoBehaviour
{
    public GameObject[] propPrefabs;
    private BoxCollider area;
    public int count = 100; //100���� ����

    //���ӵ� ���⿡ �ְ� ���� ��ȸ�� ������ ��ġ �缳��
    private List<GameObject> props = new List<GameObject>();
    
    void Start()
    {
        area = GetComponent<BoxCollider>();

        for (int i = 0; i < count; i++)
        {
            //������ �Լ�
            Spawn();
        }

        area.enabled = false; //������Ʈ�� box collider�� ���� �浹 ���� �� ���� �־
    }

    private void Spawn(){
        int selection = Random.Range(0,propPrefabs.Length); //MAX���� ���� ����, ������ ������ �Ҽ��� �Ҽ��� ��ȯ��

        GameObject selectedPrefab = propPrefabs[selection];

        Vector3 spawnPos= GetRandomPosition();

        GameObject instance = Instantiate(selectedPrefab,spawnPos,Quaternion.identity); // (����, ��ġ, ȸ��(identitiy = 0,0,0))

        props.Add(instance);        
    }

    private Vector3 GetRandomPosition(){
        Vector3 basePosition = transform.position; //Ʈ������ ��ġ
        Vector3 size = area.size; //�ڽ� �ݶ��̴��� ������

        float posX = basePosition.x + Random.Range(-size.x/2f,size.x/2f); //�� �ڽ��� �������� ���� �� ���̳ʽ�~�÷���
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
