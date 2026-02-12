using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class BossUI : MonoBehaviour
{
    public List<GameObject> BossLock;
    public List<GameObject> BossButton;
    public List<GameObject> BossClear;
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

    // Start is called before the first frame update
    private void OnEnable()
    {
        SetBoss();
    }

    public void BossFight(int i)
    {
        SaveDataManager.Instance.BossFight = true;
        SaveDataManager.Instance.FightBossNum = i;
        SceneManager.LoadScene("BossFight");
    }

}
