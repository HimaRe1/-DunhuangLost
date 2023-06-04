using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowAt : MonoBehaviour
{
    public Transform At;
    Vector3 point;
    private void Start()
    {
        point = transform.position - At.position;
    }

    private void Update()
    {
        if(At != null)
            transform.position = point + At.position;
    }
}
