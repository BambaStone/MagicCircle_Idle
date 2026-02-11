using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Stage_Boss : MonoBehaviour
{
    public float TimeOut;
    public bool Attack;
    public TMP_Text StageText;
    public TMP_Text TimeOutText;
    public float MaxTimeOut = 300f;

    public SpriteRenderer SR;
    public List<Sprite> images;

    public TMP_Text PlayerHPText;
    public float PlayerMaxHP;
    public float PlayerNowHP;
    public float PlyaerStageHP = 10;

    public int BossNum = 0;
    public TMP_Text BossHPText;
    public float BossMaxHP;
    public float BossNowHP;
    public float BossStageHP = 100f;

    public bool FightOver = false;

    public GameObject BossKillText;
    public GameObject YouFailedText;
    public GameObject DieEffect;
    public SpellFire SpellFires;

    void PlayerHPSet()
    {
        string hp = "HP : "+float.Parse(PlayerNowHP.ToString("N2")) + "/" + PlayerMaxHP + " ";
        float ShowHP = PlayerNowHP / (PlayerMaxHP * 0.1f);

        for (int i = 0; i < ShowHP; i++)
        {
            hp = hp + "l";
        }

        PlayerHPText.text = hp;
    }
    void BossHPSet()
    {
        string hp = float.Parse(BossNowHP.ToString("N2")) + "/" + BossMaxHP + " ";
        float ShowHP = BossNowHP / (BossMaxHP * 0.1f);

        for (int i = 0; i < ShowHP; i++)
        {
            hp = hp + "l";
        }

        BossHPText.text = hp;
    }


    public void BossHit(float Damage)
    {
        if (0 < BossNowHP)
        {
            BossNowHP = BossNowHP - Damage;
            BossHPSet();
            if (BossNowHP <= 0)
            {
                DieBoss();
            }
        }
    }

    public void PlayerHit(float Damage)
    {
        if (0 < PlayerNowHP)
        {
            PlayerNowHP = PlayerNowHP - Damage;
            PlayerHPSet();
            if (PlayerNowHP <= 0)
            {
                PlayerLose();
            }
        }
    }

    public void PlayerLose()
    {
        FightOver = true;
        SpellFires.Stop();
        StartCoroutine(YouFailed());
    }

    public void DieBoss()
    {
        SpellFires.Stop();
        FightOver = true;
        SaveDataManager.Instance.QuestValue[1]++;
        SaveDataManager.Instance.BossKill = true;
        Destroy(Instantiate(DieEffect, transform.position, Quaternion.identity), 1f);
        SR.gameObject.SetActive(false);
        BossHPText.gameObject.SetActive(false);
        if (SaveDataManager.Instance.BossClear==BossNum)
        {
            SaveDataManager.Instance.BossClear++;
            SaveDataManager.Instance.QuestValue[9] = SaveDataManager.Instance.BossClear;
        }
        StartCoroutine(BossKillSucces());
    }

    IEnumerator BossKillSucces()
    {
        BossKillText.SetActive(true);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Game");
    }

    IEnumerator YouFailed()
    {
        YouFailedText.SetActive(true);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Game");
    }



    private void FixedUpdate()
    {
        if (!FightOver)
        {
            TimeOut = TimeOut - Time.deltaTime;
            TimeOutText.text = "TimeOut" + (int)(TimeOut / 60) + ":" + (int)(TimeOut % 60);
            if (TimeOut <= 0)
            {
                PlayerLose();
            }
        }
        if(FightOver)
        {
            if(0<BossNowHP)
            {
                transform.Translate(Vector2.up*Time.deltaTime);
            }
        }
    }
}
