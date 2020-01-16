using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Soul_Move_Script : MonoBehaviour
{
    public float speed = 0.4f;
    float cur_speed = 0;
    public List<GameObject> agn;
    Vector2 H = new Vector2(0, 0);
    GameObject Demon;

    float pa = 0.2f;			// relative strength of repulsion from other agents - 2
    float c = 0.105f;		// relative strength of attraction to the n nearest neighbours - 1.05
    float ps = 0.15f;		// relative strength of repulsion from the shepherd - 1
    float h = 0.05f;			// relative strength of proceeding in the previous direction - 0.5
    float e = 0f;	

    // Start is called before the first frame update
    void Start()
    {
        //transform.position = new Vector2(6f * (Random.value - 0.5f), 6f * (Random.value - 0.5f));        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 LCM = new Vector2(0, 0);

        int counter = 0;
        int l = agn.Count;
        for (int i = 0; i < l; i++)
        {
            LCM += agn[i].GetComponent<Rigidbody2D>().position;
            counter++;
        }
        if (counter != 0)
            LCM /= counter;
        else
            LCM = GetComponent<Rigidbody2D>().position;
        
        // Agent and Shepherd position
        Vector2 A = GetComponent<Rigidbody2D>().position;
        Vector2 Rs;
        Vector2 C;
        if (Demon != null)
        {
            //Distance between Agent and Shepherd
            Vector2 S = Demon.GetComponent<Rigidbody2D>().position;
            Rs = A - S;
            C = LCM - A;
            cur_speed = speed;
        }
        else
        {
            Rs = Vector2.zero;
            C = Vector2.zero;
            if (cur_speed > 0)
                cur_speed -= 0.1f;
        }

        // Local repulsion of Agent from nearest neighbours
        Vector2 Ra = Vector2.zero;
        for (int i = 0; i < l; i++)
        {
            Ra += (GetComponent<Rigidbody2D>().position - agn[i].GetComponent<Rigidbody2D>().position).normalized;
        }

        //Error term
        Vector2 E = new Vector2(100f * (Random.value - 0.5f), 100f * (Random.value - 0.5f));
        
        H = h * H.normalized + c * C.normalized + pa * Ra.normalized + ps * Rs.normalized + e * E.normalized;
        transform.position = A + cur_speed * H.normalized; 

    }

    private void OnTriggerEnter2D(Collider2D collision)
    { 
        if (collision.gameObject.tag == "Soul")
        {
            agn.Add(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Demon")
        {
            Demon = collision.gameObject;
            
        }

    }


    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Demon")
        {
            Demon = null;
        }
        else if (collider.gameObject.tag == "Soul")
        {
            agn.Remove(collider.gameObject);
        }
    }


}

