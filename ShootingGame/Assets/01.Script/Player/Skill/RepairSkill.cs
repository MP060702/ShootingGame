using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairSkill : BaseSkill
{
    public override void Active()
    {
        base.Active();

        PlayerHPSystem system = GameManager.Instance.GetPlayerCharacter().GetComponent<PlayerHPSystem>();
        Debug.Log(system);

        if (system != null)
        {
            system.Health += 1;

            if (system.Health > system.MaxHealth)
            {
                system.Health = system.MaxHealth;
            }
        }
    }

}
