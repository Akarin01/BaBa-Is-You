using UnityEngine;

namespace BabaIsYou
{
    [RequireComponent(typeof(Collider2D))]
    public class NounBlock : MonoBehaviour
    {
        [SerializeField] private EntityBase m_entity;

        public void SetEntityLogic(LogicBase logic)
        {
            m_entity.Logic = logic;
        }
    }
}