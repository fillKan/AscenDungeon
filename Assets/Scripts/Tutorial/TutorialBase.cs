using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TutorialBase : MonoBehaviour
{
    public event Action OnTutorialClearEvent;

    /// <summary>
    /// 튜토리얼을 진행한다~
    /// </summary>
    public abstract void StartTutorial();

    /// <summary>
    /// 튜토리얼 클리어 판정
    /// </summary>
    protected void OnTutorialClear()
    {
        OnTutorialClearEvent?.Invoke();
    }
}
