using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class BossUI : MonoBehaviour
{
    public List<GameObject> BossLock;//목록의 잠금이미지들
    public List<GameObject> BossButton;//목록의 버튼들
    public List<GameObject> BossClear;//클리어 이미지들

    // 보스 UI를 켰을때 목록을 초기화및 셋팅
    public void SetBoss()
    {
        for(int i=0;i<=SaveDataManager.Instance.BossClear;i++)
        {
            if (i < 12)
            {
                BossLock[i].SetActive(false);
                BossButton[i].SetActive(true);
            }
            if(0<i)
            {
                BossClear[i-1].SetActive(true);
            }
        }
    }

    private void OnEnable()
    {
        SetBoss();
    }

    //보스 사냥 버튼
    public void BossFight(int i)
    {
        SaveDataManager.Instance.BossFight = true;
        SaveDataManager.Instance.FightBossNum = i;
        SceneManager.LoadScene("BossFight");
    }

}
