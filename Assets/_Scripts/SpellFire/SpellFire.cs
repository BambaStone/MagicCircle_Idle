using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellFire : MonoBehaviour
{
    public GameObject FireSpellParent;
    public GameObject SpellTankParent;
    public List<GameObject> FireSpellList;
    public List<GameObject> NowSpellLine;
    public List<GameObject> SpellLineLocks;
    public List<GameObject> SpellEffects;
    public GameObject target;
    public GameObject SpellFireSound;

    public float SpellTimer=0f;
    public int NowSpellNum = -1;
    public bool Fire=false;
    public int SpellFireLine=1;

    bool stop = false;

    static int MaxSpellFireLine=8;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FireTimer());
    }

    void SpellReposition()
    {
        for (int i = 0; i < FireSpellList.Count; i++)
        {
            FireSpellList[i].transform.localPosition = new Vector3(i * 0.55f + 0.1f, 0, -1);
            if (FireSpellList[i].GetComponent<Rigidbody2D>().bodyType != RigidbodyType2D.Static)
                FireSpellList[i].GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }
    }
    public void OnSpell(GameObject spell)
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
   
    public void OffSpell(GameObject spell)
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

    public void FireSpell()
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
                GameObject fireSpell=Instantiate(SpellEffects[magicLevel], FireSpellList[NowSpellNum].transform.position, Quaternion.identity);

                float damage = SaveDataManager.Instance.BaseDamage[magicLevel] * SaveDataManager.Instance.TotalPower*(1+SaveDataManager.Instance.SpellPower[magicLevel]*0.1f);
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
            NowSpellLine[7].SetActive(false);
            SpellTimer = 0;
            NowSpellNum = -1;
            Fire = false;
            StartCoroutine(FireTimer());
        }
        SpellTimer = SpellTimer + SaveDataManager.Instance.TotalSpeed * Time.deltaTime;
    }


    IEnumerator FireTimer()
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
