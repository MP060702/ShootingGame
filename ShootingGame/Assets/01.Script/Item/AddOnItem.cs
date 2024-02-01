using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddOnItem : BaseItem
{
    public override void OnGetItem(PlayerCharacter character)
    {
        base.OnGetItem(character);

        PlayerCharacter Player = character.GetComponent<PlayerCharacter>();

        if(GameInstance.instance.CurrentPlayerAddOnCount >= Player.MaxAddOnCount)
        {
            return;
        }

        Transform addOnspawnTransfrom = Player.AddOnTransform[GameInstance.instance.CurrentPlayerAddOnCount];
        SpawnAddOn(addOnspawnTransfrom.position, Player.AddOnPrefab, addOnspawnTransfrom);
        GameInstance.instance.CurrentPlayerAddOnCount++;
    }

    public static void SpawnAddOn(Vector3 position, GameObject prefab, Transform addOnTargetTransform)
    {
        GameObject AddOnInstance = Instantiate(prefab, position, Quaternion.identity);
        AddOnInstance.GetComponent<AddOn>().FollowTargetTransform = addOnTargetTransform;
    }
}
