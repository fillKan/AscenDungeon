﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scarecrow : EnemyBase
{
    private Timer mWaitForATK;
    private Timer mWaitForMove;

    public override void Damaged(float damage, GameObject attacker, out GameObject victim)
    {
        victim = gameObject;
    }

    public override void IInit()
    {
        mWaitForATK  = new Timer();
        mWaitForMove = new Timer();
    }

    public override bool IsActive()
    {
        return gameObject.activeSelf;
    }

    public override void IUpdate()
    {
        if (mWaitForMove.IsOver())
        {
            if (IsMoveFinish)
            {
                Vector2 movePoint;

                movePoint.x = Random.Range(-mHalfMoveRangeX, mHalfMoveRangeX) + mOriginPosition.x;
                movePoint.y = Random.Range(-mHalfMoveRangeY, mHalfMoveRangeY) + mOriginPosition.y;

                if (mPlayer != null)
                {
                    Vector2 playerPos;

                    Vector2 lookingDir = movePoint.x > transform.localPosition.x ? Vector2.right : Vector2.left;

                    if (IsLookAtPlayer(out playerPos, lookingDir))
                    {
                        if (!IsPointOnRange(playerPos))
                        {
                            movePoint -= lookingDir * mRange;
                        }
                        else
                        {
                            movePoint = transform.localPosition;
                        }
                    }
                }
                MoveToPoint(movePoint);
            }
        }
        else
        {
            mWaitForMove.Update();
        }
    }

    protected override void MoveFinish()
    {
        mWaitForMove.Start(WaitMoveTime);
    }

    public override void PlayerEnter(Player enterPlayer)
    {
        mPlayer = enterPlayer;
    }

    public override void PlayerExit()
    {
        mPlayer = null;
    }

    public override GameObject ThisObject()
    {
        return gameObject;
    }
}
