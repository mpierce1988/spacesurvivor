using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterSingleton : MonoBehaviour
{
    public static MasterSingleton instance;

    private SceneLoader _sceneLoader;
    private InputController _inputController;

    public SceneLoader SceneLoader => _sceneLoader;
    public InputController InputController { get { return _inputController; } }

    

    private void Awake()
    {
        CreateSingleton();

        _sceneLoader = GetComponentInChildren<SceneLoader>();
    }

    private void CreateSingleton()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    
}
