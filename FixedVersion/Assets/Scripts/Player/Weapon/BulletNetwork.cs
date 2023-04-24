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

    private const int ENEMY_LAYER = 6;

    TickTimer timerBeforeDestroy;

    private NetworkObject _networkObject;

    private List<LagCompensatedHit> _hits = new List <LagCompensatedHit>();

    private PlayerScore _playerScore;

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
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == numberOfCollideableLayer)
        {
            collision.gameObject.GetComponent<IDamageAble>().TakeDamage(damage,_playerScore);

            Runner.Despawn(_networkObject);

        }
    }

    public void InitializeBullet(int damage, Vector3 direction, PlayerScore score = null)
    {
        this.damage = damage;

        transform.right = direction.normalized;

        _playerScore = score;

        if (_playerScore == null)
        {
            Debug.Log("No PS Init");
        }
    }
    


}
