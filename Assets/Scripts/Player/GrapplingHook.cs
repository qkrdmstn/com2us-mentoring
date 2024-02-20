using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    public LineRenderer line;
    public Transform hook;
    public Rigidbody2D rb;

    public float hookSpeed;
    public float maxDist;
    public float time;

    bool isHookActive;
    bool isLineMax;
    public bool isAttach;
    Vector2 dir = new Vector2(1, 1.5f).normalized;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        line.positionCount = 2;
        line.endWidth = line.startWidth = 0.05f;
        line.SetPosition(0, transform.position);
        line.SetPosition(1, hook.position);
        line.useWorldSpace = true;
        isAttach = false;
    }

    // Update is called once per frame
    void Update()
    {
        line.SetPosition(0, transform.position);
        line.SetPosition(1, hook.position);
        
        if(Input.GetKeyDown(KeyCode.E))
        {
            hook.position = transform.position;

            isHookActive = true;
            hook.gameObject.SetActive(true);
        }

        if(isHookActive && !isLineMax)
        {
            hook.Translate(dir.normalized * Time.deltaTime * hookSpeed);

            if (Vector2.Distance(transform.position, hook.position) > maxDist)
            {
                isLineMax = true;
            }
        }
        else if(isHookActive && isLineMax && !isAttach)
        {
            hook.position = Vector2.MoveTowards(hook.position, transform.position, Time.deltaTime * hookSpeed);
            if(Vector2.Distance(transform.position, hook.position) < 0.1f)
            {
                isHookActive = false;
                isLineMax = false;
                hook.gameObject.SetActive(false);
            }
        }
        else if(isAttach)
        {
            float dist = Vector2.Distance(transform.position, hook.position);
            Vector2 invDir = -dist * dir;


            StartCoroutine("Wire");
            isAttach = false;
        }

        time += Time.deltaTime;

    }

    IEnumerator Wire()
    {
        rb.gravityScale = 0.0f;
        rb.isKinematic = true;
        float transY = transform.position.y;
        float veloY = 0;
        float gra = 9.81f;
        float graScale = 14;
        float rev = 20f + Vector2.Distance(transform.position, hook.position) * 1.2f;
        veloY -= rev;

        do
        {
            veloY += gra * graScale * Time.deltaTime;
            transform.position += Vector3.up * veloY * Time.deltaTime;
            yield return null;
        }
        while (transY > transform.position.y);
        WireJump();

    }

    void WireJump()
    {
        rb.isKinematic = false;
        isAttach = false;
        isHookActive = false;
        isLineMax = false;
        hook.gameObject.SetActive(false);

        StartCoroutine("WireJumping");
    }

    private IEnumerator WireJumping()
    {
        while (transform.position.y < 2.3f)
        {
            transform.position += Vector3.up * 3 * Time.deltaTime;
            yield return null;
        }
        rb.gravityScale = 6.0f;
        rb.isKinematic = false;
        rb.velocity = Vector3.up * 6f;
    }
}
