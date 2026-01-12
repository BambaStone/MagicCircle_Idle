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

    private Vector3 offset;
    public Vector3 ClickPoint;
    public bool WallOut = false;
    // Start is called before the first frame update
    void Start()
    {
        if (images.Count <= MagicLevel)
        {
            MagicLevel = 0;
        }
        SR.sprite = images[MagicLevel];
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
        

    }

    private void OnMouseDrag()
    {
        ClickPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        ClickPoint.z = 0;
        gameObject.transform.position = ClickPoint;

    }


    private void OnMouseUpAsButton()
    {
        if (SameTarget != null)
        {
            Destroy(SameTarget);
            MagicLevel++;
            SR.sprite = images[MagicLevel];
        }

        if(WallOut)
        {
            gameObject.transform.position = new Vector2(0, -1);
            WallOut = false;
        }

        

    }
    private void OnMouseUp()
    {
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
