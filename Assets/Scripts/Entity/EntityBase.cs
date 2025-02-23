using System.Collections;
using System;
using UnityEngine;

/// <summary>
/// 实体基类，存储实体相关数据，并利用 LogicBase 类处理逻辑
/// </summary>
[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public abstract class EntityBase : MonoBehaviour
{
    public static event Action OnEntityArrived;

    public abstract LogicBase Logic { get; set; }

    public Vector2 Position
    {
        get => m_rb2D.position;
        set => m_rb2D.position = value;
    }

    public bool IsMoving => m_isMoving;

    protected Rigidbody2D m_rb2D;

    protected Vector2Int m_targetPos;
    protected bool m_isMoving;


    protected virtual void Awake()
    {
        m_rb2D = GetComponent<Rigidbody2D>();

        m_targetPos = Vector2Int.RoundToInt(Position);
        m_isMoving = false;
    }

    protected virtual void Update()
    {
        Logic?.OnUpdate(this);
    }
    protected virtual void FixedUpdate()
    {
        Logic?.OnFixedUpdate(this);
    }

    /// <summary>
    /// 朝目标方向移动，如果实体正在移动，则不执行
    /// </summary>
    /// <param name="targetPos"></param>
    public void MoveByDir(Vector2 dir)
    {
        if (m_isMoving)
        {
            return;
        }
        m_targetPos += Vector2Int.RoundToInt(dir);
        StartCoroutine(MoveToTarget());
    }

    /// <summary>
    /// 尝试朝指定方向交互
    /// </summary>
    /// <param name="dir">指定方向</param>
    /// <returns>交互后是否可以朝该方向移动</returns>
    public bool TryInteractByDir(Vector2 dir)
    {
        Vector2 rayStart = Position + dir * 0.5f;
        RaycastHit2D hit = Physics2D.Raycast(rayStart, dir, 0.5f);
        if (hit && hit.transform.TryGetComponent(out EntityBase receiver))
        {
            if (receiver.Logic.TryInteract(this, receiver))
            {
                return true;
            }
            return false;
        }
        else
        {
            return true;
        }
    }

    IEnumerator MoveToTarget()
    {
        m_isMoving = true;
        while (Vector2.Distance(Position, m_targetPos) > 0.1f)
        {
            Position = Vector2.MoveTowards(Position, m_targetPos, Constant.MOVE_SPEED * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
        }
        // 到达目的地
        Position = m_targetPos;
        m_isMoving = false;
        OnEntityArrived?.Invoke();
    }
}