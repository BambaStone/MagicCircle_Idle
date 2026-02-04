using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell_Double : MonoBehaviour
{
    public SpellData Spell_1_1;
    public SpellData Spell_1_2;
    SpellData _spellData;

    private void Start()
    {
        _spellData = GetComponent<SpellData>();
    }
    void Update()
    {
        if(_spellData.Target != null)
        {
            Spell_1_1.SetData(_spellData);
            Spell_1_2.SetData(_spellData);
        }
        if(Spell_1_1 ==null && Spell_1_2==null)
        {
            Destroy(gameObject);
        }
    }

}
