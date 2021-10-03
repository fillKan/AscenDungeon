using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTutorial : TutorialBase
{
    public override void StartTutorial()
    {
        base.StartTutorial();
        Castle.Instance.CanPlayerFloorNotify = false;

        Inventory.Instance.MoveUpDownEvent += (pos, dir) =>
        {
            // 다음 층으로 이동한다면
            if (dir == Direction.Up && pos == UnitizedPosV.BOT)
            {
                StageClearNotice.Instance.Hide();
            }
        };
    }
}
