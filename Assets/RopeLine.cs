using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeLine : MonoBehaviour
{
    ropeCreator rope;
    LineRenderer line;

    private void Awake()
    {
        rope = GetComponent<ropeCreator>();
        line = GetComponent<LineRenderer>();

    }

    private void Update()
    {
        if (rope.segments != null)
        {
            line.enabled = true;
            line.positionCount = rope.segments.Length;
            for (int i = 0; i < rope.segments.Length; i++)
            {
                line.SetPosition(i, rope.segments[i].position);
            }
        }
        else
        {
            line.enabled = false;
        }
    }
}
