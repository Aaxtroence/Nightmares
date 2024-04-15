using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] MonsterPrefabs;
    private float Cooldown = 3f;
    private float Timer = 0f;

    [SerializeField] private Transform LeftSpawner;
    [SerializeField] private Transform RightSpawner;

    [SerializeField] private Transform MonsterParent;
    [SerializeField] private Info TotalInfo;

    private int Difficulty;

    void Start()
    {
        Difficulty = TotalInfo.Difficulty;
        SpawnMonster();
    }

    void Update()
    {
        if(Timer < 15f)
        {
            Timer += Time.deltaTime;
        }
        else
        {
            Timer = 0f;
            Difficulty++;
        }

        if(Cooldown > 0)
        {
            Cooldown -= Time.deltaTime;
        }
        else
        {
            SpawnMonster();

            if(Difficulty == 0)
            {
                Cooldown = Random.Range(3f, 6f);
            }
            else if(Difficulty == 1)
            {
                Cooldown = Random.Range(2f, 4f);
            }
            else if(Difficulty == 2)
            {
                Cooldown = Random.Range(1f, 3f);
            }
            else
            {
                Cooldown = Random.Range(0.25f, 2f);
            }
            
        }
    }

    private void SpawnMonster()
    {
        Transform spawner = (Random.Range(0, 2) == 0) ? LeftSpawner : RightSpawner;
        GameObject selectedPrefab = MonsterPrefabs[Random.Range(0, MonsterPrefabs.Length)];
        Vector3 spawnPosition = spawner.position;

        GameObject newMonster = Instantiate(selectedPrefab, spawnPosition, Quaternion.identity, MonsterParent);
        newMonster.GetComponent<MonsterScript>().Dir = (spawner == LeftSpawner);
    }
}
