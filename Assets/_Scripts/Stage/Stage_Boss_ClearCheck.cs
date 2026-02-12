using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage_Boss_ClearCheck : MonoBehaviour
{
    void Start()
    {
        if (SaveDataManager.Instance.BossFight)
        {
            SaveDataManager.Instance.BossKillCheck();
        }
    }

}
