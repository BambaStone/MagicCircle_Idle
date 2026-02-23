using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MagicCircleMakeButton : MonoBehaviour
{

    public SpellFire SpellFireObj;
    public GameObject MagicCirclePrefab;
    public List<GameObject> Recycling;
    public GameObject CirCles;
    public TMP_Text MakeSpellUI;

    public List<GameObject> OnMagicCircle;

    private int _nowMakeSpell;
    public int _maxMakeSpell;
    private int _maxHaveSpell;
    private void Start()
    {
        StartCoroutine(StartTimer());
    }
    
    //스펠메이크 버튼
    public void ClickButton()
    {
        if (0 < SaveDataManager.Instance.NowMakeSpell)
        {
            if (OnMagicCircle.Count < SaveDataManager.Instance.MaxHaveSpell)
            {
                MagicCircleSpawn();
                SaveDataManager.Instance.QuestValue[10]++;//스펠제작퀘스트
                SaveDataManager.Instance.NowMakeSpell--;
                _nowMakeSpell--;
                MakeSpellUI.text = _nowMakeSpell + " / " + _maxMakeSpell;
            }
        }
    }

    //스펠 만들때 스펠메이크 강화 정도에 따라 상위 스펠 뜰 확률 계산 및 적용
    private void MakeHighLevelSpell(MagicCircle spell)
    {
        int random = Random.Range(0, 100);
        if(random<SaveDataManager.Instance.MakeSpellLevel)
        {
            spell.LevelChange(spell.MagicLevel+1);
            MakeHighLevelSpell(spell);
        }
    }

    private void FixedUpdate()
    {
        if(_nowMakeSpell != SaveDataManager.Instance.NowMakeSpell)
        {
            _nowMakeSpell = SaveDataManager.Instance.NowMakeSpell;
            MakeSpellUI.text = _nowMakeSpell + " / " + _maxMakeSpell;
        }
        if (_maxMakeSpell != SaveDataManager.Instance.MaxMakeSpell)
        {
            _maxMakeSpell = SaveDataManager.Instance.MaxMakeSpell;
            MakeSpellUI.text = _nowMakeSpell + " / " + _maxMakeSpell;
        }
        if (_maxHaveSpell != SaveDataManager.Instance.MaxHaveSpell)
        {
            _maxHaveSpell = SaveDataManager.Instance.MaxHaveSpell;
        }
    }

    //스펠 소환 - 리사이클링 활용
    void MagicCircleSpawn()
    {
        bool succes = false;
        for (int i = 0; i < Recycling.Count; i++)
        {
            if (!(Recycling[i].gameObject.activeSelf))
            {
                Recycling[i].transform.position = new Vector3(0, -1, -1);
                OnMagicCircle.Add(Recycling[i]);
                MagicCircle recyclingCircle = Recycling[i].GetComponent<MagicCircle>();
                recyclingCircle.SpellNum = SaveDataManager.Instance.SpellsCount;
                MakeHighLevelSpell(recyclingCircle);
                SaveDataManager.Instance.SpellsCount= OnMagicCircle.Count;
                SaveDataManager.Instance.SpellFireOn.Add(false);
                SaveDataManager.Instance.SpellLevel.Add(recyclingCircle.MagicLevel);
                Recycling[i].SetActive(true);
                succes = true;
                break;
            }
        }
        if (!succes && Recycling.Count < _maxHaveSpell)
        {            
            Recycling.Add(Instantiate(MagicCirclePrefab, new Vector3(0,-1,-1), transform.rotation));
            Recycling[Recycling.Count - 1].transform.parent = CirCles.transform;
            OnMagicCircle.Add(Recycling[Recycling.Count - 1]);
            MagicCircle recyclingCircle = Recycling[Recycling.Count - 1].GetComponent<MagicCircle>();
            recyclingCircle.SpellNum = SaveDataManager.Instance.SpellsCount;
            recyclingCircle.SpellFires = SpellFireObj;
            recyclingCircle.Spawner = gameObject.GetComponent<MagicCircleMakeButton>();
            MakeHighLevelSpell(recyclingCircle);
            SaveDataManager.Instance.SpellsCount = OnMagicCircle.Count;
            SaveDataManager.Instance.SpellFireOn.Add(false);
            SaveDataManager.Instance.SpellLevel.Add(recyclingCircle.MagicLevel);
            Recycling[Recycling.Count - 1].SetActive(true);
        }
    }

    IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(0.1f);
        Debug.Log("nowMake1 : " + SaveDataManager.Instance.NowMakeSpell);
        _nowMakeSpell = SaveDataManager.Instance.NowMakeSpell;
        Debug.Log("maxMake1 : " + SaveDataManager.Instance.MaxMakeSpell);
        _maxMakeSpell = SaveDataManager.Instance.MaxMakeSpell;
        Debug.Log("maxMake2 : " + _maxMakeSpell);
        _maxHaveSpell = SaveDataManager.Instance.MaxHaveSpell;
        MakeSpellUI.text = _nowMakeSpell + " / " + _maxMakeSpell;

        StartMakeCircle();

    }

    public void StartMakeCircle()
    {
        for (int i = 0; i < SaveDataManager.Instance.SpellsCount; i++)
        {
            Recycling.Add(Instantiate(MagicCirclePrefab, new Vector3(0, -1, -1), transform.rotation));
            Recycling[Recycling.Count - 1].GetComponent<MagicCircle>().Spawner = gameObject.GetComponent<MagicCircleMakeButton>();
            Recycling[Recycling.Count - 1].GetComponent<MagicCircle>().SpellFires = SpellFireObj;
            Recycling[Recycling.Count - 1].transform.parent = CirCles.transform;
            Recycling[Recycling.Count - 1].GetComponent<MagicCircle>().LevelChange(SaveDataManager.Instance.SpellLevel[i]);
            Recycling[Recycling.Count - 1].GetComponent<MagicCircle>().SpellNum=i;
            OnMagicCircle.Add(Recycling[Recycling.Count - 1]);
            Recycling[Recycling.Count - 1].SetActive(true);
        }
    }

}
