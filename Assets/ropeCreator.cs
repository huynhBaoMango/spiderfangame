using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using NaughtyAttributes;

public class ropeCreator : MonoBehaviour
{
    [SerializeField, Range(2, 50)] int segmentsCount = 2;

    public HingeJoint2D hingePrefab;
    public GameObject ropepref;

    [HideInInspector] public Transform[] segments;

    Vector2 GetSegmentsPosition(Vector2 posA, Vector2 posB, int segmentIndex)
    {
        float fraction = 1f / (float)segmentsCount;
        return Vector2.Lerp(posA, posB, fraction * segmentIndex);
    }
    [Button]
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

    [Button]
    public void DeleteSegments()
    {
        if (GameObject.FindGameObjectWithTag("rope").transform.childCount > 0)
        {
            for (int i = transform.childCount; i >= 0; i--)
            {
                DestroyImmediate(GameObject.FindGameObjectWithTag("rope").transform.GetChild(i).gameObject);
            }

            segments = null;
        }
    }
}
