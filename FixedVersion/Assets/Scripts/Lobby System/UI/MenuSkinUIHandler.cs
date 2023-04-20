using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSkinUIHandler : MonoBehaviour
{
    //color FFEC00;

    //[SerializeField]
    //Button[] buttons;

    [SerializeField]
    Image[] imageOfCheck;

    public enum PlayerSkin
    {
        none = -1,
        blueFarmer = 0,
        orangeFarmer = 1,
        pinkFarmer = 2
    }

    //PlayerSkin choosedSkin = (PlayerSkin)(-1);

    private void Start()
    {
        //imageOfCheck = new Image[buttons.Length];

    }

    public void OnChooseSkin(int numberOfSkin)
    {

        DataHolderPlayer.playerSkin = (PlayerSkin)numberOfSkin;
        imageOfCheck[numberOfSkin].color = new Color(255, 235, 0, 255);
    }

    public void ClearButtons()
    {
        foreach (var item in imageOfCheck)
        {
            item.color = new Color(255, 255, 255, 255);
        }
    }

}
