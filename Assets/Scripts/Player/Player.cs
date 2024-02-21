using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    [Header("Life info")]
    public int HP = 3;
    public int damage { get; private set; }
    [SerializeField] private float coolTime = 1.0f;

    [Header("Move info")]
    public float jumpForce = 12f;

    [Header("Wire Action Info")]
    public GameObject hook;
    public LineRenderer line;
    public float hookSpeed;
    public float maxDist;
    public bool isHookActive;
    public bool isLineMax;
    public bool isAttach;
    public Vector2 dir = new Vector2(1, 1.5f).normalized;

    [Header("Collision info")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask whatIsGround;
 
    #region Componets
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public SpriteRenderer spriteRenderer { get; private set; }
    public Collider2D col { get; private set; }

    #endregion

    #region States
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerRunState runState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerFallState fallState { get; private set; }
    public PlayerShootWireState shootWireState { get; private set; }
    public PlayerOnWireState onWireState { get; private set; }
    public PlayerWireJumpState wireJumpState { get; private set; }
    public PlayerDeadState deadState { get; private set; }

    #endregion

    private void Awake()
    {
        stateMachine = new PlayerStateMachine();

        runState = new PlayerRunState(this, stateMachine, "Run");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        fallState = new PlayerFallState(this, stateMachine, "Jump");
        shootWireState = new PlayerShootWireState(this, stateMachine, "ShootWire", hook, line);
        onWireState = new PlayerOnWireState(this, stateMachine, "OnWire", hook, line);
        wireJumpState = new PlayerWireJumpState(this, stateMachine, "WireJump", hook);
        deadState = new PlayerDeadState(this, stateMachine, "Dead");
    }

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();

        stateMachine.Initialize(runState);
    }

    private void Update()
    {
        //Debug.Log(stateMachine.currentState);
        //Debug.Log(HP);
        //Debug.Log(GameManager.Instance.curScore);
        stateMachine.currentState.Update();

        if (rb.transform.position.y < -0.45f && stateMachine.currentState != onWireState && stateMachine.currentState != wireJumpState)
            col.enabled = false;

        if (rb.transform.position.y <= -3.0f) //낙사 = 즉사
            OnDamaged(HP);

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle") || 
            (collision.gameObject.CompareTag("BreakableObstacle") && (stateMachine.currentState != wireJumpState && stateMachine.currentState != onWireState))) //충돌 피해 1
        {
            //Debug.Log(collision.gameObject);
            OnDamaged(1);
        }
    }

    public void SetVelocity(float _xVelocity, float _yVelocity)
    {
        rb.velocity = new Vector2(_xVelocity, _yVelocity);
    }

    public bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);

    public void OnDamaged(int _damage)
    {
        damage = _damage;
        HP -= damage;

        if (HP == 0)
        {
            stateMachine.ChangeState(deadState);
        }
        else
        {
            //Change Layer
            gameObject.layer = 6;

            //Control Alpha value
            spriteRenderer.color = new Color(1, 1, 1, 0.4f);

            Invoke("OffDamaged", coolTime);
        }
    }

    private void OffDamaged()
    {
        gameObject.layer = 8;
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }

    public void SetCoroutine(string name)
    {
        StartCoroutine(name);
    }

    private IEnumerator OnWire()
    {
        Time.timeScale = 0.8f;

        rb.gravityScale = 0.0f;
        rb.isKinematic = true;
        float transY = transform.position.y;
        float veloY = 0;
        float gra = 9.81f;
        float graScale = 14;
        float rev = 1.0f + Vector2.Distance(transform.position, hook.transform.position) * 1.4f;
        veloY -= rev;

        do
        {
            veloY += gra * graScale * Time.deltaTime;
            transform.position += Vector3.up * veloY * Time.deltaTime;
            yield return null;
        }
        while (transY > transform.position.y);
        isAttach = false;

        hook.gameObject.SetActive(false);
        stateMachine.ChangeState(wireJumpState);
        Time.timeScale = 1.0f;
    }

    private IEnumerator WireJump()
    {
        //float targetPos = Mathf.Abs(transform.position.y) * 3.0f;
        while (transform.position.y < 2.3f)
        {
            transform.position += Vector3.up * 8.8f * Time.deltaTime;
            yield return null;
        }
        rb.gravityScale = 6.0f;
        rb.isKinematic = false;
        rb.velocity = Vector3.up * 6f;

        stateMachine.ChangeState(fallState);
    }
}
