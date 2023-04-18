using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class BulletNetwork : NetworkBehaviour
{

    [SerializeField]
    private float _lifeTime;

    [SerializeField]
    private float _speed;

    [SerializeField]
    private int damage;

    [SerializeField]
    private int numberOfCollideableLayer;

    private const float ENEMY_LAYER = 6;

    TickTimer timerBeforeDestroy;

    private NetworkObject _networkObject;

    private List<LagCompensatedHit> _hits = new List <LagCompensatedHit>();

    private void Start()
    {

        _networkObject = GetComponent<NetworkObject>();

        timerBeforeDestroy = TickTimer.CreateFromSeconds(Runner, _lifeTime);

    }

    public override void FixedUpdateNetwork()
    {
        transform.Translate(Vector2.right * _speed);

        if(timerBeforeDestroy.Expired(Runner))
        {
            Runner.Despawn(_networkObject);
        }

        /*int hitCount = Runner.LagCompensation.OverlapSphere(transform.position, 0.5f, 0,_hits);

        bool isValidHit = false;

        //We've hit something, so the hit could be valid
        if (hitCount > 0)
            isValidHit = true;

        //check what we've hit
        for (int i = 0; i < hitCount; i++)
        {
            //Check if we have hit a Hitbox
            if (_hits[i].Hitbox != null)
            {
                //Check that we didn't fire the rocket and hit ourselves. This can happen if the lag is a bit high.
                if (_hits[i].Hitbox.Root.GetBehaviour<NetworkObject>().GetComponent<NetworkPlayer>())
                    isValidHit = false;
            }
        }

        //We hit something valid
        if (isValidHit)
        {
            //Now we need to figure out of anything was within the blast radius
            hitCount = Runner.LagCompensation.OverlapSphere(transform.position, 0.5f, 0, _hits);

            //Deal damage to anything within the hit radius
            for (int i = 0; i < hitCount; i++)
            {
                IDamageAble hpHandler = _hits[i].Hitbox.transform.root.GetComponent<IDamageAble>();

                if (hpHandler != null)
                    hpHandler.TakeDamage(damage);
            }

            Runner.Despawn(GetComponent<NetworkObject>());
        }*/

        //_rigidbody2D.MovePosition(transform.position + Vector3.right * 1);
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == numberOfCollideableLayer)
        {
            // Debug.Log($"Who {collision.gameObject.name} + {damage}");

            Runner.Despawn(_networkObject);



            collision.gameObject.GetComponent<IDamageAble>().TakeDamage(damage);

            

        }

    }

    public void InitializeDamage(int damage, Vector3 direction)
    {
        this.damage = damage;

        //Debug.Log(direction);
        //Debug.Log(direction.normalized);


        transform.right = direction.normalized;

        //transform.Rotate(new Vector3(0, 0, Random.Range(-rotation,rotation)));

        //_runner = runner;


    }
    


}
