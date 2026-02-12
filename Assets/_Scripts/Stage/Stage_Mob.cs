using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Stage_Mob : MonoBehaviour
{
    public SpriteRenderer SR;
    public List<Sprite> images;

    public Stage StageData;

    public TMP_Text HPText;
    public TMP_Text MonsterCount;

    public GameObject DieEffect;

    public int MobNum = 0;
    public float BaseScale = 0.3f;
    public float StageScale = 0.025f;

    public float MaxHP;
    public float NowHP;
    public float Scale;
    public float StageHP = 10;

    void HPSet()
    {
        string hp = float.Parse(NowHP.ToString("N2")) + "/" + MaxHP + " ";
        float ShowHP = NowHP/(MaxHP * 0.1f);
        
        for (int i = 0; i < ShowHP; i++)
        {
            hp = hp + "l";
        }
        
        HPText.text = hp;
    }

    public void SetStageMob(int stage)
    {
        MobNum = 0;
        SR.sprite = images[MobNum];

        MaxHP = (stage) * StageHP;
        if (MaxHP == 0)
            MaxHP = 1;
        NowHP = MaxHP;

        HPSet();

        Scale = BaseScale + stage * StageScale;
        if(1<Scale)
        {
            Scale = 1;
        }

        MonsterCount.text = "Monster : " + (images.Count - MobNum)+"/"+images.Count;

        transform.localScale = new Vector3(Scale,Scale,0.1f);
    }

    
    public void HitSpell(float Damage)
    {
        NowHP = NowHP - Damage;
        HPSet();
        if(NowHP<=0)
        {
            DieMob();
            MonsterCount.text = "Monster : "+ (images.Count - MobNum) + "/" + images.Count;
        }
    }

    public void DieMob()
    {
        SaveDataManager.Instance.QuestValue[0]++;//몬스터사냥퀘스트
        Destroy( Instantiate(DieEffect,transform.position, Quaternion.identity),1f);
        MobNum++;
        SR.sprite = images[MobNum];
        SaveDataManager.Instance.ForcePlus((SaveDataManager.Instance.StageClear + 1)*10);
        if(20<=MobNum)
        {
            StageData.NextStage();
        }
        else
        {
            MaxHP += (SaveDataManager.Instance.StageClear + 1);
            NowHP = MaxHP;
            HPSet();
            StageData.SpawnPos();
        }
    }

}
