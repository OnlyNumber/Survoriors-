using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    private byte[] _connectionToken;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if( instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if(_connectionToken == null)
        {
            _connectionToken = ConnectionTokenUtils.NewToken();
            Debug.Log($"Plater connection token {ConnectionTokenUtils.HashToken(_connectionToken)}");
        }
    }


    public void SetConnectionToken(byte[] connectionToken)
    {
        this._connectionToken = connectionToken;
    }

    public byte[] GetConnectionToken()
    {
        return _connectionToken;
    }


}
