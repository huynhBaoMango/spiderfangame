﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public float speed;
    [SerializeField] private LayerMask grappleLayer;
    private Vector3 grapplePoint;
    public ropeCreator ropeCreator;
    private HingeJoint2D jointLeft;
    private HingeJoint2D jointRight;

    void Start()
    {
        ropeCreator = gameObject.GetComponent<ropeCreator>();
        jointLeft = GameObject.Find("webshooterl").GetComponent<HingeJoint2D>();
        jointRight = GameObject.Find("webshooterr").GetComponent<HingeJoint2D>();

        jointLeft.enabled = false;
        jointRight.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast
            (
                origin: Camera.main.ScreenToWorldPoint(Input.mousePosition),
                direction: Vector2.zero,
                distance: Mathf.Infinity,
                layerMask: grappleLayer
            );

            if (hit.collider != null)
            {
                grapplePoint = hit.point;
                grapplePoint.z = 0;
                bool isPosLeft = Vector2.Distance(grapplePoint, jointLeft.gameObject.transform.position) - Vector2.Distance(grapplePoint, jointRight.gameObject.transform.position) < 0;
                //right
                if (!isPosLeft)
                { 
                    ropeCreator.GenerateRope(grapplePoint, GameObject.Find("webshooterr").transform.position);
                }
                else 
                //left
                {
                    ropeCreator.GenerateRope(grapplePoint, GameObject.Find("webshooterl").transform.position);
                }

                GameObject curRope = GameObject.FindGameObjectWithTag("rope");
                Rigidbody2D[] allRigidbodies = curRope.GetComponentsInChildren<Rigidbody2D>();

                if (allRigidbodies.Length > 0)
                {
                    Rigidbody2D lastRigidbody = allRigidbodies[allRigidbodies.Length - 1];
                    if (isPosLeft)
                    {
                        jointLeft.enabled = true;
                        jointLeft.connectedBody = lastRigidbody;
                    }
                    else
                    {
                        jointRight.enabled = true;
                        jointRight.connectedBody = lastRigidbody;
                    }
                }
                else
                {
                    Debug.LogError("Không tìm thấy Rigidbody2D trong children của curRope!");
                }

            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            jointLeft.enabled = false;
            jointRight.enabled = false;
            ropeCreator.DeleteSegments();
            DestroyImmediate(GameObject.FindGameObjectWithTag("rope"));
        }

    }
}
