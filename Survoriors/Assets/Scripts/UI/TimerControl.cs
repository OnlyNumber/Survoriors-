using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerControl : MonoBehaviour
{
    private TMP_Text _timerText;

    private float _timeTicker;

    private void Start()
    {
        _timeTicker = 0;

        _timerText = GetComponent<TMP_Text>();

        //Debug.Log(59 % 60);
    }

    private void Update()
    {
        if (_timeTicker > 0)
        {
            UpdateTimer();
        }
    }

    public void SetTimer(float time)
    {
        _timeTicker = time;
    }

    private void UpdateTimer()
    {
        //Debug.Log(_timeTicker);

        _timeTicker -= Time.deltaTime;

        //Debug.Log(_timeTicker % 60);

        _timerText.text = $" {(int)(_timeTicker / 60)} : {(int)(_timeTicker % 60)}";
    }


}
