using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestUI : MonoBehaviour
{
    public List<TMP_Text> QuestText;
    public List<GameObject> QuestLock;
    public List<GameObject> QuestButton;
    public List<TMP_Text> QuestRewordText;
    // Start is called before the first frame update

    public void SetQuest(int i)
    {
        switch(i)
        {
            case 0:
                QuestText[i].text = "몬스터 사냥 " + ((SaveDataManager.Instance.QuestClear[i] + 1) * SaveDataManager.Instance.QuestUnit[i]) + "회";
                break;
            case 1:
                QuestText[i].text = "보스 사냥 " + ((SaveDataManager.Instance.QuestClear[i] + 1) * SaveDataManager.Instance.QuestUnit[i]) + "회";
                break;
            case 2:
                QuestText[i].text = " " + ((SaveDataManager.Instance.QuestClear[i] + 1) * SaveDataManager.Instance.QuestUnit[i]) + "개 획득";
                break;
            case 3:
                QuestText[i].text = " " + ((SaveDataManager.Instance.QuestClear[i] + 1) * SaveDataManager.Instance.QuestUnit[i]) + "개 획득";
                break;
            case 4:
                QuestText[i].text = " LV." + ((SaveDataManager.Instance.QuestClear[i] + 1) * SaveDataManager.Instance.QuestUnit[i]) + " 달성";
                break;
            case 5:
                QuestText[i].text = " LV." + ((SaveDataManager.Instance.QuestClear[i] + 1) * SaveDataManager.Instance.QuestUnit[i]) + " 달성";
                break;
            case 6:
                QuestText[i].text = " " + ((SaveDataManager.Instance.QuestClear[i] + 1) * SaveDataManager.Instance.QuestUnit[i]) + "개 사용";
                break;
            case 7:
                QuestText[i].text = " " + ((SaveDataManager.Instance.QuestClear[i] + 1) * SaveDataManager.Instance.QuestUnit[i]) + "개 사용";
                break;
            case 8:
                QuestText[i].text = "스테이지 " + ((SaveDataManager.Instance.QuestClear[i] + 1) * SaveDataManager.Instance.QuestUnit[i]) + "클리어";
                break;
            case 9:
                QuestText[i].text = "보스 " + ((SaveDataManager.Instance.QuestClear[i] + 1) * SaveDataManager.Instance.QuestUnit[i]) + "클리어";
                break;
            case 10:
                QuestText[i].text = " 스펠 제작 " + ((SaveDataManager.Instance.QuestClear[i] + 1) * SaveDataManager.Instance.QuestUnit[i]) + "회";
                break;
            case 11:
                QuestText[i].text = "스펠 합성 " + ((SaveDataManager.Instance.QuestClear[i] + 1) * SaveDataManager.Instance.QuestUnit[i]) + "회";
                break;
        }
        QuestText[i].text = QuestText[i].text + "  :  " + SaveDataManager.Instance.QuestValue[i];
        if(SaveDataManager.Instance.QuestClearCheck(i))
        {
            QuestButton[i].SetActive(true);
            QuestRewordText[i].text = SaveDataManager.Instance.QuestReword[i]+"개";
            QuestLock[i].SetActive(false);
        }
        else
        {
            QuestButton[i].SetActive(false);
            QuestLock[i].SetActive(true);
        }
    }

    private void OnEnable()
    {
        for(int i=0;i<SaveDataManager.Instance.QuestClear.Count;i++)
        {
            SetQuest(i);
        }
    }

    public void ClearButton(int i)
    {
        SaveDataManager.Instance.QuestClear[i]++;
        SaveDataManager.Instance.GemPlus(SaveDataManager.Instance.QuestReword[i]);
        SetQuest(i);
        SetQuest(3);//Gem획득 퀘스트
    }

    private void FixedUpdate()
    {
        SetQuest(0);//몬스터사냥퀘스트
        SetQuest(2);//포스획득퀘스트
        SetQuest(8);//스테이지클리어퀘스트
    }
}
