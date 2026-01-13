using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicCircleMakeButton : MonoBehaviour
{

    public GameObject MagicCirclePrefab;
    public List<GameObject> Recycling;
    public GameObject CirCles;
    public int MaxCircle=50;


    public void ClickButton()
    {
        MagicCircleSpawn();
    }


    void MagicCircleSpawn()
    {
        bool succes = false;
        for (int i = 0; i < Recycling.Count; i++)
        {
            if (!(Recycling[i].gameObject.activeSelf))
            {
                Recycling[i].transform.position = new Vector3(0, -1, 0);
                Recycling[i].SetActive(true);
                succes = true;
                break;
            }
        }
        if (!succes && Recycling.Count < MaxCircle)
        {
            Recycling.Add(Instantiate(MagicCirclePrefab, new Vector3(0,-1,0), transform.rotation));
            Recycling[Recycling.Count - 1].transform.parent = CirCles.transform;
            Recycling[Recycling.Count - 1].SetActive(true);
        }
    }


}
