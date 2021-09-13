using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog : Singleton<Dialog>
{
    [SerializeField] private float _WriteDelay = 0.1f;

    [SerializeField] private TMPro.TextMeshProUGUI _LogText;

    [SerializeField] [TextArea(3, 6)] private string _TestOfTextField;

    private Queue<string> _TextQueue = new Queue<string>();

    private StringBuilder _QueueBuilder = new StringBuilder();
    private StringBuilder _WriteBuilder = new StringBuilder();

    public void WriteLog(string text, Action callBack)
    {
        SetTextQueue(text);
        StartCoroutine(WriteLogRoutine(callBack));
    }

    [ContextMenu("TestOfWriteLog")]
    private void TestOfWriteLog()
    {
        WriteLog(_TestOfTextField, null);
    }

    private void SetTextQueue(string text)
    {
        _QueueBuilder.Clear();

        for (int i = 0; i < text.Length; ++i)
        {
            char character = text[i];
            _QueueBuilder.Append(character);

            if (character.Equals(' '))
            {
                if (_QueueBuilder.ToString() == "  ")
                {
                    _QueueBuilder.Clear();
                    _QueueBuilder.AppendLine();
                }
                continue;
            }
            _TextQueue.Enqueue(_QueueBuilder.ToString());
            _QueueBuilder.Clear();
        }
    }

    private IEnumerator WriteLogRoutine(Action callBack)
    {
        _WriteBuilder.Clear();

        while (_TextQueue.Count != 0)
        {
            _WriteBuilder.Append(_TextQueue.Dequeue());
            _LogText.text = _WriteBuilder.ToString();

            yield return new WaitForSeconds(_WriteDelay);
        }
        callBack?.Invoke();
    }
}
