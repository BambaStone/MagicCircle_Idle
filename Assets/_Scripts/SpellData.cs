using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellData : MonoBehaviour
{
    public GameObject Target;
    public float Damage;
    public float UpgradeLevel;
    public float CircleLevel;
    
    public void SetData(GameObject target,float damage,float upgrade,float circle)
    {
        Target = target;
        Damage = damage;
        UpgradeLevel = upgrade;
        CircleLevel = circle;
    }
    public void SetData(SpellData data)
    {
        Target = data.Target;
        Damage = data.Damage;
        UpgradeLevel = data.UpgradeLevel;
        CircleLevel = data.CircleLevel;
    }
}
