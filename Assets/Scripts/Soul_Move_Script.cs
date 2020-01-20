using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Soul_Move_Script : MonoBehaviour
{
    float speed = 0.2f;
    float cur_speed = 0;
    public List<GameObject> agn;
    //Rigidbody2D body;

    Vector2 H = new Vector2(0, 0);
    Vector2 LCM;
    Vector2 A;
    Vector2 Rs;
    Vector2 C;
    Vector2 S;
    Vector2 Ra;
    Vector2 E;

    GameObject Demon;
    bool demon_near = false;

    float pa = 0f;			// relative strength of repulsion from other agents - 2
    float c = 0.2f;		// relative strength of attraction to the n nearest neighbours - 1.05
    float ps = 0.15f;		// relative strength of repulsion from the shepherd - 1
    float h = 0.05f;			// relative strength of proceeding in the previous direction - 0.5
    float e = 0.001f;	

    // Start is called before the first frame update
    void Start()
    {
        //body = GetComponent<Rigidbody2D>();
        //transform.position = new Vector2(6f * (Random.value - 0.5f), 6f * (Random.value - 0.5f));        
    }

    // Update is called once per frame
    void Update()
    {
        LCM = new Vector2(0, 0);

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
        A = GetComponent<Rigidbody2D>().position;

        if (demon_near && Demon != null)
        {
            //Distance between Agent and Shepherd
            S = Demon.GetComponent<Rigidbody2D>().position;
            Rs = A - S;
            C = LCM - A;
            cur_speed = speed;
        }
        else
        {
            Rs = Vector2.zero;
            C = Vector2.zero;
            cur_speed -= 0.05f;
            if (cur_speed <= 0)
                cur_speed = 0;
                
        }

        // Local repulsion of Agent from nearest neighbours
        Ra = Vector2.zero;
        for (int i = 0; i < l; i++)
        {
            Ra += (GetComponent<Rigidbody2D>().position - agn[i].GetComponent<Rigidbody2D>().position).normalized;
        }

        //Error term
        E = new Vector2(10f * (Random.value - 0.5f), 10f * (Random.value - 0.5f));
        
        H = h * H.normalized + c * C.normalized + pa * Ra.normalized + ps * Rs.normalized ;

        //Vector3 finalMovement = A + cur_speed * H.normalized + e * E.normalized;
        transform.position = A + cur_speed * H.normalized + e * E.normalized;
        //body.AddForce((finalMovement - transform.position).normalized * speed);

    }

    private void OnTriggerEnter2D(Collider2D collider)
    { 

        if (collider.gameObject.tag == "Demon")
        {
            Demon = collider.gameObject;
            demon_near = true; 
        }

        
        if (collider.gameObject.tag == "Soul")
        {
            agn.Add(collider.gameObject);
        }
    }

    
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Demon")
        {
            Demon = null;
            demon_near = false;
        }
        if (collider.gameObject.tag == "Soul")
        {
            agn.Remove(collider.gameObject);
        }

    }

    private void OnCollisionEnter(Collision collider)
    {
        if (collider.gameObject.tag == "Pit")
        {
            Destroy(gameObject);
            Main.Score += 1;
        }

        //private void OnCollisionExit(Collision collider)
        //{
        //}
    }
}

