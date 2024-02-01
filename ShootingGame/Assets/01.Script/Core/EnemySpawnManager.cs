using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager :BaseManager
{
    public GameObject[] Enemys;
    public Transform[] EnemySpawnTransform;
    public GameObject Meteor;
    public float CoolDownTime;
    public int MaxSpawnEnemyCount;

    private int _spawnCount = 0;
    public int BossSpawnCount = 10;

    private bool _bSpawnBoss = false;

    public GameObject BossA;
    public GameObject BossB;

    public override void Init(GameManager gameManager)
    {
        base.Init(gameManager);

        StartCoroutine(SpawnEnemy());
        StartCoroutine(SpawnMeteor());
    }

    IEnumerator SpawnEnemy()
    {
        while (!_bSpawnBoss)
        {
            yield return new WaitForSeconds(CoolDownTime);
            
            int spawnCount = Random.Range(0, EnemySpawnTransform.Length);
            List<int> availablePosition = new List<int>(EnemySpawnTransform.Length);
            for(int i = 0; i < EnemySpawnTransform.Length; i++)
            {
                availablePosition.Add(i);
            }

            for(int i = 0; i < spawnCount ; i++)
            {
                int randomEnemys = Random.Range(0, Enemys.Length);
                int randomPositionIndex = Random.Range(0, availablePosition.Count - 1);
                int randomPosition = availablePosition[randomPositionIndex];

                availablePosition.RemoveAt(randomPositionIndex);

                Instantiate(Enemys[randomEnemys], EnemySpawnTransform[randomPosition].position, Quaternion.identity);

            }

            _spawnCount += spawnCount;
            
            if(_spawnCount > BossSpawnCount)
            {
                switch (GameInstance.instance.CurrentStageLevel)
                {
                    case 1:
                        _bSpawnBoss = true;
                        Instantiate(BossA, new Vector3(EnemySpawnTransform[1].position.x, EnemySpawnTransform[1].position.y + 1f, 0), Quaternion.identity);
                        break;
                    case 2:
                        _bSpawnBoss = true;
                        Instantiate(BossB, new Vector3(EnemySpawnTransform[1].position.x, EnemySpawnTransform[1].position.y + 1f, 0), Quaternion.identity);
                        break;
                }
   
            }
        }
    }

    IEnumerator SpawnMeteor()
    {
        while (true)
        {
            yield return new WaitForSeconds(CoolDownTime);

            List<int> availablePosition1 = new List<int>(EnemySpawnTransform.Length);
            for (int i = 0; i < EnemySpawnTransform.Length; i++)
            {
                availablePosition1.Add(i);
            }
            int randomPositionIndex1 = Random.Range(0, availablePosition1.Count - 1);
            int randomPosition1 = availablePosition1[randomPositionIndex1];

            availablePosition1.RemoveAt(randomPositionIndex1);

            Instantiate(Meteor, EnemySpawnTransform[randomPosition1].position, Quaternion.identity);
        }
    }
}
