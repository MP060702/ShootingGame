using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSkill : BaseSkill
{
    public override void Active()
    {
        base.Active();
        GameObject[] enimes = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject obj in enimes)
        {   
            Enemy enemy = obj.GetComponent<Enemy>();

            if(obj != null)
            {
                enemy.Dead();
            }
        }
    }
    


    
}
