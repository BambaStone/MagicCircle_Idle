using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicCircle : MonoBehaviour
{

    public SpriteRenderer SR;
    public List<Sprite> images;
    
    public bool OnSameLevel=false;
    public GameObject SameTarget;
    public int MagicLevel = 0;
    public Vector3 ClickPoint;
    public bool WallOut = false;

    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        if (images.Count <= MagicLevel)
        {
            MagicLevel = 0;
        }
        SR.sprite = images[MagicLevel];
    }

    private void OnEnable()
    {
        MagicLevel = 0;
        SR.sprite = images[MagicLevel];
        SameTarget = null;
    }
    // Update is called once per frame
    void Update()
    {
        /*
        if(Input.GetMouseButtonDown(0))
        {
            nowMagic++;
            if(images.Count<=nowMagic)
            {
                nowMagic = 0;
            }
            SR.sprite = images[nowMagic];
            
        }
        */
    }
    private void OnMouseDown()
    {
        gameObject.GetComponent<CircleCollider2D>().isTrigger = true;
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = 11;
        Debug.Log("마법진드래그");
    }

    private void OnMouseDrag()
    {
        
        ClickPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        ClickPoint.z = 0;
        gameObject.transform.position = ClickPoint;
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        
    }


    private void OnMouseUpAsButton()
    {
        if (SameTarget != null)
        {
            if (SameTarget.activeSelf)
            {
                if (SameTarget.GetComponent<MagicCircle>().MagicLevel == MagicLevel)
                {
                    SameTarget.SetActive(false);
                    MagicLevel++;
                    SR.sprite = images[MagicLevel];
                }
            }
            SameTarget = null;
        }

        

        gameObject.GetComponent<SpriteRenderer>().sortingOrder = 9;

    }
    private void OnMouseUp()
    {
        if (WallOut)
        {
            transform.position = new Vector2(0, -1);
            WallOut = false;
        }
        SameTarget = null;
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = 9;
        gameObject.GetComponent<CircleCollider2D>().isTrigger = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("MagicCircle"))
        {
            if(collision.GetComponent<MagicCircle>().MagicLevel == MagicLevel)
            {
                SameTarget = collision.gameObject;
            }
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
        if(collision.CompareTag("Wall"))
        {
            WallOut = true;
        }
    }
}
