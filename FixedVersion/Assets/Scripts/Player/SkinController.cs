using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using System.Linq;
using UnityEngine.UI;


public class SkinController : NetworkBehaviour
{
    private int playerSkin;

    public delegate void ChangePlayerSkin();

    struct NetworkSkinStruct : INetworkStruct
    {
        public int skinNumber;
        public int numberOfWeapon;
        public PlayerRef playerRef;

    }

    [Networked(OnChanged = nameof(OnSkinChanged))]
    NetworkSkinStruct playerSkinNetwork { get; set; }

    StateMachine stateMachine;

    [SerializeField]
    private List<NetworkObject> _weaponList;

    [SerializeField]
    public int numberOfWeapon;

    private Vector2 WEAPON_POSITION = new Vector2(-0.02f, -0.18f);

    PlayerRef refer;

    private void Start()
    {
        stateMachine = GetComponentInChildren<StateMachine>();
        NetworkSkinStruct skin;
        skin.skinNumber = (int)DataHolderPlayer.playerSkin;

        skin.playerRef = Runner.LocalPlayer;
        //Rpc_RequestChangeSkin(skin);

        do
        {
        
            numberOfWeapon = Random.Range(0, _weaponList.Count);
        
        } while (CheckThisNumberOfWeapon(numberOfWeapon));
        
        
        skin.numberOfWeapon = numberOfWeapon;
        Rpc_RequestChangeSkin(skin);
        Rpc_Do_Smth();
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    private void Rpc_RequestChangeSkin(NetworkSkinStruct skin, RpcInfo info = default)
    {
        playerSkinNetwork = skin;
    }

    [Rpc(RpcSources.All, RpcTargets.All)]
    private void Rpc_Do_Smth()
    {
        Debug.Log("Do smth");
    }

    static void OnSkinChanged(Changed<SkinController> changed)
    {
        changed.Behaviour.OnSkinChange();
    }

    private void OnSkinChange()
    {
        playerSkin = playerSkinNetwork.skinNumber;

        if (stateMachine == null)
        {
            stateMachine = GetComponentInChildren<StateMachine>();
        }

        stateMachine.ChangeSkin(playerSkinNetwork.skinNumber);

        //Debug.Log(playerSkinNetwork.numberOfWeapon);

        this.numberOfWeapon = playerSkinNetwork.numberOfWeapon;

        _weaponList[0].gameObject.SetActive(true);
        
    }



    private bool CheckThisNumberOfWeapon(int numberOfWeapon)
    {
        foreach(var player in FindObjectsOfType<NetworkPlayer>())
        {

            if(player.GetComponent<NetworkObject>().HasInputAuthority != GetComponent<NetworkObject>().HasInputAuthority && numberOfWeapon == player.GetComponent<SkinController>().numberOfWeapon)
            {
                Debug.Log($"{player.token} {GetComponent<NetworkPlayer>().token} Same weapons {numberOfWeapon} == {player.GetComponent<SkinController>().numberOfWeapon}");

                return true;
            }
            else
            {
                Debug.Log($"{player.GetComponent<NetworkObject>().InputAuthority.PlayerId} =Id= {GetComponent<NetworkObject>().InputAuthority.PlayerId} Not Same weapons {numberOfWeapon} == {player.GetComponent<SkinController>().numberOfWeapon}");
            }

        }

        return false;

    }


}
