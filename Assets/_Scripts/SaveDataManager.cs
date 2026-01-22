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

    public int MagicForce;
    public int MagicGem;

    public int TotalPower;

    public int Power;
    public int Speed;
    public int MaxSpellFiresCount;
    public int MaxMakeSpell;
    public int NowMakeSpell;
    public int MakeSpellLevel;
    public int MaxHaveSpell;
    public DateTime LastPlayTime;
    public DateTime NowLoginTime;
    

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
        MaxMakeSpell = PlayerPrefs.GetInt("MaxMakeSpell", 5);
        NowMakeSpell = PlayerPrefs.GetInt("NowMakeSpell", MaxMakeSpell);
        MaxHaveSpell = PlayerPrefs.GetInt("MaxHaveSpell", 50);
        MakeSpellLevel = PlayerPrefs.GetInt("MakeSpellLevel", 1);

        MagicForce= PlayerPrefs.GetInt("MagicForce", 0);
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
        PlayerPrefs.SetString("LastPlayTime", DateTime.Now.ToString());

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

    }

    IEnumerator SaveTimer()
    {
        yield return new WaitForSeconds(60f);

        MagicForce= MagicForce+10;
        MagicGem = MagicGem + 1;
        if (NowMakeSpell<MaxMakeSpell)
        {
            NowMakeSpell = NowMakeSpell + 1;
        }
        Save();
        StartCoroutine(SaveTimer());
    }


}
