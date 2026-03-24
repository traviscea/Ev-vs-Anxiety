using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;
using System.Collections.Generic;

public class DestructibleTiles : MonoBehaviour
{
    public Tilemap destructibletilemap;

    private void Start()
    {
        destructibletilemap= GetComponent<Tilemap>();
    }

 
    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.CompareTag("AttackHitBox"))
        {
            Vector3 hitPosition = Vector3.zero;
            foreach(ContactPoint2D hit in collision2D.contacts){
                hitPosition.x = hit.point.x - 0.01f * hit.normal.x;
                hitPosition.y = hit.point.y - 0.01f * hit.normal.y;
                destructibletilemap.SetTile(destructibletilemap.WorldToCell(hitPosition), null);
            }
        
        }
    }
}
