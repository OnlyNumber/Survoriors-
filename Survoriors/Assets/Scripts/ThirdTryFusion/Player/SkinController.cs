using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using System.Linq;
using UnityEngine.UI;


public class SkinController : NetworkBehaviour
{


    private int  playerSkin;

    public delegate void ChangePlayerSkin();

    /*public ChangePlayerSkin onPlayerChangedSkin;

    public int PlayerSkin
    {
        set
        {
            playerSkin = value;

            onPlayerChangedSkin?.Invoke();
        }

        get
        {
            return playerSkin;
        }
    }*/

    

    [SerializeField]
    SpriteRenderer[] skins;

    struct NetworkSkinStruct : INetworkStruct
    {
        public int skinNumber;
    }

    [Networked(OnChanged = nameof(OnSkinChanged))]
    NetworkSkinStruct playerSkinNetwork { get; set; }

    StateMachine sM;


    private void Start()
    {
        sM = GetComponentInChildren<StateMachine>();

        NetworkSkinStruct skin ;

        skin.skinNumber = (int)DataHolderPlayer.playerSkin;

        Rpc_RequestChangeSkin(skin);
    }
    
    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    private void Rpc_RequestChangeSkin(NetworkSkinStruct skin, RpcInfo info = default)
    {
        Debug.Log($"Recive RPC request for player {transform.name} DataHolder ID {DataHolderPlayer.playerSkin}");

        playerSkinNetwork = skin;
    }

    static void OnSkinChanged(Changed<SkinController> changed)
    {
        //Debug.Log("work");

        changed.Behaviour.OnSkinChange();
    }

    private void OnSkinChange()
    {
        playerSkin = playerSkinNetwork.skinNumber;

        Debug.Log("Work or not ");
        Debug.Log(playerSkin);

        if(sM == null)
        {
            sM = GetComponentInChildren<StateMachine>();
        }

        sM.ChangeSkin(playerSkinNetwork.skinNumber);
        //playerSkin.sprite = skins[playerSkinNetwork.skinNumber].sprite;
    }

}
