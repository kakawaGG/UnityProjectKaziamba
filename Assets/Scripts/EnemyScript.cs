using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyScript : MonoBehaviour
{
    public GameObject Enemy;
    private GameObject Enemy1;
    private GameObject Enemy2;
    private Tilemap map;
    public Tilemap map_pit;
    private Camera mainCamera;
    private float speed;
    private Vector3 vel = new Vector3(0.9f, -0.4f, 0f);
    // Start is called before the first frame update
    void Start()
    {
        map = GetComponent<Tilemap>();
        mainCamera = Camera.main;
        speed = 0.7f;
        Enemy1 = Instantiate(Enemy, Coord(new Vector3(-0.5f, -1f, 0f)), Quaternion.identity);
        Enemy2 = Instantiate(Enemy, Coord(new Vector3(0.5f, 0.25f, 0f)), Quaternion.identity);
        Enemy1.GetComponent<Rigidbody2D>().velocity = vel*speed;
        Enemy2.GetComponent<Rigidbody2D>().velocity = vel*speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Vector3Int CoordCamera(Vector3 position)
    {
        Vector3Int clickPositionCell = map.WorldToCell(mainCamera.ScreenToWorldPoint(position));
        clickPositionCell.z = 0;
        return clickPositionCell;
    }

    private Vector3 Coord(Vector3 position)
    {        
        return map.CellToWorld(map.WorldToCell(position)) ;
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.GetComponent<Rigidbody2D>().velocity = new Vector3(-1*collision.GetComponent<Rigidbody2D>().velocity.x, -1*collision.GetComponent<Rigidbody2D>().velocity.y, 0);
        }
    }
}
