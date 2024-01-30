using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{   
    static public GameManager Instance;

    public MapManager MapManager;
    public EnemySpawnManager EnemySpawnManager;

    [HideInInspector] public bool bStageCleared = false;

    public GameObject Player;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(this.gameObject);
    }
    
    public PlayerCharacter GetPlayerCharacter() { return Player.GetComponent<PlayerCharacter>(); }

    void Start()
    {
        MapManager.Init(this);
        
        EnemySpawnManager.Init(this);
    }
}
