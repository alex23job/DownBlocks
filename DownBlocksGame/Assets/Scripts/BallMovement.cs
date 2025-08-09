using UnityEngine;

public class BallMovement : MonoBehaviour
{
    [SerializeField] private int _typeBall;

    public int BallType { get { return _typeBall; } }

    private Vector3 _target;
    private bool _isMoving = false;
    private float _speed = 20f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_isMoving)
        {
            Vector3 delta = transform.position - _target;
            Vector3 movement = delta.normalized * _speed * Time.deltaTime;
            transform.position -= movement;
            if (delta.magnitude < 0.1f)
            {
                transform.position = _target;
                _isMoving = false;
                if (transform.parent == null) Destroy(gameObject);
            }
        }
    }

    public void SetParams(int typeBall)
    {
        _typeBall = typeBall;
    }

    public void SetTarget(Vector3 tg)
    {
        _target = tg;
        _isMoving = true;
    }

    public void StopMovement()
    {
        _isMoving = false; 
    }
}
