using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyPattern3 : MonoBehaviour
{
    
    public float MoveSpeed;
    public GameObject Projectile;
    public float ProjectileMoveSpeed;
    Enemy enemey;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {

           
        Vector3 playerPos = GameManager.Instance.GetPlayerCharacter().transform.position;
        Vector3 direction = playerPos - transform.position;
        direction.Normalize();

        transform.Translate(direction * MoveSpeed * Time.deltaTime);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
           if (collision.tag == "Player")
           {
                for (int i = 0; i < 360; i += 50)
                {
                    float angle = i * Mathf.Deg2Rad;
                    Vector3 direction = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0);
 
                    var projectile = Instantiate(Projectile, transform.position, Quaternion.identity);
                    projectile.GetComponent<Projectile>().SetDirection(direction);
                    projectile.GetComponent<Projectile>().MoveSpeed = ProjectileMoveSpeed;

                }
                Destroy(gameObject);
           }
        

    }

}