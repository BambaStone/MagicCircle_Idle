using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell_5 : MonoBehaviour
{
    public SpellData Spell_1;
    SpellData _spellData;

    private void Start()
    {
        transform.position = Vector2.zero;
        _spellData = GetComponent<SpellData>();
    }
    void Update()
    {
        if (_spellData.Target != null)
        {
            Spell_1.SetData(_spellData);
        }
        if (Spell_1 == null)
        {
            Destroy(gameObject);
        }
    }
}
