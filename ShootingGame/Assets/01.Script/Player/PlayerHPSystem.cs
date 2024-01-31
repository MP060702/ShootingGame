using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHPSystem : MonoBehaviour
{
    public int Health;
    public int MaxHealth = 3;

    void Start()
    {   
        Health = GameInstance.instance.CurrentPlayerHp;
    }

    public void InitHealth()
    {
        Health = MaxHealth;
        GameInstance.instance.CurrentPlayerHp = Health;
    }

    IEnumerator HitFlick()
    {
        int flick = 0;
        while (flick < 5)
        {
            GameManager.Instance.GetPlayerCharacter().GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.2f);
            yield return new WaitForSeconds(0.1f);

            GameManager.Instance.GetPlayerCharacter().GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            yield return new WaitForSeconds(0.1f);

            flick++;
        }
       
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy")
           && !GameManager.Instance.GetPlayerCharacter().Invincibility
           && !GameManager.Instance.bStageCleared)
        {
            Health -= 1;

            StartCoroutine(HitFlick());

            Destroy(collision.gameObject);

            if(Health <= 0)
            {
                GameManager.Instance.GetPlayerCharacter().DeadProcess();
            }
        }
        if (collision.gameObject.CompareTag("Item"))
        {
            if(Health > MaxHealth)
            {
                Health = MaxHealth;
            }
        }
        GameInstance.instance.CurrentPlayerHp = Health;
    }
}
