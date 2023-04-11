using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class WeaponNetwork : NetworkBehaviour
{
    [SerializeField]
    private int _damage;

    [SerializeField]
    private float _timeBetweenAttack;

    [SerializeField]
    private float _reloadTime;

    [SerializeField]
    private int _maxBullets;

    private int _currentBullets;

    private bool _isCanShoot;

    [SerializeField]
    protected NetworkObject Bullet;

    public virtual void Shoot()
    {

    }

    protected IEnumerator WaitingForShoot(float time)
    {
        _isCanShoot = false;

        yield return new WaitForSecondsRealtime(time);

        _isCanShoot = true;
    }


}
