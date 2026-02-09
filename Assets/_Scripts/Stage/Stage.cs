using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Stage : MonoBehaviour
{
    public float TimeOut;
    public bool Move;
    public Stage_Mob Mob;
    public TMP_Text StageText;
    public TMP_Text TimeOutText;
    public float MaxTimeOut = 300f;

    public void SpawnPos()
    {
        Move = true;
        transform.position=new Vector3(4,2.7f,0);
    }
    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(StartSet());
    }

    IEnumerator StartSet()
    {
        yield return new WaitForSeconds(0.1f);
        resetStage();
    }
    public void NextStage()
    {
        SaveDataManager.Instance.ForcePlus(100 * (SaveDataManager.Instance.StageClear + 1));
        SaveDataManager.Instance.StageClears();
        SaveDataManager.Instance.Save();
        resetStage();
    }

    private void FixedUpdate()
    {
        if (Move)
        {
            transform.Translate(new Vector3(-4, 0, 0) * Time.deltaTime);
            if (transform.position.x <= 0)
            {
                Move = false;
                transform.position = new Vector3(0, 2.7f, 0);
            }
        }

        TimeOut = TimeOut - Time.deltaTime;
        TimeOutText.text = "TimeOut " + (int)(TimeOut / 60) + ":" + (int)(TimeOut % 60);
        if(TimeOut<=0)
        {
            resetStage();
        }
    }

    void resetStage()
    {
        SpawnPos();
        TimeOut = MaxTimeOut;
        Mob.SetStageMob(SaveDataManager.Instance.StageClear);
        StageText.text = "Stage " + (SaveDataManager.Instance.StageClear + 1);
    }

    public void Hit(float Damage)
    {
        Mob.HitSpell(Damage);
    }
}
