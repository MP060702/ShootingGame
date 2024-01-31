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

    private void Awake()  // ��ü ������ ���� ���� (�׷��� �̱����� ���⼭ ����)
    {
        if (Instance == null)  // �� �ϳ��� �����ϰԲ�
        {
            Instance = this;  // ��ü ������ instance�� �ڱ� �ڽ��� �־���
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
        // ���ھ� ����
        AddScore(500);
        // �ɸ� �ð�
        float gameStartTime = GameInstance.instance.GameStartTime;
        int elapsedTime = Mathf.FloorToInt(Time.time - gameStartTime);
        // �������� Ŭ���� ���â : ����, �ð�
        int score = GameInstance.instance.Score;
        // 5�� �ڿ� ���� ��������
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
        // �� ���� ��� �� ���� ����.
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

        // ���� ���׷��̵带 �ְ� �ܰ�� ���
        if (Input.GetKeyUp(KeyCode.F2))
        {
            GetPlayerCharacter().CurrentWeaponLevel = 4;
            GameInstance.instance.CurrentPlayerWeoponLevel = GetPlayerCharacter().CurrentWeaponLevel;
        }

        // ��ų�� ��Ÿ�� �� Ƚ���� �ʱ�ȭ ��Ų��
        if (Input.GetKeyUp(KeyCode.F3))
        {
            GetPlayerCharacter().InitSkillCoolDown();
        }

        // ������ �ʱ�ȭ
        if (Input.GetKeyUp(KeyCode.F4))
        {
            GetPlayerCharacter().GetComponent<PlayerHPSystem>().InitHealth();
        }

        // ���� �ʱ�ȭ
        if (Input.GetKeyUp(KeyCode.F5))
        {
            GetPlayerCharacter().GetComponent<PlayerFuelSystem>().InitFuel();
        }

        // �������� Ŭ����
        if (Input.GetKeyUp(KeyCode.F6))
        {
            StageClear();
        }
    }
}
