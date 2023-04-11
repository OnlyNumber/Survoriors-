using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class SmallShotgun : WeaponNetwork
{
    private NetworkRunner _networkRunner;


    public override void Shoot()
    {
        _networkRunner.Spawn(Bullet, transform.position, transform.rotation);
        

    }
}
