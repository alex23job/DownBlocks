using UnityEngine;

public class BallColor : MonoBehaviour
{
    private int _numColor;

    public int NumColor { get => _numColor; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetParams(int numColor, Material matColor)
    {
        _numColor = numColor;
        MeshRenderer mr = GetComponent<MeshRenderer>();
        Material[] mats = mr.materials;
        mats[0] = matColor;
        mr.materials = mats;

    }
}
