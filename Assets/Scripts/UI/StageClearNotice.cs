using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageClearNotice : Singleton<StageClearNotice>
{
    [SerializeField]
    private Animator _Animator;
    private string _AnimParameterName;

    private void Start()
    {
        if (string.IsNullOrEmpty(_AnimParameterName))
        {
            _AnimParameterName = _Animator.GetParameter(0).name;
            _Animator.gameObject.SetActive(false);

            if (StageEventLibrary.Instance == null)
                return;
                
            StageEventLibrary.Instance.StageClearEvent += Show;
            StageEventLibrary.Instance.StageEnterEvent += Hide;
        }
    }
    public void Show()
    {
        _Animator.SetBool(_AnimParameterName, true);
        _Animator.gameObject.SetActive(true);
    }
    public void Hide()
    {
        _Animator.SetBool(_AnimParameterName, false);
    }
}
