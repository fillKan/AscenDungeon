using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialNPC : NPC
{
    public override void Interaction()
    {
        SystemMessage.Instance.ShowCheckMessage("튜토리얼을\n진행하시겠습니까?", result => 
        {
            if (result)
            {
                SceneManager.LoadScene((int)SceneIndex.Tutorial);
            }
        });
    }
}
