using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TutorialBase : MonoBehaviour
{
    /// <summary>
    /// 해당 튜토리얼의 클리어여부
    /// </summary>
    public bool IsClear
    {
        get;
        protected set;
    }

    /// <summary>
    /// 튜토리얼의 시작 동작.
    /// </summary>
    public virtual void StartTutorial()
    {
        
    }

    /// <summary>
    /// 튜토리얼의 클리어 동작
    /// </summary>
    protected abstract void TutorialClear(Action callBack);
}
