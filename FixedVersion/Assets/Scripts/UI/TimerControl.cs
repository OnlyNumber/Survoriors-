using UnityEngine;
using TMPro;

public class TimerControl : MonoBehaviour
{
    private TMP_Text _timerText;

    private float _timeTicker;

    private void Start()
    {
        _timeTicker = 0;

        _timerText = GetComponent<TMP_Text>();
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
        _timeTicker -= Time.deltaTime;
        
        if (_timeTicker % 60 > 10)
        {
            _timerText.text = $" {(int)(_timeTicker / 60)} : {(int)(_timeTicker % 60)}";
        }
        else
        {
            _timerText.text = $" {(int)(_timeTicker / 60)} : 0{(int)(_timeTicker % 60)}";
        }

    }


}
