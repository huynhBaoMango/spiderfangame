using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPlayer : MonoBehaviour
{
    public Transform followPos;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = followPos.position;
        pos.z = -10;
        pos.y += 4;
        transform.position = pos;
    }
}
