using UnityEngine;

namespace BabaIsYou
{
    [RequireComponent(typeof(Collider2D))]
    public class PropertyBlock : MonoBehaviour
    {
        [SerializeField] private string m_logicType;

        private LogicBase m_logic;

        private void Awake()
        {
            m_logic = LogicManager.GetLogic(m_logicType);
        }

        public LogicBase GetLogic()
        {
            return m_logic;
        }
    }
}