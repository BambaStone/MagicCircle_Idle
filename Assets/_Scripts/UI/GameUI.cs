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
    public TMP_Text SpellHaveText;

    private int _uiNow = 0;

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
        switch(_uiNow)
        {
            case 0:
                UpgradeUI.SetActive(true);
                _uiNow = 1;
                break;
            case 1:
                UpgradeUI.SetActive(false);
                _uiNow = 0;
                break;
            case 2:
                QuestUI.SetActive(false);
                UpgradeUI.SetActive(true);
                _uiNow = 1;
                break;
        }
    }

    public void QuestButton()
    {
        switch (_uiNow)
        {
            case 0:
                QuestUI.SetActive(true);
                _uiNow = 2;
                break;
            case 1:
                UpgradeUI.SetActive(false);
                QuestUI.SetActive(true);
                _uiNow = 2;
                break;
            case 2:
                QuestUI.SetActive(false);
                _uiNow = 0;
                break;
        }
    }
}
