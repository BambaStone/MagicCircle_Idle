using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MenuUI : MonoBehaviour
{
    public TMP_Text StageClearText;
    public TMP_Text BossClearText;
    public TMP_Text UnlockSpellText;
    public TMP_Text AFKIncomeForceText;
    public TMP_Text AFKIncomeGemText;
    public Slider VolumeSlider;
    public TMP_InputField CodeInput;

    private int _stageClear;
    private int _bossClear;
    private int _unLockSpell;
    private int _forceAFK;
    private int _gemAFK;

    public GameUI Game_UI;

    void SetInfo()
    {
        _stageClear= SaveDataManager.Instance.StageClear;
        _bossClear= SaveDataManager.Instance.BossClear;
        _unLockSpell= SaveDataManager.Instance.UnlockSpell;
        _forceAFK= SaveDataManager.Instance.AFKIncomeForce;
        _gemAFK= SaveDataManager.Instance.AFKIncomeGem;
        StageClearText.text = "StageClear : " + _stageClear;
        BossClearText.text = "BossClear : " + _bossClear;
        UnlockSpellText.text = "UnlockSpell : " + _unLockSpell;
        AFKIncomeForceText.text = _forceAFK + "/min";
        AFKIncomeGemText.text = _gemAFK + "/min";
    }


    private void OnEnable()
    {
        SetInfo();
        VolumeSlider.value = SaveDataManager.Instance.Volume;
    }


    private void FixedUpdate()
    {
        if(_stageClear != SaveDataManager.Instance.StageClear)
        {
            _stageClear = SaveDataManager.Instance.StageClear;
            StageClearText.text = "StageClear : " + _stageClear;
        }
        if(_bossClear != SaveDataManager.Instance.BossClear)
        {
            _bossClear = SaveDataManager.Instance.BossClear;
            BossClearText.text = "BossClear : " + _bossClear;
        }
        if(_unLockSpell != SaveDataManager.Instance.UnlockSpell)
        {
            _unLockSpell = SaveDataManager.Instance.UnlockSpell;
            UnlockSpellText.text = "UnlockSpell : " + _unLockSpell;
        }
        if(_forceAFK != SaveDataManager.Instance.AFKIncomeForce)
        {
            _forceAFK = SaveDataManager.Instance.AFKIncomeForce;
            _gemAFK = SaveDataManager.Instance.AFKIncomeGem;
            AFKIncomeForceText.text = _forceAFK + "/min";
            AFKIncomeGemText.text = _gemAFK + "/min";
        }
    }

    public void SetVolumeSlider(float Handle)
    {
        SaveDataManager.Instance.Volume = Handle;
    }

    public void CodeButton()
    {
        if(CodeInput.text == "Cheat")
        {
            SaveDataManager.Instance.Cheat();
            CodeInput.text = "CheatUse!";
        }
        else
        {
            CodeInput.text = "nothing";
        }
        
    }

    public void GameCloseButton()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
    public void BackButton()
    {
        SaveDataManager.Instance.Save();
        Game_UI.UINow = 0;
        gameObject.SetActive(false);
    }
}
