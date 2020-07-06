﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    [SerializeField]
    [Tooltip("가장 높이있는 방일수록 가장 작은 인덱스에 저장해 주세요!")]
    private Room[] mMemberRooms = new Room[3];

    public void IInit()
    {
        mMemberRooms[0].IInit(this);
        mMemberRooms[1].IInit(this);
        mMemberRooms[2].IInit(this);
    }

    public void IUpdate()
    {
        mMemberRooms[0].IUpdate();
        mMemberRooms[1].IUpdate();
        mMemberRooms[2].IUpdate();
    }

    public void EnterPlayer(POSITION3 position)
    {
        // 플레이어가 해당 층에 존재한다! [강림]
        mMemberRooms[(int)position].EnterPlayer();

        Debug.Log("강림");
    }
    public void ExitPlayer(POSITION3 position)
    {
        // 플레이어가 나갔다..
        mMemberRooms[(int)position].ExitPlayer();

        Debug.Log("퇴장!");
    }

    public Vector2[] GetMovePoints(POSITION3 position)
    {
        return mMemberRooms[(int)position].GetMovePoints();
    }
}
