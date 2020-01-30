using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyScript : MonoBehaviour
{
    public GameObject Enemy;
    private GameObject Enemy1;
    private GameObject Enemy2;
    

    private Vector3Int coord1 = new Vector3Int(-1, -1, 0);
    private Vector3Int coord2 = new Vector3Int(0, 0, 0);

    private Vector3 last1;
    private Vector3 last2;

    private Tilemap map;
    public Tilemap map_pit;
    public Tilemap map_view1;
    public Tilemap map_view2;

    public Tile tile;

    private Camera mainCamera;

    private float speed;
    private Vector3 vel;
    void Start()
    {
        map = GetComponent<Tilemap>();
        mainCamera = Camera.main;
        speed = 0.7f;
        vel = map.CellToWorld(map.LocalToCell(new Vector3Int(0, 0, 0) - new Vector3Int(-2, 1, 0))).normalized;

        Enemy1 = Instantiate(Enemy, map.CellToWorld(map.LocalToCell(coord1)), Quaternion.identity);
        Enemy2 = Instantiate(Enemy, map.CellToWorld(map.LocalToCell(coord2)), Quaternion.identity);

        Enemy1.GetComponent<Rigidbody2D>().velocity = vel*speed;
        Enemy2.GetComponent<Rigidbody2D>().velocity = vel*speed;

        last1 = coord1;
        last2 = coord2;
    }

    // Update is called once per frame
    void Update()
    {
        if(map.LocalToCell(last1) != map.LocalToCell(Enemy1.GetComponent<Rigidbody2D>().transform.position))
        {
            //Debug.Log(map.LocalToCell(last1));
            last1 = Enemy1.GetComponent<Rigidbody2D>().transform.position;
            Invoke("NewMas1", 0.0f);
        }

        if (map.LocalToCell(last2) != map.LocalToCell(Enemy2.GetComponent<Rigidbody2D>().transform.position))
        {
            //Debug.Log(map.LocalToCell(last2));
            last2 = Enemy2.GetComponent<Rigidbody2D>().transform.position;
            Invoke("NewMas2", 0.0f);

        }
    }

    private void NewMas1()
    {
        map_view1.ClearAllTiles();
        Vector3 pos = Enemy1.GetComponent<Rigidbody2D>().position;
        map_view1.SetTile(map.LocalToCell(last1), tile);
        map_view1.SetTile(map.LocalToCell(new Vector3(last1.x + 1, last1.y, last1.z)), tile);
        map_view1.SetTile(map.LocalToCell(new Vector3(last1.x - 1, last1.y, last1.z)), tile);
        map_view1.SetTile(map.LocalToCell(new Vector3(last1.x, last1.y + 0.5f, last1.z)), tile);
        map_view1.SetTile(map.LocalToCell(new Vector3(last1.x, last1.y - 0.5f, last1.z)), tile);
        map_view1.SetTile(map.LocalToCell(new Vector3(last1.x + 0.5f, last1.y - 0.25f, last1.z)), tile);
        map_view1.SetTile(map.LocalToCell(new Vector3(last1.x + 0.5f, last1.y + 0.25f, last1.z)), tile);
        map_view1.SetTile(map.LocalToCell(new Vector3(last1.x - 0.5f, last1.y - 0.25f, last1.z)), tile);
        map_view1.SetTile(map.LocalToCell(new Vector3(last1.x - 0.5f, last1.y + 0.25f, last1.z)), tile);

    }

    private void NewMas2()
    {
        map_view2.ClearAllTiles();
        Vector3 pos = Enemy1.GetComponent<Rigidbody2D>().position;
        map_view2.SetTile(map.LocalToCell(last2), tile);
        map_view2.SetTile(map.LocalToCell(new Vector3(last2.x + 1, last2.y, last2.z)), tile);
        map_view2.SetTile(map.LocalToCell(new Vector3(last2.x - 1, last2.y, last2.z)), tile);
        map_view2.SetTile(map.LocalToCell(new Vector3(last2.x, last2.y + 0.5f, last2.z)), tile);
        map_view2.SetTile(map.LocalToCell(new Vector3(last2.x, last2.y - 0.5f, last2.z)), tile);
        map_view2.SetTile(map.LocalToCell(new Vector3(last2.x + 0.5f, last2.y - 0.25f, last2.z)), tile);
        map_view2.SetTile(map.LocalToCell(new Vector3(last2.x + 0.5f, last2.y + 0.25f, last2.z)), tile);
        map_view2.SetTile(map.LocalToCell(new Vector3(last2.x - 0.5f, last2.y - 0.25f, last2.z)), tile);
        map_view2.SetTile(map.LocalToCell(new Vector3(last2.x - 0.5f, last2.y + 0.25f, last2.z)), tile);
        map_view2.SetTile(map.LocalToCell(Enemy2.GetComponent<Rigidbody2D>().position), tile);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.GetComponent<Rigidbody2D>().velocity = new Vector3(-1*collision.GetComponent<Rigidbody2D>().velocity.x, -1*collision.GetComponent<Rigidbody2D>().velocity.y, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Pit")
        {
            Debug.Log("Pit");
        }
    }
}
