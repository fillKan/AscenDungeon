using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrivingTutorial : TutorialBase
{
    private void Reset()
    {
        if (!TryGetComponent(out BoxCollider2D collider))
        {
            collider = gameObject.AddComponent<BoxCollider2D>();
        }
        collider.isTrigger = true;
    }

    public override void StartTutorial()
    {
        // TODO :: 튜토리얼 진행...
        gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            OnTutorialClear();
            gameObject.SetActive(false);
        }
    }
}
