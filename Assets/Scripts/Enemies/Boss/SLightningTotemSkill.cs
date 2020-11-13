﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SLightningTotemSkill : MonoBehaviour
{
    public Action<SLightningTotemSkill> CastOverAction;

    [SerializeField] 
    private SLightningTotem   SLightningTotem;
    private SLightningTotem[] mLightningTotems;

    private IEnumerator mESkillCasting;

    public void Init()
    {
        mLightningTotems = new SLightningTotem[3];

        for (int i = 0; i < 3; i++)
        {
            mLightningTotems[i] = Instantiate(SLightningTotem);
            mLightningTotems[i].Init();
        }
    }
    public void Cast()
    {
        if (mESkillCasting == null)
        {
            StartCoroutine(mESkillCasting = SkillCasting());
        }
    }
    private IEnumerator SkillCasting()
    {
        DIRECTION9 startDIR =
            DIRECTION9.BOT_LEFT + Kimbokchi.Utility.LuckyNumber(0.5f, 0f, 0.5f);

        for (int i = 0; i < 3; i++)
        {
            int direction = i;

            if (startDIR.Equals(DIRECTION9.BOT_RIGHT))
            {
                direction *= -1;
            }
            mLightningTotems[i].Cast(Castle.Instance.GetMovePoint(startDIR + direction));

            yield return new WaitForSeconds(1f);
        }
        yield return new WaitUntil(() => mLightningTotems.All(o => o.IsOver));

        mESkillCasting = null;
        CastOverAction?.Invoke(this);
    }
}
