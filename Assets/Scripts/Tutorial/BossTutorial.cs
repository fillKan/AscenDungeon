using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTutorial : TutorialBase
{
    public override void StartTutorial()
    {
        base.StartTutorial();
        Castle.Instance.CanPlayerFloorNotify = false;

        return;
        Inventory.Instance.MoveUpDownEvent += (pos, dir) =>
        {
            // 다음 층으로 이동한다면
            if (dir == Direction.Up && pos == UnitizedPosV.BOT)
            {

                MainCamera.Instance.Fade(0.6f, FadeType.In, () =>
                {
                    Camera.main.transform.position = 
                        Castle.Instance.PlayerFloor.transform.localPosition + Vector3.back * 10;
                    
                    MainCamera.Instance.Fade(0.6f, FadeType.Out);
                });
            }
        };
    }
}
