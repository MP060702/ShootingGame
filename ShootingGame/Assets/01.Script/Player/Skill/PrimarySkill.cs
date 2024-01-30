using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimarySkill : BaseSkill
{
    public float ProjectileMoveSpeed;
    public GameObject Projectile;

    private Weapon[] weapons;
    // Start is called before the first frame update
    void Start()
    {
        CooldownTime = 0.2f;

        weapons = new Weapon[5];

        weapons[0] = new Level1Weapon();
        weapons[1] = new Level2Weapon();
        weapons[2] = new Level3Weapon();
        weapons[3] = new Level4Weapon();
        weapons[4] = new Level5Weapon();
    }
    
    public override void Active()
    {
        base.Active();
        weapons[GameManager.Instance.GetPlayerCharacter().CurrentWeaponLevel].Activate(this, GameManager.Instance.GetPlayerCharacter());
    }

    public void ShootProjectile(Vector3 position, Vector3 direction)
    {
        
        GameObject instance = Instantiate(Projectile, position, Quaternion.identity);
        Projectile projectile = instance.GetComponent<Projectile>();

        if(projectile != null )
        {
            projectile.MoveSpeed = ProjectileMoveSpeed;
            projectile.SetDirection(direction.normalized);
        }
    }

    public interface Weapon
    {
        void Activate(PrimarySkill primaryskill, PlayerCharacter playerCharacter);
    }

    public class Level1Weapon : Weapon
    {
        public void Activate(PrimarySkill primaryskill, PlayerCharacter playerCharacter)
        {
            Vector3 position = playerCharacter.transform.position;
            primaryskill.ShootProjectile(position, Vector2.up);
        }
    }
    public class Level2Weapon : Weapon
    {
        public void Activate(PrimarySkill primaryskill, PlayerCharacter playerCharacter)
        {

        }
    }
    public class Level3Weapon : Weapon
    {
        public void Activate(PrimarySkill primaryskill, PlayerCharacter playerCharacter)
        {

        }
    }
    public class Level4Weapon : Weapon
    {
        public void Activate(PrimarySkill primaryskill, PlayerCharacter playerCharacter)
        {

        }
    }

    public class Level5Weapon : Weapon
    {
        public void Activate(PrimarySkill primaryskill, PlayerCharacter playerCharacter)
        {

        }
    }
}
