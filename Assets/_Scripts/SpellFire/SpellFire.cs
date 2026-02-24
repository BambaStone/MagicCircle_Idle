using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellFire : MonoBehaviour //장착된 스펠을 발동 및 발사하는 클래스
{
    public GameObject FireSpellParent; //발동할 스펠을 옮겨놓을 부모객체
    public GameObject SpellTankParent; //발동하지 않는 스펠이 보관되어 있는 부모객체
    public List<GameObject> FireSpellList; //발동할 스펠 목록
    public List<GameObject> NowSpellLine;  //스펠 발동 표시
    public List<GameObject> SpellLineLocks;//잠금된 발동칸
    public List<GameObject> SpellEffects; //스펠 발동시 발사될 투사체
    public GameObject target;//스펠의 대상이 될 타겟

    public GameObject SpellFireSound;

    public float SpellTimer=0f;
    public int NowSpellNum = -1;
    public bool Fire=false;
    public int SpellFireLine=1;

    bool stop = false;

    static int MaxSpellFireLine=8;//최대 발동가능 스펠수

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FireTimer());
    }

    void SpellReposition()//스펠이 새로 추가되거나 빠졌을때 스펠의 위치를 재정렬
    {
        for (int i = 0; i < FireSpellList.Count; i++)
        {
            FireSpellList[i].transform.localPosition = new Vector3(i * 0.55f + 0.1f, 0, -1);
            if (FireSpellList[i].GetComponent<Rigidbody2D>().bodyType != RigidbodyType2D.Static)
                FireSpellList[i].GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }
    }
    public void OnSpell(GameObject spell)//스펠을 발동 목록에 추가
    {
        bool same = false;
        for (int i = 0; i < FireSpellList.Count; i++)
        {
            if (FireSpellList[i] == spell)
            {
                same = true;
            }
        }
        if (!same)
        {
            FireSpellList.Add(spell);
            spell.transform.parent = FireSpellParent.transform;
        }
        SpellReposition();

    }
   
    public void OffSpell(GameObject spell)//스펠을 발동 목록에서 제거
    {
        for(int i=0;i<FireSpellList.Count;i++)
        {
            if(FireSpellList[i]==spell)
            {
                FireSpellList.RemoveAt(i);
                spell.transform.parent = SpellTankParent.transform;
                spell.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            }
        }
        SpellReposition();

    }

    private void FixedUpdate()
    {
        if(SaveDataManager.Instance.MaxSpellFiresCount != SpellFireLine)
        {
            SpellFireLine = SaveDataManager.Instance.MaxSpellFiresCount;
            for(int i=0;i<SpellFireLine-1;i++)
            {
                if (SpellLineLocks[i].activeSelf)
                    SpellLineLocks[i].SetActive(false);
            }
        }
        if(Fire)
        {
            FireSpell();
        }
    }

    public void FireSpell()//목록에 있는 스펠을 발동
    {
        int SpellTimerInt = (int)SpellTimer/1;
        if(SpellTimerInt != NowSpellNum && SpellTimerInt < 8)
        {
            SpellFireSound.SetActive(false);
            NowSpellNum = SpellTimerInt;
            if(NowSpellNum<FireSpellList.Count)
            {
                SpellFireSound.SetActive(true);
                int magicLevel = FireSpellList[NowSpellNum].GetComponent<MagicCircle>().MagicLevel;
                Vector3 spellPosition = FireSpellList[NowSpellNum].transform.position;
                GameObject fireSpell =
                    Instantiate(SpellEffects[magicLevel], spellPosition , Quaternion.identity);
                float damage = 
                    SaveDataManager.Instance.BaseDamage[magicLevel] 
                    * SaveDataManager.Instance.TotalPower
                    * (1+SaveDataManager.Instance.SpellPower[magicLevel]*0.1f);
                fireSpell.GetComponent<SpellData>().SetData(target,damage);
            }
            if(1<=NowSpellNum)
            {
                NowSpellLine[NowSpellNum - 1].SetActive(false);
            }
            NowSpellLine[NowSpellNum].SetActive(true);
        }
        if (MaxSpellFireLine <= SpellTimer)
        {
            NowSpellLine[MaxSpellFireLine-1].SetActive(false);
            SpellTimer = 0;
            NowSpellNum = -1;
            Fire = false;
            StartCoroutine(FireTimer());
        }
        else
            SpellTimer += SaveDataManager.Instance.TotalSpeed * Time.deltaTime;
    }


    IEnumerator FireTimer() //1초후 다음 스펠발동 시작
    {
        yield return new WaitForSeconds(1f);
        if(!stop)
            Fire = true;
    }

    public void Stop()
    {
        stop = true;
    }
}
