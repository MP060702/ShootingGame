using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldSkill : BaseSkill
{
    public GameObject Shield;

    public override void Active()
    {
        base.Active();
        for(int i = 0; i < 360; i+= 30) 
        {
            float angle = i * Mathf.Deg2Rad;
            Vector3 direction = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0);
            SpawnShield(transform.position, direction);
        }
    }
    public void SpawnShield(Vector3 position, Vector3 direction)
    {
        GameObject instance = Instantiate(Shield, position, Quaternion.identity);
        Shield shield = instance.GetComponent<Shield>();
        instance.transform.parent = gameObject.transform;
        if(shield != null )
        {
            shield.SetDirection(direction);
        }
    }
}
