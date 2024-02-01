using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddOn : MonoBehaviour
{
    public GameObject Projectile;

    [HideInInspector]
    public Transform FollowTargetTransform;
    private Transform TargetEnemyTransform;

    public int FollowSpeed = 20;
    public float AttackInterval = 0.5f;


    private void Start()
    {
        StartCoroutine(ShootProjectile());
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, FollowTargetTransform.position,FollowSpeed *Time.deltaTime);
    }

    IEnumerator ShootProjectile()
    {
        
        SearchEnemy();

        Vector3 direction = Vector3.up;

        if(TargetEnemyTransform is not null)
        {
            GameObject instance2 = Instantiate(Projectile, transform.position, Quaternion.identity);
            instance2.GetComponent<Projectile>().SetDirection(TargetEnemyTransform.position);
            instance2.GetComponent<Projectile>().MoveSpeed = 10f;

        }
        else
        {
            GameObject instance = Instantiate(Projectile, transform.position, Quaternion.identity);
            instance.GetComponent<Projectile>().SetDirection(direction.normalized);
            instance.GetComponent<Projectile>().MoveSpeed = 10f;
        }

        yield return new WaitForSeconds(AttackInterval);
        StartCoroutine(ShootProjectile());
    }

    private void SearchEnemy()
    {
        TargetEnemyTransform = null;

        float distance = float.MaxValue;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject obj in enemies)
        {
            if(obj?.GetComponent<Projectile>() == null && obj?.GetComponent<Meteor>() == null)
            {
                float targetDistance = Vector3.Distance(transform.position, obj.transform.position);
                if(distance >= targetDistance)
                {
                    TargetEnemyTransform = obj.transform;
                    distance = Mathf.Min(targetDistance, distance);
                }
            }
        }
    }
}
