using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMap : MonoBehaviour
{
    [SerializeField] private float speed;

    // Update is called once per frame
    void Update()
    {
        Vector3 curPos = transform.position;
        Vector3 nxtPos = Vector3.left * speed * Time.deltaTime;

        transform.position = curPos + nxtPos;
        if (transform.position.x <= -20.0f)
        {
            gameObject.SetActive(false);
        }
    }
}
