using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
//using UnityEngine.UI;
using TMPro;

public class WeaponNetwork : NetworkBehaviour
{
    //protected PlayerScoreV2 playerScore;

    [SerializeField]
    protected NetworkObject networkObject;

    [SerializeField]
    protected TMP_Text AmmoText;  

    [SerializeField]
    protected int Damage;

    [SerializeField]
    protected float TimeBetweenAttack;

    [SerializeField]
    protected float ReloadTime;

    [SerializeField]
    protected int MaxBullets;


    protected int CurrentBullets;

    [SerializeField]
    protected int Ammo;

    protected bool IsCanShoot = true;

    [SerializeField]
    protected NetworkObject Bullet;

    private void Start()
    {
        networkObject = GetComponentInParent<NetworkObject>();
        //playerScore = GetComponent<PlayerScoreV2>();

    }

    public virtual void Shoot(Vector3 rotatePos)
    {

    }

    protected IEnumerator WaitingForShoot(float time)
    {
        IsCanShoot = false;

        yield return new WaitForSecondsRealtime(time);

        IsCanShoot = true;
    }

    protected IEnumerator Reload(float time)
    {
        IsCanShoot = false;

        yield return new WaitForSecondsRealtime(time);

        int reason = MaxBullets - CurrentBullets;
        if(Ammo >= reason)
        {
            Ammo = Ammo - reason;
            CurrentBullets = MaxBullets;
        }
        else
        {
            CurrentBullets = CurrentBullets + Ammo;
            Ammo = 0;
        }


        IsCanShoot = true;
    }

    public void TakeAmmo()
    {
        this.Ammo += MaxBullets;
    }

}
