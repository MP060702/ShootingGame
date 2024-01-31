using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefuelItem : BaseItem
{
    public override void OnGetItem(PlayerCharacter character)
    {
 
        PlayerFuelSystem  system = character.GetComponent<PlayerFuelSystem>();
        if (system != null)
        {
            system.Fuel += 10f;

            if(system.Fuel > system.MaxFuel)
            {
                system.Fuel = system.MaxFuel;
            }
        }
    }
}
