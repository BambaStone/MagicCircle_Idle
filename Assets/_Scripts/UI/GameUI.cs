using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameUI : MonoBehaviour
{
    public TMP_Text MagicForceText;
    public TMP_Text MagicGemText;
    public TMP_Text ForcePlusText;
    public TMP_Text GemPlusText;
    public GameObject UpgradeUI;
    public GameObject QuestUI;
    public GameObject BossUI;
    public GameObject MenuUI;
    public TMP_Text SpellHaveText;

    public int UINow = 0;

    private int _force;
    private int _gem;

    private int _forcePlusCount;
    private int _gemPlusCount;

    private int _nowHaveSpell;
    private int _maxHaveSpell;
    // Start is called before the first frame update
    void Start()
    {
        _force = SaveDataManager.Instance.MagicForce;
        MagicForceText.text = _force+"";
        _forcePlusCount = SaveDataManager.Instance.ForcePlusList.Count;
        _gem = SaveDataManager.Instance.MagicGem;
        MagicGemText.text = _gem + "";
        _gemPlusCount = SaveDataManager.Instance.GemPlusList.Count;
        _nowHaveSpell = SaveDataManager.Instance.SpellsCount;
        _maxHaveSpell = SaveDataManager.Instance.MaxHaveSpell;
        SpellHaveText.text = _nowHaveSpell + " / " + _maxHaveSpell;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("A´©¸§");
            SaveDataManager.Instance.Cheat();
            SaveDataManager.Instance.Save();
        }
    }

    private void FixedUpdate()
    {
        if(_force != SaveDataManager.Instance.MagicForce)
        {
            _force = SaveDataManager.Instance.MagicForce;
            MagicForceText.text = _force + "";
            
        }
        if(_forcePlusCount != SaveDataManager.Instance.ForcePlusList.Count)
        {
            _forcePlusCount = SaveDataManager.Instance.ForcePlusList.Count;
            ForcePlusText.text = "";
            for (int i = 0; i < _forcePlusCount; i++)
            {
                ForcePlusText.text += "+" + SaveDataManager.Instance.ForcePlusList[i] + " ";
            }
        }

        if (_gem != SaveDataManager.Instance.MagicGem)
        {
            _gem = SaveDataManager.Instance.MagicGem;
            MagicGemText.text = _gem + "";
            
        }
        if(_gemPlusCount != SaveDataManager.Instance.GemPlusList.Count)
        {
            _gemPlusCount = SaveDataManager.Instance.GemPlusList.Count;
            GemPlusText.text = "";
            for (int i = 0; i < _gemPlusCount; i++)
            {
                GemPlusText.text += "+" + SaveDataManager.Instance.GemPlusList[i] + " ";
            }
        }

        if(_nowHaveSpell !=SaveDataManager.Instance.SpellsCount)
        {
            _nowHaveSpell = SaveDataManager.Instance.SpellsCount;
            SpellHaveText.text = _nowHaveSpell + " / " + _maxHaveSpell;
        }
        if(_maxHaveSpell != SaveDataManager.Instance.MaxHaveSpell)
        {
            _maxHaveSpell = SaveDataManager.Instance.MaxHaveSpell;
            SpellHaveText.text = _nowHaveSpell + " / " + _maxHaveSpell;
        }
    }



    public void UpgradeButton()
    {
        switch(UINow)
        {
            case 0:
                UpgradeUI.SetActive(true);
                UINow = 1;
                break;
            case 1:
                UpgradeUI.SetActive(false);
                UINow = 0;
                break;
            case 2:
                QuestUI.SetActive(false);
                UpgradeUI.SetActive(true);
                UINow = 1;
                break;
            case 3:
                BossUI.SetActive(false);
                UpgradeUI.SetActive(true);
                UINow = 1;
                break;
        }
    }

    public void QuestButton()
    {
        switch (UINow)
        {
            case 0:
                QuestUI.SetActive(true);
                UINow = 2;
                break;
            case 1:
                UpgradeUI.SetActive(false);
                QuestUI.SetActive(true);
                UINow = 2;
                break;
            case 2:
                QuestUI.SetActive(false);
                UINow = 0;
                break;
            case 3:
                BossUI.SetActive(false);
                QuestUI.SetActive(true);
                UINow = 2;
                break;
        }
    }

    public void BossButton()
    {
        switch (UINow)
        {
            case 0:
                BossUI.SetActive(true);
                UINow = 3;
                break;
            case 1:
                UpgradeUI.SetActive(false);
                BossUI.SetActive(true);
                UINow = 3;
                break;
            case 2:
                QuestUI.SetActive(false);
                BossUI.SetActive(true);
                UINow = 3;
                break;
            case 3:
                BossUI.SetActive(false);
                UINow = 0;
                break;
        }
    }

    public void MenuButton()
    {
        switch (UINow)
        {
            case 0:
                MenuUI.SetActive(true);
                UINow = 4;
                break;
            case 1:
                UpgradeUI.SetActive(false);
                MenuUI.SetActive(true);
                UINow = 4;
                break;
            case 2:
                QuestUI.SetActive(false);
                MenuUI.SetActive(true);
                UINow = 4;
                break;
            case 3:
                BossUI.SetActive(false);
                MenuUI.SetActive(true);
                UINow = 4;
                break;
        }
    }
}
