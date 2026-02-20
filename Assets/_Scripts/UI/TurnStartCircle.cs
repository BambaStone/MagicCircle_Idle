using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnStartCircle : MonoBehaviour
{
    private void FixedUpdate()
    {
        GetComponent<RectTransform>().Rotate(new Vector3(0,0,Time.deltaTime*5));
    }
}
