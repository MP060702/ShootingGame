using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeWeaponItem : BaseItem
{
    public override void OnGetItem(PlayerCharacter character)
    {

        int currentLevel = character.CurrentWeaponLevel;
        int maxLevel = character.MaxWeaponLevel;

        if(currentLevel > maxLevel)
        {
            currentLevel = maxLevel;
        }

        character.CurrentWeaponLevel = Mathf.Clamp(currentLevel + 1, 0, maxLevel);
        GameInstance.instance.CurrentPlayerWeoponLevel = character.CurrentWeaponLevel;
    }
}
