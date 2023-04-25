using System.Collections.Generic;
using UnityEngine;
using Fusion;


public class SkinController : NetworkBehaviour
{
    public int playerSkin { get; private set; }

    public delegate void ChangePlayerSkin();

    struct NetworkSkinStruct : INetworkStruct
    {
        public int skinNumber;
        public int numberOfWeapon;
        public PlayerRef playerRef;

    }

    [Networked(OnChanged = nameof(OnSkinChanged))]
    private NetworkSkinStruct playerSkinNetwork { get; set; }

    StateMachine stateMachine;

    [SerializeField]
    private List<NetworkObject> _weaponList;

    [SerializeField]
    public int numberOfWeapon;

    //private Vector2 WEAPON_POSITION = new Vector2(-0.02f, -0.18f);

    //PlayerRef refer;

    private void Start()
    {
        stateMachine = GetComponentInChildren<StateMachine>();
        NetworkSkinStruct skin;
        skin.skinNumber = (int)DataHolderPlayer.playerSkin;

        skin.playerRef = Runner.LocalPlayer;
        do
        {
        
            numberOfWeapon = Random.Range(0, _weaponList.Count);
        
        } while (CheckThisNumberOfWeapon(numberOfWeapon));
        
        
        skin.numberOfWeapon = numberOfWeapon;
        Rpc_RequestChangeSkin(skin);
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    private void Rpc_RequestChangeSkin(NetworkSkinStruct skin, RpcInfo info = default)
    {
        playerSkinNetwork = skin;
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

        this.numberOfWeapon = playerSkinNetwork.numberOfWeapon;

        _weaponList[numberOfWeapon].gameObject.SetActive(true);
        
    }



    private bool CheckThisNumberOfWeapon(int numberOfWeapon)
    {
        foreach(var player in FindObjectsOfType<NetworkPlayer>())
        {

            if(player.GetComponent<NetworkObject>().InputAuthority.PlayerId != GetComponent<NetworkObject>().InputAuthority.PlayerId && numberOfWeapon == player.GetComponent<SkinController>().numberOfWeapon)
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
