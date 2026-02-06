using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeUI : MonoBehaviour
{
    public TMP_Text StageClearText;
    public TMP_Text BossClearText;
    public TMP_Text UnlockSpellText;
    public TMP_Text AFKIncomeText;
    public TMP_Text TotalPowerText;
    public TMP_Text TotalSpeedText;
    public TMP_Text MaxSpellFireText;
    public TMP_Text HighLevelSpellMake;

    public TMP_Text PowerText;
    public TMP_Text Power_DamageText;
    public TMP_Text Power_NeedGemText;

    public TMP_Text SpeedText;
    public TMP_Text Speed_SpellSpeedText;
    public TMP_Text Speed_NeedGemText;

    public TMP_Text SpellFireText;
    public TMP_Text SpellFire_MaxSpellFireText;
    public TMP_Text SpellFire_NeedGemText;
    public GameObject SpellFire_Button;
    public GameObject SpellFire_Lock;


    public TMP_Text SpellTankText;
    public TMP_Text SpellTank_MaxHaveSpellText;
    public TMP_Text SpellTank_NeedGemText;

    public TMP_Text SpellMakeText;
    public TMP_Text SpellMake_MaxSpellMakeText;
    public TMP_Text SpellMake_SpellMakeLevelText;
    public TMP_Text SpellMake_NeedGemText;
    public GameObject SpellMake_Button;
    public GameObject SpellMake_Lock;

    public List<GameObject> SpellPower;
    public List<TMP_Text> SpellPower_Power;
    public List<TMP_Text> SpellPower_SpellDamage;
    public List<TMP_Text> SpellPower_Plus;
    public List<TMP_Text> SpellPower_NeedForce;

    void SetInfo()
    {
        SaveDataManager.Instance.TotalPowerCal();
        SaveDataManager.Instance.TotalSpeedCal();
        StageClearText.text = "StageClear : "+SaveDataManager.Instance.StageClear;
        BossClearText.text = "BossClear : " + SaveDataManager.Instance.BossClear;
        UnlockSpellText.text = "UnlockSpell : " + SaveDataManager.Instance.UnlockSpell;
        AFKIncomeText.text = "AFK Income :   "+SaveDataManager.Instance.AFKIncome;
        TotalPowerText.text = "TotalPower : "+SaveDataManager.Instance.TotalPower;
        TotalSpeedText.text = "TotalSpeed : " + SaveDataManager.Instance.TotalSpeed;
        MaxSpellFireText.text = "MaxSpellFire : "+SaveDataManager.Instance.MaxSpellFiresCount;
        HighLevelSpellMake.text = "HighLevelSpellMake : " + SaveDataManager.Instance.MakeSpellLevel + "%";
    }

    void SetPowerUp()
    {
        PowerText.text = "Power  LV. " + SaveDataManager.Instance.Power;
        Power_DamageText.text = "Damage + " + SaveDataManager.Instance.Power * 10 + "%";
        Power_NeedGemText.text = "" + (SaveDataManager.Instance.Power + 1) * 2;
    }

    void SetSpeedUp()
    {
        SpeedText.text = "Speed  LV. " + SaveDataManager.Instance.Speed;
        Speed_SpellSpeedText.text = "SpellSpeed + " + SaveDataManager.Instance.Speed * 10 + "%";
        Speed_NeedGemText.text = "" + (SaveDataManager.Instance.Speed + 1) * 2;
    }

    
    void SetSpellFireUp()
    {
        if (8 <= SaveDataManager.Instance.MaxSpellFiresCount)
        {
            SpellFireText.text = "SpellFire  LV. MAX";
            
            if (SpellFire_Button.activeSelf)
                SpellFire_Button.SetActive(false);
            if (!SpellFire_Lock.activeSelf)
                SpellFire_Lock.SetActive(true);
        }
        else
        {
            SpellFireText.text = "SpellFire  LV. " + (SaveDataManager.Instance.MaxSpellFiresCount - 1);
            SpellFire_NeedGemText.text = "" + SaveDataManager.Instance.MaxSpellFiresCount * 2;
            if (!SpellFire_Button.activeSelf)
                SpellFire_Button.SetActive(true);
            if (SpellFire_Lock.activeSelf)
                SpellFire_Lock.SetActive(false);
        }

        SpellFire_MaxSpellFireText.text = "MaxSpellFire : " + SaveDataManager.Instance.MaxSpellFiresCount;
    }

    void SetSpellMakeUp()
    {
        if (20 <= SaveDataManager.Instance.MakeSpellLevel)
        {
            SpellMakeText.text = "SpellMake  LV. MAX";

            if (SpellMake_Button.activeSelf)
                SpellMake_Button.SetActive(false);
            if (!SpellMake_Lock.activeSelf)
                SpellMake_Lock.SetActive(true);
        }
        else
        {
            SpellMakeText.text = "SpellMake  LV. " + SaveDataManager.Instance.MakeSpellLevel;
            SpellMake_NeedGemText.text = "" + (SaveDataManager.Instance.MakeSpellLevel + 1) * (1 + SaveDataManager.Instance.MakeSpellLevel);
            if (!SpellMake_Button.activeSelf)
                SpellMake_Button.SetActive(true);
            if (SpellMake_Lock.activeSelf)
                SpellMake_Lock.SetActive(false);
        }
        SpellMake_MaxSpellMakeText.text = "MaxMakeSpell : " + SaveDataManager.Instance.MaxMakeSpell;
        SpellMake_SpellMakeLevelText.text = "High Level Spell Make " + SaveDataManager.Instance.MakeSpellLevel + "%";
    }

    void SetSpellMemoryUp()
    {
        SpellTankText.text = "SpellMemory  LV. " + (SaveDataManager.Instance.MaxHaveSpell / 10 - 1);
        SpellTank_MaxHaveSpellText.text = "MaxHaveSpell : " + SaveDataManager.Instance.MaxHaveSpell;
        SpellTank_NeedGemText.text = "" + SaveDataManager.Instance.MaxHaveSpell * (SaveDataManager.Instance.MaxHaveSpell / 10);
    }

    

    void SetSpellPowerUp(int i)
    {
        if (!SpellPower[i].activeSelf)
            SpellPower[i].SetActive(true);
        SpellPower_Power[i].text = "SpellPower  LV. " + SaveDataManager.Instance.SpellPower[i];
        float damage = SaveDataManager.Instance.BaseDamage[i] * (1 + SaveDataManager.Instance.SpellPower[i] * 0.1f);
        SpellPower_SpellDamage[i].text = "SpellDamage : " + damage;
        if (i == 0 || i % 2 == 0)
        {
            SpellPower_Plus[i].text = "Damage + " + SaveDataManager.Instance.SpellPower[i] + "%";
            
            
        }
        else
        {
            SpellPower_Plus[i].text = "SpellSpeed + " + SaveDataManager.Instance.SpellPower[i] + "%";
            
        }
        SpellPower_NeedForce[i].text = "" + (SaveDataManager.Instance.SpellPower[i] + 1) * (i + 1);

    }

    private void OnEnable()
    {
        SetInfo();

        SetPowerUp();

        SetSpeedUp();

        SetSpellFireUp();

        SetSpellMemoryUp();

        SetSpellMakeUp();

        for (int i = 0; i <= SaveDataManager.Instance.UnlockSpell; i++)
        {
            SetSpellPowerUp(i);
        }
    }

    
    IEnumerator FalseUpgradeText_ForRed(TMP_Text Need, float i)
    {
        yield return new WaitForSeconds(0.05f);
        Need.color = new Color(1, 1- i * 0.1f, 1- i * 0.1f, 1);
        if (i < 10)
        {
            StartCoroutine(FalseUpgradeText_ForRed(Need, i + 1));
        }
        else
        {
            Need.color = Color.red;
            StartCoroutine(FalseUpgradeText_ForWhite(Need, 0));
        }
    }
    IEnumerator FalseUpgradeText_ForWhite(TMP_Text Need, float i)
    {
        yield return new WaitForSeconds(0.05f);
        Need.color = new Color(1, 0 + i * 0.1f, 0 + i * 0.1f, 1);
        if (i < 10)
        {
            StartCoroutine(FalseUpgradeText_ForWhite(Need, i + 1));
        }
        else
        {
            Need.color = Color.white;
        }
    }

    public void PowerUP()
    {
        int needGem = (SaveDataManager.Instance.Power + 1) * 2;

        if (needGem <= SaveDataManager.Instance.MagicGem)
        {
            SaveDataManager.Instance.GemMinus(needGem);
            SaveDataManager.Instance.Power++;
            SaveDataManager.Instance.TotalPowerCal();
            SetPowerUp();
            SetInfo();
        }
        else
        {
            StartCoroutine(FalseUpgradeText_ForRed(Power_NeedGemText, 0));
        }

    }

    public void SpeedUP()
    {
        int needGem = (SaveDataManager.Instance.Speed + 1) * 2;

        if (needGem <= SaveDataManager.Instance.MagicGem)
        {
            SaveDataManager.Instance.GemMinus(needGem);
            SaveDataManager.Instance.Speed++;
            SaveDataManager.Instance.TotalSpeedCal();
            SetSpeedUp();
            SetInfo();
        }
        else
        {
            StartCoroutine(FalseUpgradeText_ForRed(Speed_NeedGemText, 0));
        }
    }

    public void SpellFireUP()
    {
        int needGem = SaveDataManager.Instance.MaxSpellFiresCount * 2;

        if (needGem <= SaveDataManager.Instance.MagicGem)
        {
            SaveDataManager.Instance.GemMinus(needGem);
            SaveDataManager.Instance.MaxSpellFiresCount++;
            SetSpellFireUp();
            SetInfo();
        }
        else
        {
            StartCoroutine(FalseUpgradeText_ForRed(SpellFire_NeedGemText, 0));
        }
    }

    public void SpellMakeUP()
    {
        int needGem = (SaveDataManager.Instance.MakeSpellLevel + 1) * (1 + SaveDataManager.Instance.MakeSpellLevel); ;

        if (needGem <= SaveDataManager.Instance.MagicGem)
        {
            SaveDataManager.Instance.GemMinus(needGem);
            SaveDataManager.Instance.MaxMakeSpell++;
            SaveDataManager.Instance.MakeSpellLevel++;
            SetSpellMakeUp();
            SetInfo();
        }
        else
        {
            StartCoroutine(FalseUpgradeText_ForRed(SpellMake_NeedGemText, 0));
        }
    }


    public void SpellMemoryUP()
    {
        int needGem = SaveDataManager.Instance.MaxHaveSpell * (SaveDataManager.Instance.MaxHaveSpell / 10);

        if (needGem <= SaveDataManager.Instance.MagicGem)
        {
            SaveDataManager.Instance.GemMinus(needGem);
            SaveDataManager.Instance.MaxHaveSpell= SaveDataManager.Instance.MaxHaveSpell+10;
            SetSpellMemoryUp();
            SetInfo();
        }
        else
        {
            StartCoroutine(FalseUpgradeText_ForRed(SpellTank_NeedGemText, 0));
        }
    }

    public void SpellPowerUP(int i)
    {
        int needForce= (SaveDataManager.Instance.SpellPower[i] + 1) * (i + 1);
        if (needForce <= SaveDataManager.Instance.MagicForce)
        {
            SaveDataManager.Instance.ForceMinus(needForce);
            SaveDataManager.Instance.SpellPower[i]++;
            SetSpellPowerUp(i);
            SetInfo();
        }
        else
        {
            StartCoroutine(FalseUpgradeText_ForRed(SpellPower_NeedForce[i], 0));
        }
        
    }
}
