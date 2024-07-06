using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeLine : MonoBehaviour
{
    ropeCreator rope;
    public LineRenderer line;
    playerController pctrl;

    private void Awake()
    {
        rope = GetComponent<ropeCreator>();
        line = GetComponent<LineRenderer>();
        pctrl = GetComponent<playerController>();
        line.enabled = false;
    }

    private void Update()
    {
        if (rope.segments != null)
        {
            if(rope.segments.Length != 1)
            {
                line.enabled = true;
                line.positionCount = rope.segments.Length;
                for (int i = 0; i < rope.segments.Length; i++)
                {
                    line.SetPosition(i, rope.segments[i].position);
                }
            }
            if (rope.segments.Length == 1)
            {
                line.enabled = true;
                line.positionCount = 2;
                line.SetPosition(1, pctrl.grapplePoint);
                if (pctrl.isPosLeft)
                {
                    line.SetPosition(0, GameObject.Find("webshooterl").transform.position);
                }
                else
                {
                    line.SetPosition(0, GameObject.Find("webshooterr").transform.position);
                }
            }
        }
        else
        {
            line.enabled = false;
        }
    }
}
