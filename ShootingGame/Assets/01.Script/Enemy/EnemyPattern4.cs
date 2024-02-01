using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyPattern4 : MonoBehaviour
{
    Enemy enemy;
    public float MoveSpeed;
    public float AttackStopTime;
    public float MoveTime;
    public GameObject Projectile;
    public float ProjectileMoveSpeed;
    public GameObject BulletPos1;
    public GameObject BulletPos2;

    private bool _isAttack = false;

    void Start()
    {   
        enemy = GetComponent<Enemy>();
        StartCoroutine(Attack());
    }

    void Update()
    {
        if (false == _isAttack)
            Move();
    }

    IEnumerator Attack()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f); // 1초 기다림

            

            Vector3 playerPos = GameManager.Instance.GetPlayerCharacter().transform.position;
            Vector3 direction = playerPos - transform.position;
            direction.Normalize();

            if (enemy.bisFreeze != true)
            { 
                var projectile1 = Instantiate(Projectile, BulletPos1.transform.position, Quaternion.identity);
                projectile1.GetComponent<Projectile>().SetDirection(direction);
                projectile1.GetComponent<Projectile>().MoveSpeed = ProjectileMoveSpeed;

                var projectile2 = Instantiate(Projectile, BulletPos2.transform.position, Quaternion.identity);
                projectile2.GetComponent<Projectile>().SetDirection(direction);
                projectile2.GetComponent<Projectile>().MoveSpeed = ProjectileMoveSpeed;
            }

            _isAttack = true;

            yield return new WaitForSeconds(AttackStopTime); // 1초 기다림

            _isAttack = false;

            yield return new WaitForSeconds(MoveTime); // 3초 동안 움직임
        }
    }

    void Move()
    {
        if (enemy.bisFreeze != true)
        {
            transform.position -= new Vector3(0f, MoveSpeed * Time.deltaTime, 0f);
            
        }
    }
}