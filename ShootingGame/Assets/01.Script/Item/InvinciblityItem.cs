using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvinciblityItem : BaseItem
{
    public override void OnGetItem(PlayerCharacter character)
    {
        character.SetInvincibility(true);
    }
}
