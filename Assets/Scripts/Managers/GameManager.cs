using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [HideInInspector]
    public PlayerController playerController;

    private Transform _cameraTransform;
    public Transform CameraTransform
    {
        get 
        {
            if (_cameraTransform == null)
                _cameraTransform = Camera.main.transform;
            return _cameraTransform;
        }
        private set 
        {
            _cameraTransform = value; 
        }
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;   
        Cursor.visible = false;
    }

}
