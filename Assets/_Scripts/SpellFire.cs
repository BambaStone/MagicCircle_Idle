using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellFire : MonoBehaviour
{
    public GameObject FireSpellParent;
    public GameObject SpellTankParent;
    public List<GameObject> FireSpellList;
    public List<GameObject> NowSpellLine;

    public float SpellTimer=0f;
    public int NowSpellNum = -1;
    public bool Fire=false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FireTimer());
    }

    // Update is called once per frame
    void Update()
    {
        
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
        for (int i = 0; i < FireSpellList.Count; i++)
        {
            FireSpellList[i].transform.localPosition = new Vector3(i * 0.55f + 0.1f, 0,-1);
            FireSpellList[i].GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }

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

        for (int i = 0; i < FireSpellList.Count; i++)
        {
            FireSpellList[i].transform.localPosition = new Vector3(i * 0.55f + 0.1f, 0,-1);
        }

    }

    private void FixedUpdate()
    {
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
            NowSpellNum = SpellTimerInt;

            if(1<=NowSpellNum)
            {
                NowSpellLine[NowSpellNum - 1].SetActive(false);
            }
            NowSpellLine[NowSpellNum].SetActive(true);
        }

        
        if (8<=SpellTimer)
        {
            NowSpellLine[7].SetActive(false);
            SpellTimer = 0;
            NowSpellNum = -1;
            Fire = false;
            StartCoroutine(FireTimer());
        }
        SpellTimer = SpellTimer + (SaveDataManager.Instance.Speed + 1) * Time.deltaTime;
    }


    IEnumerator FireTimer()
    {
        yield return new WaitForSeconds(1f);
        Fire = true;
    }
}
