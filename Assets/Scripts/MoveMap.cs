using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMap : MonoBehaviour
{
    public float speed;
    [SerializeField] private float cullingPos;
    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.isGameOver)
        {
            Vector3 curPos = transform.position;
            Vector3 nxtPos = Vector3.left * speed * Time.deltaTime;

            transform.position = curPos + nxtPos;
            if (transform.position.x <= cullingPos)
                Destroy(this.gameObject);
        }
    }
}
