using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ArrivingTutorial : TutorialBase
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            // TODO :: 튜토리얼 클리어 판정
        }
    }
}
