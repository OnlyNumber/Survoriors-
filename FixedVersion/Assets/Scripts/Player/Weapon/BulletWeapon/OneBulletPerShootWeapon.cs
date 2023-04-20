using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Fusion;

public class OneBulletPerShootWeapon : WeaponNetwork
{

    private void Start()
    {
        AmmoText = GameObject.Find("AmmoIndicator").GetComponent<TMP_Text>();
    }

    public override void FixedUpdateNetwork()
    {

        try
        {
            if (HasInputAuthority)
            {
                AmmoText.text = $"{CurrentBullets} /{MaxBullets}  {Ammo}";
            }
        }
        catch
        {
            //Debug.Log(ex.Message);
        }


        if (GetInput(out NetworkInputData networkInput))
        {
            if (networkInput.isFireButtonPressed)
            {
                if (IsCanShoot)
                {
                    if (CurrentBullets > 0)
                    {
                        //Debug.Log("Shoot");

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
        //Vector3 direction = rotatePos - transform.position;
        //direction.z = 0;

        if (Runner == null)
        {
            Debug.Log("NULL RUNEER");
        }


        Runner.Spawn(Bullet, transform.position, null, null, (Runner, obj) => { obj.GetComponent<BulletNetwork>().InitializeDamage(Damage, rotatePos); });

        CurrentBullets--;


    }
}
