using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class EnemyNetwork : NetworkBehaviour
{
//<<<<<<< HEAD:Survoriors/Assets/Scripts/Enemies/EnemyNetwork.cs

    //private NetworkRunner _networkRunner;
//=======
//    NetworkRunner networkRunner;
//>>>>>>> parent of e7ad8a1e (Start creating weapon and enemy fabric):Survoriors/Assets/Scripts/ThirdTryFusion/Enemies/EnemyNetwork.cs

    [SerializeField]
    private float Speed;

    List<GameObject> players;

//<<<<<<< HEAD:Survoriors/Assets/Scripts/Enemies/EnemyNetwork.cs
    protected GameObject Target;

    protected bool _isCanAttack = true;

    [SerializeField]
    protected int damage;

    [SerializeField]
    protected float distanceForAttack;

    [SerializeField]
    protected float _timeBetweenAttack;
//=======
//    private GameObject target;
//>>>>>>> parent of e7ad8a1e (Start creating weapon and enemy fabric):Survoriors/Assets/Scripts/ThirdTryFusion/Enemies/EnemyNetwork.cs

    private void Awake()
    {
        players = new List<GameObject>();
    }


    public override void FixedUpdateNetwork()
    {
        //Debug.Log("Update");

        FindTarget();

//<<<<<<< HEAD:Survoriors/Assets/Scripts/Enemies/EnemyNetwork.cs
        if (Target != null && Vector2.Distance(Target.transform.position, transform.position) > distanceForAttack)
        {
            MoveToTarget();
        }
//=======
 //       MoveToTarget();

//>>>>>>> parent of e7ad8a1e (Start creating weapon and enemy fabric):Survoriors/Assets/Scripts/ThirdTryFusion/Enemies/EnemyNetwork.cs
    }

    protected virtual void Attack() { }

    private void FindTarget()
    {
//<<<<<<< HEAD:Survoriors/Assets/Scripts/Enemies/EnemyNetwork.cs
        /*if(_networkRunner == null)
        {
            _networkRunner = FindObjectOfType<NetworkRunner>();
        }*/

 //       if(_players.Count == 0)
//=======
        if(players.Count == 0)
//>>>>>>> parent of e7ad8a1e (Start creating weapon and enemy fabric):Survoriors/Assets/Scripts/ThirdTryFusion/Enemies/EnemyNetwork.cs
        {
            FindPlayers();
        }

        if (players.Count == 0)
        {
//<<<<<<< HEAD:Survoriors/Assets/Scripts/Enemies/EnemyNetwork.cs
            Debug.Log("No players");
            Target = null;
//=======
            //Debug.Log("WTF");

//            target = null;
//>>>>>>> parent of e7ad8a1e (Start creating weapon and enemy fabric):Survoriors/Assets/Scripts/ThirdTryFusion/Enemies/EnemyNetwork.cs
            return;
        }


        float dist;
        dist = Vector2.Distance(players[0].transform.position, transform.position);

//<<<<<<< HEAD:Survoriors/Assets/Scripts/Enemies/EnemyNetwork.cs
        Target = players[0];
//=======
//        target = players[0];
//>>>>>>> parent of e7ad8a1e (Start creating weapon and enemy fabric):Survoriors/Assets/Scripts/ThirdTryFusion/Enemies/EnemyNetwork.cs

        foreach (var item in players)
        {

            //Debug.Log($"{dist} |=| {Vector2.Distance(item.transform.position, transform.position)}");

            if(dist > Vector2.Distance(item.transform.position, transform.position))
            {
                dist = Vector2.Distance(item.transform.position, transform.position);

//<<<<<<< HEAD:Survoriors/Assets/Scripts/Enemies/EnemyNetwork.cs
                Target = item;
//=======
//                target = item;
//>>>>>>> parent of e7ad8a1e (Start creating weapon and enemy fabric):Survoriors/Assets/Scripts/ThirdTryFusion/Enemies/EnemyNetwork.cs
            }

        }
    }

    private void MoveToTarget()
    {
//<<<<<<< HEAD:Survoriors/Assets/Scripts/Enemies/EnemyNetwork.cs
        if (Target != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, Target.transform.position, Speed);
        }
        else
        {
            Debug.Log("No target");
//
        

        /*if (target != null)
        {
            //Debug.Log("Moving");
            //Debug.Log(target.name);
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed);
        }
        else
        {
            //Debug.Log("target NULL");
>>>>>>> parent of e7ad8a1e (Start creating weapon and enemy fabric):Survoriors/Assets/Scripts/ThirdTryFusion/Enemies/EnemyNetwork.cs*/
        }
    }


    public void FindPlayers()
    {
        //Debug.Log("Find player");

        foreach (var item in FindObjectsOfType<PlayerInputHandler>())
        {
//<<<<<<< HEAD:Survoriors/Assets/Scripts/Enemies/EnemyNetwork.cs
//            _players.Add(item.gameObject);
//            Debug.Log("Find player " + item.gameObject.name);
//=======
            players.Add(item.gameObject);
            Debug.Log(item.gameObject.name);
//>>>>>>> parent of e7ad8a1e (Start creating weapon and enemy fabric):Survoriors/Assets/Scripts/ThirdTryFusion/Enemies/EnemyNetwork.cs

        }
    }

    /*public void InitializeNetwork(NetworkRunner networkRunner)
    {
//<<<<<<< HEAD:Survoriors/Assets/Scripts/Enemies/EnemyNetwork.cs
        this._networkRunner = networkRunner;
    }*/
    
    protected IEnumerator AttackReload(float time)
    {
        _isCanAttack = false;

        yield return new WaitForSecondsRealtime(time);


        _isCanAttack = true;
    }

//=======
        //this.networkRunner = networkRunner;
    


//>>>>>>> parent of e7ad8a1e (Start creating weapon and enemy fabric):Survoriors/Assets/Scripts/ThirdTryFusion/Enemies/EnemyNetwork.cs
}
