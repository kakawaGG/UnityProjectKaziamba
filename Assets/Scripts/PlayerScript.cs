using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private bool seek;
    // Start is called before the first frame update
    void Start()
    {
        seek = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool Seek()
    {
        return seek;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Pit")
        {
            seek = true;
        }
        else if (collision.gameObject.tag == "Soul")
        {
            Destroy(collision.gameObject);
            Debug.Log("+soul");
        }
        else if (collision.gameObject.tag == "Jam")
        {
            seek = true;
            Debug.Log("You-re in Jam");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Pit")
        {
            seek = false;
        }
        else if (collision.gameObject.tag == "Jam")
        {
            seek = false;
            //Debug.Log("You-re in Jam");
        }
    }
}
