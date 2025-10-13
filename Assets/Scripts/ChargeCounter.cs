using UnityEngine;

public class ChargeCounter : MonoBehaviour
{

    private int juice = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Juice Initial Count : " + juice);
    }

    public void JuiceUp()
    {
        juice += 1;
        Debug.Log("Juice : " + juice);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
