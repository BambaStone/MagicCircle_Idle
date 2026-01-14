using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeelTankDrag : MonoBehaviour
{
    public GameObject SpellTank;

    private Vector3 offset;

    // 마우스 버튼을 눌렀을 때 호출
    void OnMouseDown()
    {
        // 오브젝트와 마우스 포인터 사이의 상대적인 위치 차이 계산

        Debug.Log("탱크드래그");
        offset = SpellTank.gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
    }

    // 마우스 드래그 중일 때 호출
    void OnMouseDrag()
    {
        // 마우스 위치를 월드 좌표로 변환하고 offset을 더하여 오브젝트 이동
        Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane);
        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint) + offset;
        if (0 < cursorPosition.y)
        {
            cursorPosition.y = 0;
        }
        if (cursorPosition.y < -17.5f)
        {
            cursorPosition.y = -17.5f;
        }
        SpellTank.transform.position = new Vector3(transform.position.x, cursorPosition.y,0);
    }
}
