using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapScript : MonoBehaviour
{
    public GameObject Player;
    public Tilemap map_pit;

    private Tilemap map;
    private Camera mainCamera;
    private float speed;
    private float progress;
    private Vector3 fromPosition = Vector3.zero;
    private Vector3 toPosition = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        map = GetComponent<Tilemap>();
        mainCamera = Camera.main;
        speed = 1f;

        //Instantiate(Player);
        Player.GetComponent<Rigidbody2D>().position = map.CellToWorld(map.LocalToCell(new Vector3Int(-3,-1,0)));
        Player.GetComponent<Rigidbody2D>().rotation = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
            fromPosition = Player.transform.position;
            Debug.Log(map.LocalToCell( fromPosition));
            Vector3Int c = CoordCamera(Input.mousePosition);
            
            Vector3 n = (map.CellToWorld(c) - fromPosition).normalized;
            if ((System.Math.Round(n.x, 1) == 0.9 || System.Math.Round(n.x, 1) == -0.9) &&
                (System.Math.Round(n.y, 1) == 0.4 || System.Math.Round(n.y, 1) == -0.4))
            {
                toPosition = map.CellToWorld(c);
                CancelInvoke("WTF");
                Player.GetComponent<Rigidbody2D>().velocity = n * speed;
                InvokeRepeating("WTF", 0.0f, 0.1f);
            }
            else
            {
                CancelInvoke("WTF");
                Player.GetComponent<Rigidbody2D>().transform.position = Coord(Player.GetComponent<Rigidbody2D>().transform.position);
                Player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                Player.GetComponent<Rigidbody2D>().rotation = 0f;
            }

        }
        
    }

    private void WTF()
    {
        if ((toPosition - Player.transform.position).magnitude < 0.1)
        {
            CancelInvoke("WTF");
            Player.GetComponent<Rigidbody2D>().transform.position = toPosition;
            Player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            Player.GetComponent<Rigidbody2D>().rotation = 0f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            CancelInvoke("WTF");
            Player.GetComponent<Rigidbody2D>().transform.position = fromPosition;
            Player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            Player.GetComponent<Rigidbody2D>().rotation = 0f;
        }
    }

    private Vector3Int CoordCamera(Vector3 position)
    {
        Vector3Int clickPositionCell = map.WorldToCell(mainCamera.ScreenToWorldPoint(position));
        clickPositionCell.z = 0;
        return clickPositionCell;
    }

    private Vector3 Coord(Vector3 position)
    {
        return map.CellToWorld(map.WorldToCell(position)); ;
    }
}

