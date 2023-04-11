using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class EnemyNetwork : NetworkBehaviour
{
    NetworkRunner networkRunner;

    [SerializeField]
    private float speed;

    List<GameObject> players;

    private GameObject target;

    private void Awake()
    {
        players = new List<GameObject>();
    }


    public override void FixedUpdateNetwork()
    {
        //Debug.Log("Update");

        FindTarget();

        MoveToTarget();

    }

    private void FindTarget()
    {
        if(players.Count == 0)
        {
            FindPlayers();
        }

        if (players.Count == 0)
        {
            //Debug.Log("WTF");

            target = null;
            return;
        }


        float dist;
        dist = Vector2.Distance(players[0].transform.position, transform.position);

        target = players[0];

        foreach (var item in players)
        {

            //Debug.Log($"{dist} |=| {Vector2.Distance(item.transform.position, transform.position)}");

            if(dist > Vector2.Distance(item.transform.position, transform.position))
            {
                dist = Vector2.Distance(item.transform.position, transform.position);

                target = item;
            }

        }
    }

    private void MoveToTarget()
    {

        

        if (target != null)
        {
            //Debug.Log("Moving");
            //Debug.Log(target.name);
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed);
        }
        else
        {
            //Debug.Log("target NULL");
        }
    }


    public void FindPlayers()
    {
        //Debug.Log("Find player");

        foreach (var item in FindObjectsOfType<PlayerInputHandler>())
        {
            players.Add(item.gameObject);
            Debug.Log(item.gameObject.name);

        }
    }

    public void InitializeNetwork(NetworkRunner networkRunner)
    {
        this.networkRunner = networkRunner;
    }


}
