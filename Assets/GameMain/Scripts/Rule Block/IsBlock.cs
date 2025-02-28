using UnityEngine;

public class IsBlock : MonoBehaviour
{
    #region 上一帧检测到的规则块
    private NounBlock m_prevLeftNoun = null;
    private NounBlock m_prevUpNoun = null;
    #endregion

    private void Awake()
    {
        EntityBase.OnEntityArrived += UpdateRule;
    }

    private void Start()
    {
        UpdateRule();
    }

    private void OnDisable()
    {
        EntityBase.OnEntityArrived -= UpdateRule;
    }

    private void UpdateRule()
    {
        ModifyRule(new Vector2(-1, 0), ref m_prevLeftNoun);
        ModifyRule(new Vector2(0, 1), ref m_prevUpNoun);
    }

    /// <summary>
    /// 修改指定方向的规则
    /// </summary>
    /// <param name="dir">NounBlock 的方向</param>
    /// <param name="prevNoun">上一帧的 Noun Block</param>
    /// <param name="prevProperty">上一帧的 Property Block</param>
    private void ModifyRule(Vector2 dir,
        ref NounBlock prevNoun)
    {
        // Noun Block 检测
        NounBlock currNoun = null;
        Vector2 rayStart = (Vector2)transform.position + dir * 0.5f;
        RaycastHit2D hit = Physics2D.Raycast(rayStart, dir, 0.5f);
        if (hit)
        {
            currNoun = hit.transform.GetComponent<NounBlock>();
        }

        // Property Block 检测
        PropertyBlock currProperty = null;
        rayStart = (Vector2)transform.position - dir * 0.5f;
        hit = Physics2D.Raycast(rayStart, -dir, 0.5f);
        if (hit)
        {
            currProperty = hit.transform.GetComponent<PropertyBlock>();
        }

        ModifyNoun(prevNoun, currNoun, currProperty);

        prevNoun = currNoun;
    }

    private void ModifyNoun(NounBlock prevNoun, NounBlock currNoun, PropertyBlock property)
    {
        LogicBase logic = LogicManager.GetDefaultLogic();
        if (property != null)
        {
            logic = property.GetLogic();
        }
        if (prevNoun != null)
        {
            prevNoun.SetEntityLogic(LogicManager.GetDefaultLogic());
        }
        if (currNoun != null)
        {
            currNoun.SetEntityLogic(logic);
        }
    }
}
