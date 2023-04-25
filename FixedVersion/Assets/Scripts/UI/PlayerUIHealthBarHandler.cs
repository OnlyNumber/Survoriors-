using UnityEngine;
using TMPro;

public class PlayerUIHealthBarHandler : MonoBehaviour
{
    [SerializeField]
    private HealthHandler halthHandler;

    private TMP_Text healthPointsBar;

    private const string HEALTH_BAR_INDICATOR = "HealthPointsIndicator";

    private void Start()
    {
        healthPointsBar = GameObject.Find(HEALTH_BAR_INDICATOR).GetComponent<TMP_Text>();

        healthPointsBar.text = $"HP: {halthHandler.HealthPoints}/{halthHandler._maxhealthPoints}";

        halthHandler.onChangeHealth += ChangeHealthPoints;
    }

    private void ChangeHealthPoints()
    {
        healthPointsBar.text = $"HP: {halthHandler.HealthPoints}/{halthHandler._maxhealthPoints}";

        //healthPointsBar.fillAmount = (float)halthHandler.HealthPoints / (float)halthHandler._maxhealthPoints;
    }

}
