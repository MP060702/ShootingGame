using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairItem : BaseItem
{
    public override void OnGetItem(PlayerCharacter character)
    {
        
        PlayerHPSystem system = character.GetComponent<PlayerHPSystem>();
        if(system != null )
        {
            system.Health += 1;
        }

    }
}
