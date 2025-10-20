using UnityEngine;

public class SparkColCheck : MonoBehaviour
{
    private float floatingSpeed = 5;
    private float floatingHeight = 0.5f;
    private float newY;
    public float rotateSpeed;
    private Vector3 initialPos;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ChargeCounter juice = other.GetComponent<ChargeCounter>();

            if (juice != null)
            {
                juice.JuiceUp();
                Object.Destroy(this.gameObject);

            }
            else
            {
                Debug.LogError("you screwed up the connections");
            }
        }
    
    
    
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initialPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        newY = Mathf.Sin(Time.time * floatingSpeed);
        transform.position = new Vector3(initialPos.x, initialPos.y + newY * floatingHeight, initialPos.z);
    }
    private void FixedUpdate()
    {
        transform.Rotate(0, rotateSpeed, 0);
    }
}
