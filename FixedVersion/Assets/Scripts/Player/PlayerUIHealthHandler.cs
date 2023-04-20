using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIHealthHandler : MonoBehaviour
{
    [SerializeField]
    private HealthHandler halthHandler;


    private Image healthPointsBar;

    private void Start()
    {
        healthPointsBar = GameObject.Find("HealthPoints").GetComponent<Image>();

        halthHandler.onChangeHealth += ChangeHealthPoints;
    }

    private void ChangeHealthPoints()
    {
        //Debug.Log($"{halthHandler.HealthPoints}  {halthHandler.HealthPoints / halthHandler._maxhealthPoints}   {halthHandler._maxhealthPoints}");

        healthPointsBar.fillAmount = (float)halthHandler.HealthPoints / (float)halthHandler._maxhealthPoints;
    }

}
