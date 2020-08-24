﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chief : EnemyBase, IObject, ICombat
{
    [SerializeField] private float mWaitSummonTotem;
    [SerializeField] private float mWaitContinuousAttack;

    [SerializeField] private GameObject[] mTotems;

    private Timer mWaitForSummonTotem;
    private Timer mWaitForContinuousAttack;

    private Timer mWaitForMove;

    [SerializeField] private StatTable mStat;

    private Dictionary<STAT_ON_TABLE, float> mStatTable;

    public override StatTable Stat => mStat;

    public override void Damaged(float damage, GameObject attacker, out GameObject victim)
    {
        victim = gameObject;

        if ((mStatTable[STAT_ON_TABLE.CURHEALTH] -= damage) <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public override void IInit()
    {
        Debug.Assert(Stat.GetTable(gameObject.GetHashCode(), out mStatTable));

        mWaitForSummonTotem      = new Timer();
        mWaitForContinuousAttack = new Timer();
        mWaitForMove             = new Timer();

             mWaitForSummonTotem.Start(mWaitSummonTotem);
        mWaitForContinuousAttack.Start(mWaitContinuousAttack);
    }

    public override bool IsActive()
    {
        return gameObject.activeSelf;
    }

    public override void IUpdate()
    {
        if (mWaitForSummonTotem.IsOver())
        {
            Skill_summonTotem();

            mWaitForSummonTotem.Start(mWaitSummonTotem);
        }
        else
        {
            mWaitForSummonTotem.Update();
        }

        if (mWaitForContinuousAttack.IsOver())
        {
            if (mPlayer != null)
            {
                if (IsInReachPlayer())
                {
                    Skill_continuousAttack();

                    mWaitForContinuousAttack.Start(mWaitContinuousAttack);
                }
            }
        }
        else
        {
            mWaitForContinuousAttack.Update();
        }
    }

    public override void PlayerEnter(MESSAGE message, Player enterPlayer)
    {
        mPlayer = enterPlayer;
    }

    public override void PlayerExit(MESSAGE message)
    {
        if (message.Equals(MESSAGE.BELONG_FLOOR))
        {
            mPlayer = null;
        }
    }

    public override void CastBuff(BUFF buffType, IEnumerator castedBuff)
    {
        StartCoroutine(castedBuff);
    }

    public override GameObject ThisObject() => gameObject;

    private void Skill_summonTotem(int summonCount = 2)
    {
        for (int i = 0; i < summonCount; i++)
        {
            GameObject totem = Instantiate(mTotems[Random.Range(0, mTotems.Length)], transform.parent, false);
        }
    }
    private void Skill_continuousAttack()
    {

    }
}
