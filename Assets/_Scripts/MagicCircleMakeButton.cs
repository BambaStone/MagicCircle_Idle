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
        StartCoroutine(startTimer());
    }
    public void ClickButton()
    {
        if (0 < SaveDataManager.Instance.NowMakeSpell)
        {
            MagicCircleSpawn();
            SaveDataManager.Instance.NowMakeSpell--;
            _nowMakeSpell--;
            MakeSpellUI.text = _nowMakeSpell + " / " + _maxMakeSpell;
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
    void MagicCircleSpawn()
    {
        bool succes = false;
        for (int i = 0; i < Recycling.Count; i++)
        {
            if (!(Recycling[i].gameObject.activeSelf))
            {
                Recycling[i].transform.position = new Vector3(0, -1, 0);
                Recycling[i].SetActive(true);
                OnMagicCircle.Add(Recycling[i]);
                Recycling[i].GetComponent<MagicCircle>().SpellNum = SaveDataManager.Instance.SpellsCount;
                SaveDataManager.Instance.SpellsCount= OnMagicCircle.Count;
                SaveDataManager.Instance.SpellLevel.Add(0);
                SaveDataManager.Instance.SpellFireOn.Add(false);
                succes = true;
                break;
            }
        }
        if (!succes && Recycling.Count < _maxHaveSpell)
        {
            Recycling.Add(Instantiate(MagicCirclePrefab, new Vector3(0,-1,0), transform.rotation));
            Recycling[Recycling.Count - 1].transform.parent = CirCles.transform;
            OnMagicCircle.Add(Recycling[Recycling.Count - 1]);
            Recycling[Recycling.Count - 1].GetComponent<MagicCircle>().SpellNum = SaveDataManager.Instance.SpellsCount;
            Recycling[Recycling.Count - 1].GetComponent<MagicCircle>().SpellFires = SpellFireObj;
            Recycling[Recycling.Count - 1].GetComponent<MagicCircle>().Spawner = gameObject.GetComponent<MagicCircleMakeButton>();
            SaveDataManager.Instance.SpellsCount = OnMagicCircle.Count;
            SaveDataManager.Instance.SpellLevel.Add(0);
            SaveDataManager.Instance.SpellFireOn.Add(false);
            Recycling[Recycling.Count - 1].SetActive(true);
        }

    }

    IEnumerator startTimer()
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
            Recycling.Add(Instantiate(MagicCirclePrefab, new Vector3(0, -1, 0), transform.rotation));
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
