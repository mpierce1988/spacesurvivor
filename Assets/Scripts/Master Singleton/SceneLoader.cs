using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadGameplay()
    {
        SceneManager.LoadScene("gameplay");
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("menu");
    }
}
