﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour
{
    [SerializeField]
    private string[] mSenseTags;
    
    private List<GameObject> mEnterObjects;

    private void Awake()
    {
        mEnterObjects = new List<GameObject>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        for (int i = 0; i < mSenseTags.Length; ++i)
        {
            if (collision.CompareTag(mSenseTags[i]))
            {
                mEnterObjects.Add(collision.gameObject);

                break;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        for (int i = 0; i < mSenseTags.Length; ++i)
        {
            if (collision.CompareTag(mSenseTags[i]))
            {
                mEnterObjects.Remove(collision.gameObject);

                break;
            }
        }
    }

    public GameObject[] EnterObjects()
    {
        return mEnterObjects.ToArray();
    }

    public T[] GetEnterTypeT<T>() where T : class
    {
        List<T> TContainer = new List<T>();

        for (int i = 0; i < mEnterObjects.Count; ++i)
        {
            if (mEnterObjects[i].TryGetComponent(out T instnace))
            {
                TContainer.Add(instnace);

            }
        }
        return TContainer.ToArray();
    }
}
