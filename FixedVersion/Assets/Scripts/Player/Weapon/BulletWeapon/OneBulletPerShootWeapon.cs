using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Fusion;

public class OneBulletPerShootWeapon : WeaponNetwork
{
    private PlayerScrV3 playerScrV3;

    private TMP_Text testText;

    private void Start()
    {
        //playerScore = GetComponentInParent<PlayerScoreV2>();

        playerScrV3 = GetComponentInParent<PlayerScrV3>();

        AmmoText = GameObject.Find("AmmoIndicator").GetComponent<TMP_Text>();

        //testText = GameObject.Find("Kills Indicator").GetComponent<TMP_Text>();

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
        //Debug.Log("Shoot");

        //playerScrV3.Rpc_RequestSaveShoots(1);

        //if(HasInputAuthority)
        //testText.text = $"{playerScrV3.Shoots}";

        Runner.Spawn(Bullet, transform.position, null, null, (Runner, obj) => { obj.GetComponent<BulletNetwork>().InitializeBullet(Damage, rotatePos, playerScrV3); });

        CurrentBullets--;

        if (HasInputAuthority)
        {
            AmmoText.text = $"{CurrentBullets} /{MaxBullets}  {Ammo}";


        }

    }
}
