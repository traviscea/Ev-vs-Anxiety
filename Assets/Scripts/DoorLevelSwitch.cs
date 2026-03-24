using UnityEngine;
using UnityEngine.SceneManagement; // Essential for scene management

public class LevelLoader : MonoBehaviour
{
    public string nextLevelName; 

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            
            SceneManager.LoadScene(nextLevelName);
        
        }
    }
}