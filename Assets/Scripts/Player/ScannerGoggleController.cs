using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class ScannerGoggleController : MonoBehaviour
{
    [SerializeField]
    private Transform barTransform;
    [SerializeField]
    private float speed = 40f;
    Vector4 tempPos = Vector3.zero;
    private float direction = 1f;
    private RectTransform _rect;

    private Vector2 screenBounds;

    private void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        _rect = GetComponent<RectTransform>();


        Debug.Log(_rect.anchoredPosition3D.x + "  " + -_rect.anchoredPosition3D.x);
    }
    private void Update()
    {
        Move(-direction);
        
    }
    private void LateUpdate()
    {
        if(_rect.position.x < 0 || _rect.position.x > -_rect.anchoredPosition3D.x)
        {
            direction *= -1f;
        }

    }
    private void Move(float x)
    {
        tempPos = barTransform.position;
        tempPos.x += (Time.deltaTime * x) * speed;
        barTransform.position = tempPos;
    }



}
