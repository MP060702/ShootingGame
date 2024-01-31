using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Health = 3f;
    public float AttackDamage = 1f;
    bool bIsDead = false;
    public bool bMustSpawnItem = false;

    public GameObject ExplodeFX;

    void Start()
    {
        
    }

    public void Dead()
    {
        if (!bIsDead)
        {
            if (bMustSpawnItem)
                GameManager.Instance.ItemManager.SpawnRandomItme(0, 3, transform.position);
            else
                GameManager.Instance.ItemManager.SpawnRandomItem(transform.position);

            bIsDead = true;
            Instantiate(ExplodeFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            Health -= 1;

            if(Health < 0)
            {
                Dead();
            }

            StartCoroutine(HitFlick());
            Destroy(collision.gameObject);

        }
    }

    IEnumerator HitFlick()
    {
        int flick = 0;
        while (flick < 1)
        {
            this.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(0.1f);

            this.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            yield return new WaitForSeconds(0.1f);

            flick++;
        }
    }
}
