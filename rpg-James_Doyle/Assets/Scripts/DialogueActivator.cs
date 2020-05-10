﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueActivator : MonoBehaviour
{

    //array of character lines
    public string[] lines;

    private bool canActivate;

    public bool isNPC = true;

    [Header("Quest Settings")]
    public bool shouldActivateQuest;
    public string questToMark;
    public bool markComplete;
    //public bool markIncomplete;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canActivate && Input.GetButtonDown("Fire1") && !DialogManager.instance.dialogueBox.activeInHierarchy)
        {
            DialogManager.instance.ShowDialog(lines, isNPC);

            if (shouldActivateQuest)
            {
                DialogManager.instance.ShouldActivateQuestAtEnd(questToMark, markComplete);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            canActivate = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            canActivate = false;
        }
    }
}
