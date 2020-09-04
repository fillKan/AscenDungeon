﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scarecrow : EnemyBase
{
    private Timer mWaitForATK;
    private Timer mWaitForMove;

    [SerializeField] private float mDamage;

    [SerializeField] private AbilityTable mAbilityTable;

    private Dictionary<STAT_ON_TABLE, float> mPersonalTable;

    public override AbilityTable GetAbility => mAbilityTable;

    public override void Damaged(float damage, GameObject attacker)
    {
        if ((mPersonalTable[STAT_ON_TABLE.CURHEALTH] -= damage) <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public override void IInit()
    {
        mWaitForATK  = new Timer();
        mWaitForMove = new Timer();

        Debug.Assert(mAbilityTable.GetTable(gameObject.GetHashCode(), out mPersonalTable));

        mWaitForATK.Start(mWaitATKTime);
    }

    public override bool IsActive()
    {
        return gameObject.activeSelf;
    }

    public override void IUpdate()
    {
        if (mWaitForMove.IsOver())
        {
            if (IsMoveFinish && !IsInReachPlayer())
            {
                Vector2 movePoint;

                movePoint.x = Random.Range(-mHalfMoveRangeX, mHalfMoveRangeX) + mOriginPosition.x;
                movePoint.y = Random.Range(-mHalfMoveRangeY, mHalfMoveRangeY) + mOriginPosition.y;

                if (mPlayer != null)
                {
                    Vector2 lookingDir = movePoint.x > transform.localPosition.x ? Vector2.right : Vector2.left;

                    Vector2 playerPos;

                    if ((IsLookAtPlayer(lookingDir) || IsLookAtPlayer()) && mPlayer.Position(out playerPos))
                    {
                        movePoint = PositionLocalized(playerPos);

                        if (!IsPointOnRange(movePoint))
                        {
                            movePoint -= (movePoint.x > transform.localPosition.x ? Vector2.right : Vector2.left) * mRange;

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

        if (IsInReachPlayer())
        {
            if (mWaitForATK.IsOver())
            {
                mPlayer.Damaged(mAbilityTable.RAttackPower, gameObject);

                mWaitForATK.Start(mWaitATKTime);
            }
            else
            {
                mWaitForATK.Update();
            }
        }
    }

    protected override void MoveFinish()
    {
        mWaitForMove.Start(WaitMoveTime);
    }

    public override void PlayerEnter(MESSAGE message, Player enterPlayer)
    {
        if (message.Equals(MESSAGE.THIS_ROOM))
        {
            mPlayer = enterPlayer;
        }
    }

    public override void PlayerExit(MESSAGE message)
    {
        mPlayer = null;
    }

    public override GameObject ThisObject()
    {
        return gameObject;
    }

    public override void CastBuff(BUFF buffType, IEnumerator castedBuff)
    {
        StartCoroutine(castedBuff);
    }
}