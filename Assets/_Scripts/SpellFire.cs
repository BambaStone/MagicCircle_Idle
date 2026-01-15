using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellFire : MonoBehaviour
{
    public GameObject FireSpellParent;
    public GameObject SpellTankParent;
    public List<GameObject> FireSpellList;
    // Start is called before the first frame update
    void Start()
    {
        
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
            FireSpellList[i].transform.localPosition = new Vector2(i * 0.6f + 0.1f, 0);
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
            FireSpellList[i].transform.localPosition = new Vector2(i * 0.6f + 0.1f, 0);
        }

    }

    public void FireSpell()
    {

    }
}
