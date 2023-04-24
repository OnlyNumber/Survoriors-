using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageAble
{
    public void TakeDamage(int damage);

    public void TakeDamage(int damage, PlayerScrV3 playerScr = null);
}
