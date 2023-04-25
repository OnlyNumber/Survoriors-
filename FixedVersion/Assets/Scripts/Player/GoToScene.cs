using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GoToScene : MonoBehaviour
{

    private const string NEXT_SCENE = "MenuScene";

    public void GoMenu()
    {
        SceneManager.LoadScene(NEXT_SCENE);
    }
}
