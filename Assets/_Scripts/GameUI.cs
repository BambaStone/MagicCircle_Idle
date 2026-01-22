using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameUI : MonoBehaviour
{
    public TMP_Text MagicForceText;
    public TMP_Text MagicGemText;
    public GameObject UpgradeUI;
    public GameObject QuestUI;

    private int _uiNow = 0;

    private int _force;
    private int _gem;
    // Start is called before the first frame update
    void Start()
    {
        _force = SaveDataManager.Instance.MagicForce;
        MagicForceText.text = _force+"";
        _gem = SaveDataManager.Instance.MagicGem;
        MagicGemText.text = _gem + "";
    }

    private void FixedUpdate()
    {
        if(_force != SaveDataManager.Instance.MagicForce)
        {
            _force = SaveDataManager.Instance.MagicForce;
            MagicForceText.text = _force + "";
        }

        if (_gem != SaveDataManager.Instance.MagicGem)
        {
            _gem = SaveDataManager.Instance.MagicGem;
            MagicGemText.text = _gem + "";
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
