using UnityEngine;
using UnityEngine.SceneManagement;


public class Finish_Level : MonoBehaviour
{
    
    private Rigidbody2D _rigidBody;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Finish"))
        {
            SceneManager.LoadScene("Level_Finish");
        }
    }
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

}
