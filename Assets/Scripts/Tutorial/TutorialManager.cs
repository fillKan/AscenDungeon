using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : Singleton<TutorialManager>
{
    [SerializeField] private DialogData _TutorialDialog;

    private int _CurrentGroupIndex = 1;
    private Queue<DialogIndex> _DialogQueue = new Queue<DialogIndex>();

    private void Awake()
    {
        Dialog.Instance.OnTouchOutputFinish += () => 
        {
            if (_DialogQueue.Count == 0) {
                MakeDialogGroup(++_CurrentGroupIndex);

                // 출력할 수 있는 로그를 모두 출력했다면
                if (_DialogQueue.Count == 0)
                {
                    Dialog.Instance.CloseLog();
                    return;
                }
            }
            var logData = _DialogQueue.Dequeue();
            Dialog.Instance.WriteLog(logData.Name, logData.Text, null);
        };
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
}
