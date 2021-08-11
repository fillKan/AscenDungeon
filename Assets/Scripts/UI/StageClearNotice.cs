using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageClearNotice : MonoBehaviour
{
    [SerializeField]
    private Animator _Animator;
    private string _AnimParameterName;

    private void Start()
    {
        if (string.IsNullOrEmpty(_AnimParameterName))
        {
            _AnimParameterName = _Animator.GetParameter(0).name;
            gameObject.SetActive(false);
        }
        StageEventLibrary.Instance.StageClearEvent += Show;
        StageEventLibrary.Instance.StageEnterEvent += Hide;
    }
    private void Show()
    {
        _Animator.SetBool(_AnimParameterName, true);
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        _Animator.SetBool(_AnimParameterName, false);
    }
    private void AE_AnimPlayOver()
    {
        gameObject.SetActive(false);
    }
}
