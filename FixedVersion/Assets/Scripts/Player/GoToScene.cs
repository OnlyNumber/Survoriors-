using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GoToScene : MonoBehaviour
{
    [ContextMenu("GoToMenu")]
    public void GoMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
