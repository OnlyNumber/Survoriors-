using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Fusion;

public class MultipleBulletPerShootWeapon : WeaponNetwork
{
    [SerializeField]
    private float _randoScatter;

    [SerializeField]
    private int _amountOfBulletsPerShoot;

    private void Start()
    {
        //playerScore = GetComponentInParent<PlayerScoreV2>();

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

        //Debug.Log("ShotGun");


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

        for (int i = 0; i < _amountOfBulletsPerShoot; i++)
        {



            Runner.Spawn(Bullet, transform.position, null, null, (Runner, obj) =>
            {
                obj.GetComponent<BulletNetwork>().InitializeBullet(Damage, rotatePos/*, playerScore*/);
                obj.transform.Rotate(new Vector3(0, 0, Random.Range(-_randoScatter, _randoScatter)));

            });
        }
        CurrentBullets--;

        if (HasInputAuthority)
        {
            AmmoText.text = $"{CurrentBullets} /{MaxBullets}  {Ammo}";
        }


    }
}
