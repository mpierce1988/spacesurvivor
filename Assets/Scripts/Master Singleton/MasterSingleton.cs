using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterSingleton : MonoBehaviour
{
    public static MasterSingleton instance;

    private SceneLoader _sceneLoader;
    private InputController _inputController;

    public SceneLoader SceneLoader 
    { 
        get 
        { 
            if(_sceneLoader == null)
            {
                _sceneLoader = GetComponentInChildren<SceneLoader>();
            }
            return _sceneLoader; 
        } 
    }
    public InputController InputController 
    { 
        get 
        {
            if(_inputController == null)
            {
                _inputController = GetComponentInChildren<InputController>();
            }
            return _inputController; 
        } 
    }

    

    private void Awake()
    {
        CreateSingleton();

        if(_sceneLoader == null)
            _sceneLoader = GetComponentInChildren<SceneLoader>();
        
        if(_inputController == null)
        {
            _inputController = GetComponentInChildren<InputController>();
        }
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
