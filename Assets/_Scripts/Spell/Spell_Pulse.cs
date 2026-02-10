using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell_Pulse : MonoBehaviour
{
    public GameObject TriggerEffect;

    public Animator ani;

    public float Speed = 1f;

    private Rigidbody2D _rigidbody2D;
    private GameObject _target;
    private bool _hitOn = false;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void SetTarget()
    {
        _target = GetComponent<SpellData>().Target;
    }

    // Update is called once per frame


    private void FixedUpdate()
    {
        if (_target == null)
        {
            SetTarget();
        }
        if (_target != null && !_hitOn)
        {
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
            // 오브젝트의 회전값을 설정
            transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
            //방향대로 이동
            transform.Translate(Vector3.up * Speed * Time.deltaTime);
        }
        if(_hitOn)
        {
            transform.Translate(Vector3.up * Speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (ani.gameObject.activeSelf)
        {
            if (collision.CompareTag("Enemy"))
            {
                Instantiate(TriggerEffect, transform.position, Quaternion.identity);
                if (SaveDataManager.Instance.BossFight)
                { }
                else
                {
                    _target.GetComponent<Stage>().Hit(GetComponent<SpellData>().Damage);
                }
                _hitOn = true;
                Destroy(gameObject, 1f);
            }
        }
    }
}
