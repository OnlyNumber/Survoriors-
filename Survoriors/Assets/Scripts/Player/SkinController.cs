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
    }

    [Networked(OnChanged = nameof(OnSkinChanged))]
    NetworkSkinStruct playerSkinNetwork { get; set; }

    StateMachine stateMachine;

    [SerializeField]
    private List<NetworkObject> _weaponList;

    //[SerializeField]
    //public List<Sprite> listsprite;

    //public SpriteRenderer weaponSprite;

    public int numberOfWeapon { get; private set; }

    private Vector2 WEAPON_POSITION = new Vector2(-0.02f, -0.18f);

    PlayerRef refer;

    private void Start()
    {
        //weapon = GetComponentInChildren<WeaponRotater>().gameObject;

        //weapon.AddComponent<MultipleBulletPerShootWeapon>();

        //weaponSprite.sprite = listsprite[1];

        /*NetworkObject weaponObject;

        if (Runner == null)
        {
            Debug.Log("No runner");
        }

        if (weapon == null)
        {
            Debug.Log("weaponPrefab");
        }
        



        weaponObject = Runner.Spawn(weapon,null,null, Runner.LocalPlayer);

        Debug.Log(Runner.LocalPlayer.PlayerId);

        if (weaponObject == null)
        {
            Debug.Log("weaponObject");
        }
        
        if (weaponObject.gameObject == null)
        {
            Debug.Log("weaponObject.gameObject");
        }
        if (weaponObject.gameObject.transform == null)
        {
            Debug.Log("weaponObject.gameObject.transform");
        }
        if (gameObject == null)
        {
            Debug.Log("gameObject");
        }
        if (gameObject.transform == null)
        {
            Debug.Log("gameObject.transform");
        }



        weaponObject.gameObject.transform.SetParent(gameObject.transform);

        weaponObject.transform.position = WEAPON_POSITION;

        weaponObject.transform.localScale = new Vector3(1, 1, 0);*/


        stateMachine = GetComponentInChildren<StateMachine>();
        NetworkSkinStruct skin;
        skin.skinNumber = (int)DataHolderPlayer.playerSkin;
        Rpc_RequestChangeSkin(skin);

        do
        {

            numberOfWeapon = Random.Range(0, _weaponList.Count);

        } while (CheckThisNumberOfWeapon(numberOfWeapon));

        Rpc_RequestSpawnWeapon(Runner.LocalPlayer, numberOfWeapon);
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    private void Rpc_RequestChangeSkin(NetworkSkinStruct skin, RpcInfo info = default)
    {
        Debug.Log($"Recive RPC request for player {transform.name} DataHolder ID {DataHolderPlayer.playerSkin}");

        playerSkinNetwork = skin;
    }



    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    private void Rpc_RequestSpawnWeapon(PlayerRef playerRef,int numberOfWeapon,  RpcInfo info = default)
    {
        Debug.Log($"Recive RPC request for player {transform.name} DataHolder ID {DataHolderPlayer.playerSkin}");

        //numberOfWeapon = Random.Range(0, _weaponList.Count);

        //CheckThisNumberOfWeapon(numberOfWeapon);

        this.numberOfWeapon = numberOfWeapon;

        NetworkObject weaponObject = Runner.Spawn(_weaponList[numberOfWeapon], null, null, playerRef);
        weaponObject.gameObject.transform.SetParent(gameObject.transform);
        weaponObject.transform.position = WEAPON_POSITION;

        //weaponObject.transform.localScale = new Vector3(1, 1, 0);

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
    }

    private bool CheckThisNumberOfWeapon(int numberOfWeapon)
    {
        foreach(var player in FindObjectsOfType<NetworkPlayer>())
        {
            Debug.Log("Search");

            if(player.GetComponent<NetworkObject>().InputAuthority.PlayerId != GetComponent<NetworkObject>().InputAuthority.PlayerId && numberOfWeapon == player.GetComponent<SkinController>().numberOfWeapon)
            {
                if(player.GetComponent<NetworkObject>().InputAuthority.PlayerId != GetComponent<NetworkObject>().InputAuthority.PlayerId)
                {
                    Debug.Log($" Same weapons ID");
                }
                Debug.Log($"{player.token} {GetComponent<NetworkPlayer>().token} Same weapons");

                return true;
            }
            else
            {
                Debug.Log($"{player.GetComponent<NetworkObject>().InputAuthority.PlayerId} {GetComponent<NetworkObject>().InputAuthority.PlayerId} Not Same Weapons");
            }
        }

        return false;

    }


}
