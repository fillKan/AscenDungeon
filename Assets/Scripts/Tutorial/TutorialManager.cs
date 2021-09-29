using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : Singleton<TutorialManager>
{
    [Header("Dialog Property")]
    [SerializeField] private DialogData _TutorialDialog;

    [Header("Tutorial Property")]
    [SerializeField] private TutorialBase[] _TutorialArray;

    private int _CurrentGroupIndex = 1;
    private int _CurrentTutorialIndex = 0;

    private Queue<DialogIndex> _DialogQueue = new Queue<DialogIndex>();

    private void Start()
    {
        Dialog.Instance.OnTouchOutputFinish += BeginOfTutorial;
        
        MakeDialogGroup(_CurrentGroupIndex);
        WriteLog();
    }

    /// <summary>
    /// 튜토리얼 진행을 위한 준비
    /// </summary>
    private void BeginOfTutorial()
    {
        if (_DialogQueue.Count == 0)
        {
            ProgressTutorial();
            MakeDialogGroup(++_CurrentGroupIndex);
            return;
        }
        WriteLog();
    }

    /// <summary>
    /// 튜토리얼 진행 !
    /// </summary>
    private void ProgressTutorial()
    {
        Dialog.Instance.CloseLog();

        // 로그가 닫히면, 플레이어는 움직일 수 있다.
        // 다만 움직임을 제약해야 한다면...그건 튜토리얼에서 처리하도록 한다!
        PlayerActionManager.Instance.SetEnableController(true);

        if (_CurrentTutorialIndex >= _TutorialArray.Length)
            return;
        
        _TutorialArray[_CurrentTutorialIndex].StartTutorial();
        _TutorialArray[_CurrentTutorialIndex].OnTutorialClearEvent += WriteLog;
        
        _CurrentTutorialIndex++;
    }

    // 입력한 인덱스의 다이얼로그 그룹을 형성한다.
    private void MakeDialogGroup(int groupIndex)
    {
        foreach (var data in _TutorialDialog.Data)
        {
            // 정렬되어있다는 가정하에...
            if (data.GroupCode == groupIndex) {
                _DialogQueue.Enqueue(data);
            }
        }
    }
    
    private void WriteLog()
    {
        // 로그가 출력되는 동안에는 플레이어가 움직일 수 없다-
        PlayerActionManager.Instance.SetEnableController(false);

        var logData = _DialogQueue.Dequeue();
        Dialog.Instance.WriteLog(logData.Name, logData.Text, null);
    }
}
