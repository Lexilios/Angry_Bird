using UnityEngine;

public class TestClick : MonoBehaviour

{
    private void Start()
    {
        Debug.Log("Start");
    }
    void OnMouseDown()
    {
        Debug.Log("Clicked!");
    }
}
