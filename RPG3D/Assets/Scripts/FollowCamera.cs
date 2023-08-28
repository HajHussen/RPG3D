using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] GameObject target;


    void LateUpdate()
    {
        gameObject.transform.position = target.transform.position;
    }
}
