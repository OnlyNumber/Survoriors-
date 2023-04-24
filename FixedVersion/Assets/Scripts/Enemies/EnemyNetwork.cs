using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class EnemyNetwork : NetworkBehaviour
{

    [SerializeField]
    private float Speed;

    List<GameObject> players;

    protected GameObject Target;

    protected bool _isCanAttack = true;

    [SerializeField]
    protected int damage;

    [field: SerializeField]
    public float distanceForAttack { get; protected set; }

    [SerializeField]
    protected float _timeBetweenAttack;

    private void Awake()
    {
        players = new List<GameObject>();
    }


    public override void FixedUpdateNetwork()
    {

        FindTarget();

        if (Target != null && Vector2.Distance(Target.transform.position, transform.position) > distanceForAttack)
        {
            MoveToTarget();
        }

    }

    public float GetDistance()
    {
        if (Target != null)
            return Vector2.Distance(Target.transform.position, transform.position);
        else
            return -1;
    }


    protected virtual void Attack() { }

    private void FindTarget()
    {



        if(players.Count == 0)
        {
            FindPlayers();
        }

        if (players.Count == 0)
        {
            //Debug.Log("No players");
            Target = null;

            return;
        }


        
        

        Target = players[0];

        float dist = GetDistance();

        if (dist != -1)
        {
            foreach (var item in players)
            {


                if (dist > Vector2.Distance(item.transform.position, transform.position))
                {
                    dist = Vector2.Distance(item.transform.position, transform.position);

                    Target = item;
                }

            }
        }
    }

    private void MoveToTarget()
    {
        if (Target != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, Target.transform.position, Speed);
        }
        else
        {
            Debug.Log("No target");
        }
    }


    public void FindPlayers()
    {
        foreach (var item in FindObjectsOfType<PlayerInputHandler>())
        {
            players.Add(item.gameObject);
            Debug.Log(item.gameObject.name);

        }
    }
    
    protected IEnumerator AttackReload(float time)
    {
        _isCanAttack = false;

        yield return new WaitForSecondsRealtime(time);


        _isCanAttack = true;
    }
}
