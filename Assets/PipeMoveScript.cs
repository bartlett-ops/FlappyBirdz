using UnityEngine;

public class PipeMoveScript : MonoBehaviour
{
    public float moveSpeed = 5;
    public float deadZone = -150;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + (Vector3.left * moveSpeed)  * Time.deltaTime;
        if (! inScreen())
        {
            Debug.Log("Pipe Deleted");
            Destroy(gameObject);
        }
    }
    private bool inScreen() {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);

        if (screenPosition.x < -500) 
        {
            return false;
        }
        return true;
    }
}
