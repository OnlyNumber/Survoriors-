using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using System.Linq;
using UnityEngine.UI;


public class SkinController : NetworkBehaviour
{
    [SerializeField]
    SpriteRenderer playerSkin;

    [SerializeField]
    Sprite[] skins;

    struct NetworkSkinStruct : INetworkStruct
    {
        public int skinNumber;
    }

    [Networked(OnChanged = nameof(OnSkinChanged))]
    NetworkSkinStruct playerSkinNetwork { get; set; }


    private void Start()
    {
        NetworkSkinStruct skin ;

        skin.skinNumber = (int)DataHolderPlayer.playerSkin;

        Rpc_RequestChangeSkin(skin);
    }
    
    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    private void Rpc_RequestChangeSkin(NetworkSkinStruct skin, RpcInfo info = default)
    {


        playerSkinNetwork = skin;
    }

    static void OnSkinChanged(Changed<SkinController> changed)
    {
        Debug.Log("work");

        changed.Behaviour.OnSkinChange();
    }

    private void OnSkinChange()
    {
        playerSkin.sprite = skins[playerSkinNetwork.skinNumber];
    }

}
