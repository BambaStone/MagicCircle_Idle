using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class SaveDataManager : MonoBehaviour
{
    #region Singleton
    private static SaveDataManager s_instance = null;
    public static SaveDataManager Instance
    {
        get
        {
            if (s_instance == null) return null;
            return s_instance;
        }
    }
    private void Awake()
    {
        if (s_instance == null)
        {
            s_instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    
    public List<bool> SpellFireOn;
    public List<int> SpellLevel;
    public int SpellsCount;

    public int StageClear;
    public int BossClear;

    public int MagicForce;
    public int MagicGem;

    public float TotalPower;
    public float TotalSpeed;

    public List<int> SpellPower;

    public int UnlockSpell;
    public int Power;
    public int Speed;
    public int MaxSpellFiresCount;
    public int MaxMakeSpell;
    public int NowMakeSpell;
    public int MakeSpellLevel;
    public int MaxHaveSpell;
    public DateTime LastPlayTime;
    public DateTime NowLoginTime;


    /*
    퀘스트 목록
    0 : 몬스터킬 N회
    1 : 보스킬 N회
    2 : 총 포스 N개 획득
    3 : 총 보석 N개 획득
    4 : 파워 N레벨 달성
    5 : 스피드 N레벨 달성
    6 : 포스 N개 사용
    7 : 보석 N개 사용
    8 : 스테이지 N 클리어
    9 : 보스 N 클리어
    10 : 스펠 제작 N 회
    QuestValue는 수행횟수
    QuestClear는 퀘스트 클리어 단계 : 이 단계를 통해 수행횟수의 목표를 지정
    예시 : 0번 퀘스트를 100킬 단위로 하면
    몬스터킬을 하면 QuestValue[0]을 +1
    QuestValue[0]의 값이 (QuestClear[0]+1)*100을 달성할때마다 보상을 주고 QuestClear[0]을 +1
    */
    public List<int> QuestValue;
    public List<int> QuestClear;

    public int AFKIncome;

    public List<int> BaseDamage;
    public void TotalSpeedCal()
    {
        TotalSpeed = (Speed * 0.1f) + 1;
        for (int i = 0; i <= UnlockSpell; i++)
        {
            if (i % 2 == 1)
            {
                TotalSpeed = TotalSpeed + (SpellPower[i] * 0.01f);
            }
        }
    }

    public void TotalPowerCal()
    {
        TotalPower = (Power*0.1f)+1;
        for (int i = 0; i <= UnlockSpell; i++)
        {
            if (i == 0 || i % 2 == 0)
            {
                TotalPower = TotalPower + (SpellPower[i] * 0.01f);
            }
        }
    }

    


    // Start is called before the first frame update
    void Start()
    {
        Load();
        StartCoroutine(SaveTimer());
        BaseDamage.Add(1);
        for(int i=1;i<12;i++)
        {
            BaseDamage.Add(BaseDamage[i-1] * 2);
        }
    }

    public void Load()
    {
        StageClear = PlayerPrefs.GetInt("StageClear", 0);
        BossClear = PlayerPrefs.GetInt("BossClear", 0);
        Power = PlayerPrefs.GetInt("Power", 0);
        Speed = PlayerPrefs.GetInt("Speed", 0);
        MaxSpellFiresCount = PlayerPrefs.GetInt("MaxSpellFiresCount", 1);
        MaxMakeSpell = PlayerPrefs.GetInt("MaxMakeSpell", 1);
        NowMakeSpell = PlayerPrefs.GetInt("NowMakeSpell", MaxMakeSpell);
        MaxHaveSpell = PlayerPrefs.GetInt("MaxHaveSpell", 10);
        MakeSpellLevel = PlayerPrefs.GetInt("MakeSpellLevel", 0);

        UnlockSpell = PlayerPrefs.GetInt("UnlockSpell", 0);

        MagicForce = PlayerPrefs.GetInt("MagicForce", 0);
        MagicGem = PlayerPrefs.GetInt("MagicGem", 0);

        string tempTimeStr = PlayerPrefs.GetString("LastPlayTime", "");

        if(string.IsNullOrEmpty(tempTimeStr))
        {
            Debug.Log("First Play");
            LastPlayTime = DateTime.Now;
        }
        else
        {
            LastPlayTime = DateTime.Parse(tempTimeStr);
        }
        NowLoginTime = DateTime.Now;

        TimeSpan timeDif = NowLoginTime - LastPlayTime;
        if(0<timeDif.TotalMinutes)
        {
            NowMakeSpell = NowMakeSpell + (int)timeDif.TotalMinutes;
            if(MaxMakeSpell<NowMakeSpell)
            {
                NowMakeSpell = MaxMakeSpell;
            }
        }

        SpellsCount = PlayerPrefs.GetInt("SpellsCount", 0);
        for (int i = 0; i < SpellsCount; i++)
        {
            SpellLevel.Add( PlayerPrefs.GetInt("SpellLevel_" + i, 0));
            if (PlayerPrefs.GetInt("SpellFireOn_" + i, 0) == 1)
            {
                SpellFireOn.Add(true);
            }
            else
            {
                SpellFireOn.Add( false);
            }
        }

        for(int i=0;i<12;i++)
        {
            SpellPower.Add(PlayerPrefs.GetInt("SpellPower"+i, 0));
        }

        for(int i=0;i<21;i++)
        {
            QuestValue.Add(PlayerPrefs.GetInt("QuestValue"+i,0));
            QuestClear.Add(PlayerPrefs.GetInt("QuestClear" + i, 0));
        }

        TotalSpeedCal();
        TotalPowerCal();
    }
    public void Save()
    {

        PlayerPrefs.SetInt("StageClear", StageClear);
        PlayerPrefs.SetInt("BossClear", BossClear);
        PlayerPrefs.SetInt("Power", Power);
        PlayerPrefs.SetInt("Speed", Speed);
        PlayerPrefs.SetInt("MaxSpellFiresCount", MaxSpellFiresCount);
        PlayerPrefs.SetInt("MaxMakeSpell", MaxMakeSpell);
        PlayerPrefs.SetInt("NowMakeSpell", NowMakeSpell);
        PlayerPrefs.SetInt("MaxHaveSpell", MaxHaveSpell);
        PlayerPrefs.SetInt("MakeSpellLevel", MakeSpellLevel);
        PlayerPrefs.SetString("LastPlayTime", DateTime.Now.ToString());


        PlayerPrefs.SetInt("UnlockSpell", UnlockSpell);

        PlayerPrefs.SetInt("MagicForce", MagicForce);
        PlayerPrefs.SetInt("MagicGem", MagicGem);

        PlayerPrefs.SetInt("SpellsCount", SpellsCount);
        for (int i = 0; i < SpellsCount; i++)
        {
            PlayerPrefs.SetInt("SpellLevel_" + i, SpellLevel[i]);
            if(SpellFireOn[i])
            {
                PlayerPrefs.SetInt("SpellFireOn_" + i, 1);
            }
            else
            {
                PlayerPrefs.SetInt("SpellFireOn_" + i, 0);
            }
        }
        for (int i = 0; i < 12; i++)
        {
            PlayerPrefs.GetInt("SpellPower" + i, SpellPower[i]);
        }

        for (int i = 0; i < 21; i++)
        {
            PlayerPrefs.GetInt("QuestValue" + i, QuestValue[i]);
            PlayerPrefs.GetInt("QuestClear" + i, QuestClear[i]);
        }
    }

    IEnumerator SaveTimer()
    {
        yield return new WaitForSeconds(60f);

        SaveTime();
        Save();
        StartCoroutine(SaveTimer());
    }

    public void Cheat()
    {
        NowMakeSpell = MaxMakeSpell;
    }
    public void SaveTime()
    {
        MagicForce = MagicForce + 10;
        MagicGem = MagicGem + 1;
        if (NowMakeSpell < MaxMakeSpell)
        {
            NowMakeSpell = NowMakeSpell + 1;
        }
    }

    
    public void ForcePlus(int force)
    {
        MagicForce += force;
        QuestValue[2] += force;
    }
    public void ForceMinus(int force)
    {
        MagicForce -= force;
        QuestValue[6] += force;
    }

    public void GemPlus(int gem)
    {
        MagicGem += gem;
        QuestValue[3] += gem;
    }
    public void GemMinus(int gem)
    {
        MagicGem -= gem;
        QuestValue[7] += gem;
    }

}
