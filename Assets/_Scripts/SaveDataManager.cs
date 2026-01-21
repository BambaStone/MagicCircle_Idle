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
    public int SpellOpen;
    
    public int Power;
    public int Speed;
    public int MaxSpellFiresCount;
    public int MaxMakeSpell;
    public int NowMakeSpell;
    public int MakeSpellLevel;
    public int MaxHaveSpell;
    public DateTime LastPlayTime;
    

    // Start is called before the first frame update
    void Start()
    {
        Load();
        StartCoroutine(SaveTimer());
    }

    public void Load()
    {
        StageClear = PlayerPrefs.GetInt("StageClear", 0);
        BossClear = PlayerPrefs.GetInt("BossClear", 0);
        SpellOpen = PlayerPrefs.GetInt("SpellOpen", 0);
        Power = PlayerPrefs.GetInt("Power", 0);
        Speed = PlayerPrefs.GetInt("Speed", 0);
        MaxSpellFiresCount = PlayerPrefs.GetInt("MaxSpellFiresCount", 1);
        MaxMakeSpell = PlayerPrefs.GetInt("MaxMakeSpell", 1);
        NowMakeSpell = PlayerPrefs.GetInt("NowMakeSpell", 1);
        MaxHaveSpell = PlayerPrefs.GetInt("MaxHaveSpell", 50);
        MakeSpellLevel = PlayerPrefs.GetInt("MakeSpellLevel", 1);
        LastPlayTime.AddYears(PlayerPrefs.GetInt("LastYear", DateTime.Now.Year));
        LastPlayTime.AddMonths(PlayerPrefs.GetInt("LastMonths", DateTime.Now.Month));
        LastPlayTime.AddDays(PlayerPrefs.GetInt("Lastday", DateTime.Now.Day));
        LastPlayTime.AddHours(PlayerPrefs.GetInt("LastHour", DateTime.Now.Hour));
        LastPlayTime.AddMinutes(PlayerPrefs.GetInt("LastMinute", DateTime.Now.Minute));
        LastPlayTime.AddSeconds(PlayerPrefs.GetInt("LastSecond", DateTime.Now.Second));

        SpellsCount = PlayerPrefs.GetInt("SpellsCount", 0);
        for (int i = 0; i < SpellsCount; i++)
        {
            SpellLevel[i] = PlayerPrefs.GetInt("SpellLevel_" + i, 0);
            if (PlayerPrefs.GetInt("SpellFireOn_" + i, 0) == 1)
            {
                SpellFireOn[i] = true;
            }
            else
            {
                SpellFireOn[i] = false;
            }
        }
    }
    public void Save()
    {

        PlayerPrefs.SetInt("StageClear", StageClear);
        PlayerPrefs.SetInt("BossClear", BossClear);
        PlayerPrefs.SetInt("SpellOpen", SpellOpen);
        PlayerPrefs.SetInt("Power", Power);
        PlayerPrefs.SetInt("Speed", Speed);
        PlayerPrefs.SetInt("MaxSpellFiresCount", MaxSpellFiresCount);
        PlayerPrefs.SetInt("MaxMakeSpell", MaxMakeSpell);
        PlayerPrefs.SetInt("NowMakeSpell", NowMakeSpell);
        PlayerPrefs.SetInt("MaxHaveSpell", MaxHaveSpell);
        PlayerPrefs.SetInt("MakeSpellLevel", MakeSpellLevel);
        PlayerPrefs.SetInt("LastYear", DateTime.Now.Year);
        PlayerPrefs.SetInt("LastMonth", DateTime.Now.Month);
        PlayerPrefs.SetInt("LastDay", DateTime.Now.Day);
        PlayerPrefs.SetInt("LastHour", DateTime.Now.Hour);
        PlayerPrefs.SetInt("LastMinute", DateTime.Now.Minute);
        PlayerPrefs.SetInt("LastSecond", DateTime.Now.Second);

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

    }
    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator SaveTimer()
    {
        yield return new WaitForSeconds(30f);
        Save();
        StartCoroutine(SaveTimer());
    }


}
