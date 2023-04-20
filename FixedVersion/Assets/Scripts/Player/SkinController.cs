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

    //[SerializeField]
    //public List<Sprite> listsprite;

    //public SpriteRenderer weaponSprite;

    [SerializeField]
    public int numberOfWeapon;

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

        skin.playerRef = Runner.LocalPlayer;
        //Rpc_RequestChangeSkin(skin);

        do
        {
        
            numberOfWeapon = Random.Range(0, _weaponList.Count);
        
        } while (CheckThisNumberOfWeapon(numberOfWeapon));

        //Debug.Log(numberOfWeapon);
        
        skin.numberOfWeapon = numberOfWeapon;
        
        //Debug.Log(HasStateAuthority);

        Rpc_RequestChangeSkin(skin);
        


        Rpc_Do_Smth();

        //Rpc_RequestSpawnWeapon(Runner.LocalPlayer, numberOfWeapon);
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    private void Rpc_RequestChangeSkin(NetworkSkinStruct skin, RpcInfo info = default)
    {
        //Debug.Log($"Recive RPC request for player {transform.name} DataHolder ID {DataHolderPlayer.playerSkin}");

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

        if(!HasStateAuthority)
        {
            Debug.Log("No Host");
            Debug.Log(numberOfWeapon + "Number weapon");
        }

        _weaponList[1].gameObject.SetActive(true);
        
        /*if(!HasStateAuthority)
        {
            _weaponList[numberOfWeapon].gameObject.SetActive(true);
        }*/    

        /*NetworkObject weaponObject = Runner.Spawn(_weaponList[numberOfWeapon], null, null, playerSkinNetwork.playerRef, (Runner, obj) => 
            {

                obj.gameObject.transform.SetParent(gameObject.transform);
                Debug.Log(gameObject.transform);
                obj.transform.position = WEAPON_POSITION;

            });*/


        // GameObject.Find(_weaponList[numberOfWeapon].name + "(Clone)").transform.SetParent(gameObject.transform);

        /* if (weaponObject == null)
         {
             Debug.Log("No weaponObject");
         }

         if (weaponObject.gameObject == null)
         {
             Debug.Log("No gameobject");
         }

         weaponObject.gameObject.transform.SetParent(gameObject.transform);
         weaponObject.transform.position = WEAPON_POSITION;*/

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
