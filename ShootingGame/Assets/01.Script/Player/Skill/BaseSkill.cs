using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSkill : MonoBehaviour
{
    protected PlayerCharacter _playerCharacter;
    public float CooldownTime;
    public float CurrentTime;
    public bool bIsCoolDown = false;

    public void Init(PlayerCharacter playerCharacter)
    {
        _playerCharacter = playerCharacter;
    }

    public void Update()
    {   
        if(bIsCoolDown)
        {
            CurrentTime -= Time.deltaTime;
            if (CurrentTime <= 0)
            {
                bIsCoolDown = false;
            }
        }
    }

    public bool isAvailable()
    {
        return !bIsCoolDown;
    }

    public virtual void Active()
    {
        bIsCoolDown = true;
        CurrentTime = CooldownTime;
    }

    public void InitCoolDown()
    {
        bIsCoolDown = false;
        CurrentTime = 0;
    }
}
