using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell_11 : MonoBehaviour
{
    public GameObject TriggerEffect;

    public Animator ani;
    public float rotationSpeed = 0.1f; // 회전 속도 조절 변수

    public float HitRate = 0;


    private GameObject _target;
    private Rigidbody2D _rigidbody2D;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if(_target == null)
        {
            SetTarget();
        }
        if(!(ani.GetCurrentAnimatorStateInfo(0).normalizedTime<1f))
        {
            ani.gameObject.SetActive(false);
            Destroy(gameObject,1f);
        }
        if (_target != null && ani.gameObject.activeSelf)
        {
            rotationSpeed = rotationSpeed + Time.deltaTime * 5;
            Vector2 targetPosition = _target.transform.position;
            // 현재 오브젝트의 위치를 가져오기
            Vector2 currentPosition = transform.position;
            // 두 지점 사이의 벡터를 계산
            Vector2 direction = targetPosition - currentPosition;
            // 벡터를 각도로 변환.
            // Math.Atan2는 각도를 라디안으로 반환
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            // 각도를 Quaternion.Euler로 변환
            // 2D이므로 Z축을 기준으로 회전
            Quaternion rotation = Quaternion.Euler(0f, 0f, angle - 90f);
            // 오브젝트의 회전값을 설정
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);

        }
    }

    public void SetTarget()
    {
        _target = GetComponent<SpellData>().Target;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (ani.gameObject.activeSelf)
        {
            if (collision.CompareTag("Enemy"))
            {
                HitRate = HitRate + Time.deltaTime;
                if (1 <= HitRate)
                {
                    HitRate = 0;
                    Vector2 closets = GetComponent<BoxCollider2D>().ClosestPoint(collision.transform.position);
                    Instantiate(TriggerEffect, closets, Quaternion.identity);
                }
            }
        }
    }
}
