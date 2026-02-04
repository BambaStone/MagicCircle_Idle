using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellData : MonoBehaviour
{
    public GameObject Target;
    public float Damage;
    
    public void SetData(GameObject target,float damage)
    {
        Target = target;
        Damage = damage;
    }
    public void SetData(SpellData data)
    {
        Target = data.Target;
        Damage = data.Damage;
    }
}
