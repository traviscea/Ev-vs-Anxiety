using UnityEngine;
using UnityEngine.Tilemaps;

public class AttackBoxScript : MonoBehaviour
{
    public int damage = 1;

    public SBKManager sBKManager;

    void OnTriggerEnter2D(Collider2D collider2D){
        if(collider2D.CompareTag("Destructible")){
            Debug.Log("HIT");
            Destroy(collider2D.gameObject);
        }
        if(collider2D.CompareTag("Projectile")){
            Debug.Log("HIT");
            Destroy(collider2D.gameObject);
            ++sBKManager.sbkNote_Count;
        }
    }
}
