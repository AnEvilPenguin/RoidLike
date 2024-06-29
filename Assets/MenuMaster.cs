using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMaster : MonoBehaviour
{
    public void HandleNewGameClick() =>
        SceneManager.LoadScene(1);

    public void HandleQuitClick() =>
        Application.Quit();
}
