using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicCircle : MonoBehaviour
{
    public SpriteRenderer SR;               //스펠에 적용된 이미지
    public List<Sprite> images;             //스펠의 레벨에 따른 이미지들
    public MagicCircleMakeButton Spawner;   //스펠의 스포너 오브젝트
    public GameObject SameTarget;           //스펠 드래그시 겹쳤을때 같은 단계면 타겟으로 지정될 오브젝트
    public SpellFire SpellFires;            //스펠 발동칸 오브젝트
    public ParticleSystem Effect;           //스펠의 이펙트
    public Vector3 ClickPoint;              //스펠 드래그시 마우스포인트
    public int SpellNum;                    //데이터상 저장될 스펠의 번호
    public int MagicLevel = 0;              //스펠의 단계
    //public bool OnSameLevel = false;        
    public bool WallOut = false;            //스펠이 스펠탱크 바깥으로 나갔는지 여부
    public bool OnFire = false;             //스펠이 스펠발동칸 안에 있는지 여부
    

    // Start is called before the first frame update
    void Start()
    {
        if (images.Count <= MagicLevel)
        {
            MagicLevel = 0;
        }
        SR.sprite = images[MagicLevel];
        StartOnFireSpell();
    }

    private void OnEnable()
    {
        MagicLevel = 0;
        SR.sprite = images[MagicLevel];
        SameTarget = null;
        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-1, 1), Random.Range(-1, 1)));
    }

    private void FixedUpdate()
    {
        if (SameTarget != null)
        {
            float sqrdis = (transform.position - SameTarget.transform.position).sqrMagnitude;
            if (sqrdis > 0.5f * 0.5f || !SameTarget.activeSelf)
            {
                SameTarget = null;
            }
        }
    }

    private void OnMouseDown()
    {
        if (!SaveDataManager.Instance.BossFight)
        {
            gameObject.GetComponent<CircleCollider2D>().isTrigger = true;
            Effect.GetComponent<Renderer>().sortingOrder = 11;
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = 11;
            Debug.Log("마법진드래그");
        }
    }

    private void OnMouseDrag()
    {
        if (!SaveDataManager.Instance.BossFight)
        {
            ClickPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            ClickPoint.z = 0;
            gameObject.transform.position = ClickPoint;
            if (!(gameObject.GetComponent<Rigidbody2D>().bodyType == RigidbodyType2D.Static))
            {
                gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }
        }
    }
    private void OnMouseUp()
    {
        if (!SaveDataManager.Instance.BossFight)
        {
            if (OnFire && SpellFires.FireSpellList.Count < SaveDataManager.Instance.MaxSpellFiresCount)
            {
                SameTarget = null;
                SpellFires.OnSpell(gameObject);
                SaveDataManager.Instance.SpellFireOn[SpellNum] = true;
                Effect.GetComponent<Renderer>().sortingOrder = 11;
                gameObject.GetComponent<SpriteRenderer>().sortingOrder = 11;
                gameObject.GetComponent<CircleCollider2D>().isTrigger = true;
            }
            else
            {
                if (SameTarget != null)
                {
                    if (SameTarget.activeSelf && SameTarget.GetComponent<MagicCircle>().MagicLevel == MagicLevel && MagicLevel < 11 && !OnFire)
                    {
                        SameTarget.SetActive(false);
                        LevelChange(MagicLevel + 1);
                        SaveDataManager.Instance.SpellLevel[SpellNum] = MagicLevel;
                        SaveDataManager.Instance.QuestValue[11]++;//스펠합성퀘스트
                    }
                    SameTarget = null;
                }

                if (SpellFires != null)
                {
                    SpellFires.OffSpell(gameObject);
                    SaveDataManager.Instance.SpellFireOn[SpellNum] = false;
                }

                if (WallOut)
                {
                    transform.position = new Vector2(0, -1);
                    gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-1, 1), Random.Range(-1, 1)));
                    WallOut = false;
                }

                Effect.GetComponent<Renderer>().sortingOrder = 9;
                gameObject.GetComponent<SpriteRenderer>().sortingOrder = 9;
                gameObject.GetComponent<CircleCollider2D>().isTrigger = false;
            }
        }

    }

    private void OnDisable()
    {
        if (!SaveDataManager.Instance.BossFight)
        {
            if (SpellNum != Spawner.OnMagicCircle.Count - 1)
            {
                for (int i = SpellNum; i < Spawner.OnMagicCircle.Count - 1; i++)
                {
                    Spawner.OnMagicCircle[i + 1].GetComponent<MagicCircle>().SpellNum = i;
                }
            }
            Spawner.OnMagicCircle.RemoveAt(SpellNum);
            SaveDataManager.Instance.SpellLevel.RemoveAt(SpellNum);
            SaveDataManager.Instance.SpellFireOn.RemoveAt(SpellNum);
            SaveDataManager.Instance.SpellsCount--;
            SpellNum = -1;

            Debug.Log("ondisable");
        }
    }

    //게임을 시작할때 저장데이터의 해당 스펠이 스펠발동칸에 있다고 저장되어 있는지판단
    public void StartOnFireSpell()
    {
        if (SaveDataManager.Instance.SpellFireOn[SpellNum])
        {
            SpellFires.OnSpell(gameObject);
            Effect.GetComponent<Renderer>().sortingOrder = 11;
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = 11;
            gameObject.GetComponent<CircleCollider2D>().isTrigger = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("MagicCircle"))
        {
            if (collision.GetComponent<MagicCircle>().MagicLevel == MagicLevel)
            {
                SameTarget = collision.gameObject;
            }
        }
        if (collision.CompareTag("SpellFire"))
        {
            OnFire = true;
            SpellFires = collision.gameObject.GetComponent<SpellFire>();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("MagicCircle"))
        {
            if (collision.GetComponent<MagicCircle>().MagicLevel == MagicLevel)
            {
                SameTarget = collision.gameObject;
            }
        }
        if (collision.CompareTag("Wall"))
        {
            if (WallOut)
            {
                WallOut = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("MagicCircle"))
        {
            if (collision.GetComponent<MagicCircle>().MagicLevel == MagicLevel)
            {
                SameTarget = null;
            }
        }
        if (collision.CompareTag("Wall"))
        {
            WallOut = true;
        }
        if (collision.CompareTag("SpellFire"))
        {
            OnFire = false;
        }
    }

    public void LevelChange(int level)
    {
        MagicLevel = level;
        if (11 < MagicLevel)
            MagicLevel = 11;
        SR.sprite = images[MagicLevel];
        if (SaveDataManager.Instance.UnlockSpell < MagicLevel)
        {
            SaveDataManager.Instance.UnlockSpell = MagicLevel;
        }
    }
}
