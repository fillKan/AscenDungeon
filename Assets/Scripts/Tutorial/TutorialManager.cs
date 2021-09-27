using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : Singleton<TutorialManager>
{
    // 튜토리얼의 다이얼로그 데이터.
    [SerializeField] private DialogData _TutorialDialog;

    // 튜토리얼에 대한 리스트.
    [SerializeField] private List<TutorialBase> _ListOfTutorial;
    
    // 튜토리얼 다이얼로그의 인덱스.
    private int _DialogGroupIndex = 0;

    // 현재 진행중인 튜토리얼의 인덱스.
    private int _CurrentTutorialIndex = 0;

    private Action _DialogEndCallBack;

    public void NextStep(Action callBack)
    {
        _DialogEndCallBack = callBack;

        int index = 0;

        var name = _TutorialDialog.Data[index].Name;
        var text = _TutorialDialog.Data[index].Text;

        Dialog.Instance.WriteLog(name, text, () =>
        {
            NextStep(++index);
        });
    }

    private void NextStep(int index)
    {
        var data = _TutorialDialog.Data[index];
        if (data.GroupCode != _DialogGroupIndex)
        {
            OnDialogEnd();
            return;
        }
        var name = data.Name;
        var text = data.Text;

        Dialog.Instance.WriteLog(name, text, () =>
        {
            NextStep(++index);
        });
    }

    private void OnDialogEnd()
    {
        _DialogGroupIndex++;
        _DialogEndCallBack?.Invoke();

        _ListOfTutorial[_CurrentTutorialIndex].StartTutorial();
    }

    private IEnumerator TutorialRoutine()
    {
        

        yield return null;
    }
}
