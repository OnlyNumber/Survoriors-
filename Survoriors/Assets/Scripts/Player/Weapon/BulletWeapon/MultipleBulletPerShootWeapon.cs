using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Fusion;

public class MultipleBulletPerShootWeapon : WeaponNetwork
{
    [SerializeField]
    private float _randoScatter;

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
            Debug.Log("Error");
        }
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
        //Vector3 direction = rotatePos - transform.position;
        //direction.z = 0;

        if (Runner == null)
        {
            Debug.Log("NULL RUNEER");
        }

        //direction.x += 90;
        for (int i = 0; i < 5; i++)
        {
            Runner.Spawn(Bullet, transform.position, null, null, (Runner, obj) =>
            {
                obj.GetComponent<BulletNetwork>().InitializeDamage(Damage, rotatePos);
                obj.transform.Rotate(new Vector3(0, 0, Random.Range(-_randoScatter, _randoScatter)));

            });
        }
        







        CurrentBullets--;


    }
}
