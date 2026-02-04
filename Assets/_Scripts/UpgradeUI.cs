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
    public TMP_Text ToTalSpeedText;
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

    public TMP_Text SpellTankText;
    public TMP_Text SpellTank_MaxHaveSpellText;
    public TMP_Text SpellTank_NeedGemText;

    public TMP_Text SpellMakeText;
    public TMP_Text SpellMake_SpellMakeLevelText;
    public TMP_Text SpellMake_NeedGemText;

    public List<GameObject> SpellPower;
    public List<TMP_Text> SpellPower_Power;
    public List<TMP_Text> SpellPower_SpellDamage;
    public List<TMP_Text> SpellPower_Plus;
    public List<TMP_Text> SpellPower_NeedForce;

    private void OnEnable()
    {
        StageClearText.text = SaveDataManager.Instance.StageClear + "";
        BossClearText.text = SaveDataManager.Instance.BossClear + "";
        UnlockSpellText.text = SaveDataManager.Instance.UnlockSpell + "";
        AFKIncomeText.text = SaveDataManager.Instance.AFKIncome + "";
        TotalPowerText.text = SaveDataManager.Instance.TotalPower + "";
        ToTalSpeedText.text = SaveDataManager.Instance.TotalSpeed + "";
        MaxSpellFireText.text = SaveDataManager.Instance.MaxSpellFiresCount + "";
        HighLevelSpellMake.text = SaveDataManager.Instance.MakeSpellLevel + "%";
    }

    public void PowerUP()
    {
        int needGem = (SaveDataManager.Instance.Power + 1);
        for(int i=0;i< SaveDataManager.Instance.Power;i++)
        {
            needGem = needGem + needGem;
        }
        if (needGem<=SaveDataManager.Instance.MagicGem)
        {
            SaveDataManager.Instance.MagicGem = SaveDataManager.Instance.MagicGem - needGem;
        }
    }
}
