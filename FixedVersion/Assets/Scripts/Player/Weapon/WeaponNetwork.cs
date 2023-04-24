using System.Collections;
using UnityEngine;
using Fusion;
using TMPro;

public class WeaponNetwork : NetworkBehaviour
{
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

    [SerializeField]
    private int AmmoModificator;

    protected int CurrentBullets;

    [SerializeField]
    protected int Ammo;

    protected bool IsCanShoot = true;

    [SerializeField]
    protected NetworkObject Bullet;

    private void Start()
    {
        networkObject = GetComponentInParent<NetworkObject>();
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
        this.Ammo += MaxBullets * AmmoModificator;

        if (HasInputAuthority)
        {
            AmmoText.text = $"{CurrentBullets} /{MaxBullets}  {Ammo}";
        }

    }

}
