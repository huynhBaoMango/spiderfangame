using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using NaughtyAttributes;

public class ropeCreator : MonoBehaviour
{
    [SerializeField, Range(2, 50)] int segmentsCount = 2;

    public HingeJoint2D hingePrefab;
    public TargetJoint2D targetPrefab;
    public GameObject ropepref;
    private RopeLine ropeline;

    [HideInInspector] public Transform[] segments;

    Vector2 GetSegmentsPosition(Vector2 posA, Vector2 posB, int segmentIndex)
    {
        float fraction = 1f / (float)segmentsCount;
        return Vector2.Lerp(posA, posB, fraction * segmentIndex);
    }

    public void GenerateRope(Vector2 posA, Vector2 posB)
    {
        segments = new Transform[segmentsCount];
        GameObject rope = Instantiate(ropepref, Vector2.zero, Quaternion.identity);
        for (int i = 0; i < segmentsCount; i++)
        {
            var currJoint = Instantiate(hingePrefab, GetSegmentsPosition(posA, posB, i), Quaternion.identity, rope.transform);
            segments[i] = currJoint.transform;

            if (i > 0)
            {
                int prevIndex = i - 1;
                currJoint.connectedBody = segments[prevIndex].GetComponent<Rigidbody2D>();
            }
        }
    }

    public void GenerateRope2(Vector2 posA, Vector2 posB)
    {
        segments = new Transform[1];
        GameObject rope = Instantiate(ropepref, Vector2.zero, Quaternion.identity);
        var currJoint = Instantiate(targetPrefab, posA, Quaternion.identity, rope.transform);
        segments[0] = currJoint.transform;
        currJoint.target = currJoint.transform.position;
        
    }

    public void DeleteSegments()
    {
        Destroy(GameObject.FindGameObjectWithTag("rope"));
        segments = null;
    }

}
