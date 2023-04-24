using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Fusion;

public class OneBulletPerShootWeapon : WeaponNetwork
{
    private PlayerScore playerScrV3;

    private void Start()
    {
        playerScrV3 = GetComponentInParent<PlayerScore>();

        AmmoText = GameObject.Find("AmmoIndicator").GetComponent<TMP_Text>();

        try
        {
            if (HasInputAuthority)
            {
                AmmoText.text = $"{CurrentBullets} /{MaxBullets}  {Ammo}";
            }
        }
        catch
        {

        }

    }

    public override void FixedUpdateNetwork()
    {


        if (GetInput(out NetworkInputData networkInput))
        {
            if (networkInput.isFireButtonPressed)
            {
                if (IsCanShoot)
                {
                    if (CurrentBullets > 0)
                    {
                        Shoot(networkInput.mousePosition);

                        StartCoroutine(WaitingForShoot(TimeBetweenAttack));
                    }
                    else
                    {

                        StartCoroutine(Reload(ReloadTime));
                    }
                }
            }

            if (networkInput.isReloadButtonPressed && CurrentBullets != MaxBullets)
            {
                StartCoroutine(Reload(ReloadTime));
            }



        }
    }


    public override void Shoot(Vector3 rotatePos)
    {
        

        Runner.Spawn(Bullet, transform.position, null, null, (Runner, obj) => { obj.GetComponent<BulletNetwork>().InitializeBullet(Damage, rotatePos, playerScrV3); });

        CurrentBullets--;

        if (HasInputAuthority)
        {
            AmmoText.text = $"{CurrentBullets} /{MaxBullets}  {Ammo}";


        }

    }
}
