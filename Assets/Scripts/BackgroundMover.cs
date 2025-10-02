using UnityEngine;

public class BackgroundMover : MonoBehaviour
{
    public float speed = 1f;      
    public float repeatDistance = 1.4f;
    private Vector3 startPosition;    

    void Start()
    {
        startPosition = transform.position; 
    }

    void Update()
    {
        
        transform.position += new Vector3(-1, -1, 0).normalized * speed * Time.deltaTime;

        if (Vector3.Distance(startPosition, transform.position) >= repeatDistance)
        {
            transform.position = startPosition; // Reset back to start
        }
    }
}
