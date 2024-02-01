using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeSkill : BaseSkill
{
    public override void Active()
    {
        base.Active();
        GameObject enemy = GameObject.FindWithTag("Enemy");
        Enemy enemyComponent = enemy.GetComponent<Enemy>();

        enemyComponent.bisFreeze = true;
    }
}
