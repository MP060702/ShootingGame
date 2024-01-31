using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{   
    static public GameManager Instance;

    public MapManager MapManager;
    public EnemySpawnManager EnemySpawnManager;
    public ItemManager ItemManager;

    public Canvas StageResultCanvas;
    public TMP_Text CurrentScoreText;
    public TMP_Text TimeText;

    [HideInInspector] public bool bStageCleared = false;

    private void Awake()  // 객체 생성시 최초 실행 (그래서 싱글톤을 여기서 생성)
    {
        if (Instance == null)  // 단 하나만 존재하게끔
        {
            Instance = this;  // 객체 생성시 instance에 자기 자신을 넣어줌
        }
        else
            Destroy(this.gameObject);
    }

    public PlayerCharacter GetPlayerCharacter() { return GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacter>(); }

    void Start()
    {
        MapManager.Init(this);
        EnemySpawnManager.Init(this);
    }

    public void GameStart()
    {
        SceneManager.LoadScene("Stage1");
    }

    public void EnemyDies()
    {
        AddScore(10);
    }

    public void StageClear()
    {
        // 스코어 증가
        AddScore(500);
        // 걸린 시간
        float gameStartTime = GameInstance.instance.GameStartTime;
        int elapsedTime = Mathf.FloorToInt(Time.time - gameStartTime);
        // 스테이지 클리어 결과창 : 점수, 시간
        int score = GameInstance.instance.Score;
        // 5초 뒤에 다음 스테이지
        StageResultCanvas.gameObject.SetActive(true);
        CurrentScoreText.text = "CurrentScore" + score;
        TimeText.text = "ElapsedTime" + elapsedTime;

        bStageCleared = true;

        StartCoroutine(LoadNextStageAfterDelay(5f));
    }

    IEnumerator LoadNextStageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        switch (GameInstance.instance.CurrentStageLevel)
        {
            case 1:
                SceneManager.LoadScene("Stage2");
                GameInstance.instance.CurrentStageLevel = 2;
                break;
            case 2:
                SceneManager.LoadScene("Result");
                break;
        }
    }

    public void AddScore(int score)
    {
        GameInstance.instance.Score += score;
    }

    private void Update()
    {
        // 맵 내에 모든 적 유닛 제거.
        if (Input.GetKeyUp(KeyCode.F1))
        {
            GameObject[] enimes = GameObject.FindGameObjectsWithTag("Enemy");

            foreach (GameObject obj in enimes)
            {
                Enemy enemy = obj.GetComponent<Enemy>();

                if (obj != null)
                {
                    enemy.Dead();
                }
            }
        }

        // 공격 업그레이드를 최고 단계로 상승
        if (Input.GetKeyUp(KeyCode.F2))
        {
            GetPlayerCharacter().CurrentWeaponLevel = 4;
            GameInstance.instance.CurrentPlayerWeoponLevel = GetPlayerCharacter().CurrentWeaponLevel;
        }

        // 스킬의 쿨타임 및 횟수를 초기화 시킨다
        if (Input.GetKeyUp(KeyCode.F3))
        {
            GetPlayerCharacter().InitSkillCoolDown();
        }

        // 내구도 초기화
        if (Input.GetKeyUp(KeyCode.F4))
        {
            GetPlayerCharacter().GetComponent<PlayerHPSystem>().InitHealth();
        }

        // 연료 초기화
        if (Input.GetKeyUp(KeyCode.F5))
        {
            GetPlayerCharacter().GetComponent<PlayerFuelSystem>().InitFuel();
        }

        // 스테이지 클리어
        if (Input.GetKeyUp(KeyCode.F6))
        {
            StageClear();
        }
    }
}
