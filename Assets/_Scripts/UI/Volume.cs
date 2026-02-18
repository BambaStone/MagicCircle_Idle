using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Volume : MonoBehaviour
{
    public AudioSource Audio;
    public float soundOriginVoluem=1f;
    // Start is called before the first frame update
    void Start()
    {
        Audio = GetComponent<AudioSource>();
        Audio.volume = SaveDataManager.Instance.Volume;
    }

    // Update is called once per frame
    void Update()
    {
        if(Audio.volume!=SaveDataManager.Instance.Volume)
        {
            Audio.volume = SaveDataManager.Instance.Volume*soundOriginVoluem;
        }
    }
}
