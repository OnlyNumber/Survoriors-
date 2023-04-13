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

    

    private void Awake()
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

        //_rigidbody2D.MovePosition(transform.position + Vector3.right * 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == numberOfCollideableLayer)
        {
            Debug.Log($"Who {collision.gameObject.name} + {damage}");



            collision.gameObject.GetComponent<IDamageAble>().TakeDamage(damage);

            Runner.Despawn(_networkObject);

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
