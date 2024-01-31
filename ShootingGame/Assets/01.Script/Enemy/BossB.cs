using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossB : MonoBehaviour
{
    public GameObject Projectile;
    public float ProjectileMoveSpeed = 5.0f;
    public float FireRate = 2.0f;
    public float MoveSpeed = 1.0f;

    private int _currentPatternIndex = 0;
    private bool _movingRight = true;
    private bool _bCanMove = false;
    private Vector3 _originPosition;

    private void Start()
    {
        _originPosition = transform.position;
        StartCoroutine(MoveDownAndStartPattern());
    }

    private IEnumerator MoveDownAndStartPattern()
    {
        while (transform.position.y > _originPosition.y - 1f)
        {
            transform.Translate(Vector3.down * MoveSpeed * Time.deltaTime);
            yield return null;
        }

        _bCanMove = true;
        InvokeRepeating("NextPattern", 2f, FireRate);

    }

    private void Update()
    {
        if (_bCanMove)
            MoveSideways();
    }

    private void NextPattern()
    {
        _currentPatternIndex = (_currentPatternIndex + 1) % 4;

        switch (_currentPatternIndex)
        {
            case 0:
                Pattern1();
                break;
            case 1:
                Pattern2();
                break;
            case 2:
                StartCoroutine(Pattern3());
                break;
            case 3:
                Pattern4();
                break;
        }
    }

    private void MoveSideways()
    {
        if (_movingRight)
        {
            transform.Translate(Vector2.right * MoveSpeed * Time.deltaTime);
            if (transform.position.x > 2.4f)
            {
                _movingRight = false;
            }
        }
        else
        {
            transform.Translate(Vector2.left * MoveSpeed * Time.deltaTime);
            if (transform.position.x < -2.4f)
            {
                _movingRight = true;
            }
        }
    }

    private void StartMovingSideways()
    {
        StartCoroutine(MovingSidewaysRountine());
    }

    private IEnumerator MovingSidewaysRountine()
    {
        while (true)
        {
            MoveSideways();
            yield return null;
        }
    }

    public void ShootProjectile(Vector3 position, Vector3 direction)
    {
        GameObject instance = Instantiate(Projectile, position, Quaternion.identity);
        Projectile projectile = instance.GetComponent<Projectile>();

        projectile.MoveSpeed = ProjectileMoveSpeed;
        projectile.SetDirection(direction.normalized);
    }

    private void Pattern1()
    {
        int numBullets1 = 30;
        float angleStep1 = 360.0f / numBullets1;

        for (int i = 0; i < numBullets1; i++)
        {
            float angle1 = i * angleStep1;
            float radian1 = angle1 * Mathf.Deg2Rad;
            Vector3 direction1 = new Vector3(Mathf.Cos(radian1), Mathf.Sin(radian1), 0f);

            ShootProjectile(transform.position, direction1);
        }
    }

    private void Pattern2()
    {
        
    }

    private IEnumerator Pattern3()
    {
        int numBullets = 5;
        float interval = 1.0f;

        for (int i = 0; i < numBullets; i++)
        {
            Vector3 playerdirection = GameManager.Instance.GetPlayerCharacter().GetComponent<Transform>().position;
            Vector3 direction3 = playerdirection - transform.position;
            ShootProjectile(transform.position, direction3);
            yield return new WaitForSeconds(interval);
        }
    }

    private void Pattern4()
    {
        
    }

    private void OnDestroy()
    {
        GameManager.Instance.StageClear();
    }
}
