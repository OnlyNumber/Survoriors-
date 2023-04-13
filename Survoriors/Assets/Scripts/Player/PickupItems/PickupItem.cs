using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public interface PickupItem
{
    public static int PLAYER_LAYER = 7;
    public static int ENEMY_LAYER = 6;

    public void DoOnPickUp();
}
