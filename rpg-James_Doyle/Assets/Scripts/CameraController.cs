﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraController : MonoBehaviour
{
    //ref to obj camera focuses on
    public Transform target;

    //max positions of where the camera can move, calc'd at Start
    public Tilemap theMap;
    private Vector3 bottomLeftLimit;
    private Vector3 topRightLimit;

    //used to lock camera to game area, removing edges
    private float halfHeight;
    private float halfWidth;

    //music loader?/Controller
    public int musicToPlay;
    private bool musicStarted;

    void Start()
    {
        halfHeight = Camera.main.orthographicSize;
        halfWidth = halfHeight * Camera.main.aspect;

        //gets the minimum amount from the defined map
        bottomLeftLimit = theMap.localBounds.min + new Vector3(halfWidth, halfHeight, 0f);
        topRightLimit = theMap.localBounds.max + new Vector3(-halfWidth, -halfHeight, 0f);

        //send the play area bounds to the player func
        PlayerController.instance.SetBounds(theMap.localBounds.min, theMap.localBounds.max);
    }

    //LateUpdate calls AFTER other Update scripts
    void LateUpdate()
    {
        target = FindObjectOfType<PlayerController>().transform; //searches through scene objects for player controller script if it's present

        //update the camera every frame
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);

        //keep camera inside map boundary
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, bottomLeftLimit.x, topRightLimit.x), 
            Mathf.Clamp(transform.position.y,bottomLeftLimit.y,topRightLimit.y), transform.position.z);

        if (!musicStarted)
        {
            musicStarted = true;
            AudioManager.instance.PlayBGM(musicToPlay);
        }
        

    }
}
