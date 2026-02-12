using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Stage_Boss : MonoBehaviour
{
    public float TimeOut=300f;
    public TMP_Text TimeOutText;

    public SpriteRenderer SR;
    public List<Sprite> images;
    public List<GameObject> Boss_Effect;

    public TMP_Text PlayerHPText;
    public float PlayerMaxHP;
    public float PlayerNowHP;
    public float PlayerStageHP = 10;

    public int BossNum = 0;
    public TMP_Text BossHPText;
    public float BossMaxHP;
    public float BossNowHP;
    public float BossStageHP = 500f;

    public bool FightOver = false;
    bool bossAttack = false;

    public GameObject BossKillText;
    public GameObject YouFailedText;
    public GameObject DieEffect;
    public SpellFire SpellFires;

    private void Start()
    {
        StartCoroutine(SetBoss());
    }

    IEnumerator SetBoss()
    {
        yield return new WaitForSeconds(0.1f);
        PlayerMaxHP = (SaveDataManager.Instance.StageClear+1) * PlayerStageHP;
        PlayerNowHP = PlayerMaxHP;
        PlayerHPSet();
        BossNum = SaveDataManager.Instance.FightBossNum;
        BossMaxHP = BossStageHP;
        for (int i = 0; i < BossNum; i++)
        {
            BossMaxHP *= 2;
        }
        BossNowHP = BossMaxHP;
        BossHPSet();
        SR.sprite = images[BossNum];
        StartCoroutine(BossAttack());
    }

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
        if (0 < BossNowHP && !FightOver)
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
        if (0 < PlayerNowHP && !FightOver)
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

    IEnumerator BossAttack()
    {
        yield return new WaitForSeconds(5f);
        if (0 < BossNowHP)
        {
            bossAttack = true;
            StartCoroutine(BossAttackEffect());
            StartCoroutine(BossAttack());
        }
    }

    IEnumerator BossAttackEffect()
    {
        yield return new WaitForSeconds(0.25f);
        bossAttack = false;
        Destroy(Instantiate(Boss_Effect[BossNum], transform.position, Quaternion.identity), 2f);
        yield return new WaitForSeconds(0.5f);
        PlayerHit((BossNum+1) * (BossNum + 1)*10);

    }

    IEnumerator BossKillSucces()
    {
        StopCoroutine(BossAttackEffect());
        StopCoroutine(BossAttack());
        bossAttack = false;
        BossKillText.SetActive(true);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Game");
    }

    IEnumerator YouFailed()
    {
        StopCoroutine(BossAttackEffect());
        StopCoroutine(BossAttack());
        bossAttack = false;
        YouFailedText.SetActive(true);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Game");
    }



    private void FixedUpdate()
    {

        if (bossAttack)
        {
            transform.Translate(Vector2.down * Time.deltaTime * 4f);
        }
        else if( transform.position.y < 6.5f)
        {
            transform.Translate(Vector2.up * Time.deltaTime * 4f);
        }
        else if( 6.5f<transform.position.y)
        {
            transform.position = new Vector2(0, 6.5f);
        }


        if (!FightOver)
        {
            TimeOut = TimeOut - Time.deltaTime;
            TimeOutText.text = "TimeOut" + (int)(TimeOut / 60) + ":" + (int)(TimeOut % 60);
            if (TimeOut <= 0)
            {
                PlayerLose();
            }
        }
        
    }
}
