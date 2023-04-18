using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    [SerializeField]
    private Button startGameButton;

    public void SetStartButtonActive(bool active)
    {
        startGameButton.gameObject.SetActive(active);
    }
}
