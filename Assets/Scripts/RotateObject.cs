using UnityEngine;
using System.Collections;

public class RotateObject : MonoBehaviour
{
    public float Speed = 10;

    void Update()
    {
        transform.Rotate(new Vector3(0, 0, Time.deltaTime * Speed));
    }
}
