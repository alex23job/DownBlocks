using UnityEngine;

public class BallMovement : MonoBehaviour
{
    [SerializeField] private int _typeBall;

    public int BallType { get { return _typeBall; } }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetParams(int typeBall)
    {
        _typeBall = typeBall;
    }
}
