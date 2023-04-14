using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using TMPro;

public class OneBulletPerShootWeapon : WeaponNetwork
{

    //private NetworkRunner _networkRunner;

    private void Start()
    {
        //_networkRunner = FindObjectOfType<NetworkRunner>();

        AmmoText = GameObject.Find("AmmoIndicator").GetComponent<TMP_Text>();
    }

    public override void FixedUpdateNetwork()
    {
        //if(AmmoText == null)
        // GameObject.Find("AmmoIndicator").GetComponent<TMP_Text>();

        if (HasInputAuthority && AmmoText != null)
            AmmoText.text = $"{CurrentBullets} /{MaxBullets}  {Ammo}";
        else
        {
            Debug.Log("No acces");
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
        Vector3 direction = rotatePos - transform.position;
        direction.z = 0;

        if (Runner == null)
        {
            Debug.Log("NULL RUNEER");
        }


        //direction.y += 90;

        //direction *= 3f;


        Runner.Spawn(Bullet, transform.position, null, null, (Runner, obj) => { obj.GetComponent<BulletNetwork>().InitializeDamage(Damage, direction); });

        CurrentBullets--;


    }
}
