using UnityEngine;
using UnityEngine.UI;

public class PlayerUIHealthBarHandler : MonoBehaviour
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
        healthPointsBar.fillAmount = (float)halthHandler.HealthPoints / (float)halthHandler._maxhealthPoints;
    }

}
