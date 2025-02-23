using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PropertyBlock : MonoBehaviour
{
    [SerializeField] private LogicBase m_logic = LogicManager.GetDefaultLogic();

    public LogicBase GetLogic()
    {
        return m_logic;
    }
}
