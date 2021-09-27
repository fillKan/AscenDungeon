using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ArrivingTutorial : TutorialBase
{
    protected override void TutorialClear(Action callBack)
    {
        TutorialManager.Instance.NextStep(() => 
        {
            PlayerActionManager.Instance.SetMoveLock(false);
            callBack?.Invoke();
        });
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            IsClear = true;
            PlayerActionManager.Instance.SetMoveLock(true);
        }
    }
}
