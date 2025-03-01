using UnityEngine;

namespace BabaIsYou
{
    public class EdgeEntity : EntityBase
    {
        public override LogicBase Logic
        {
            get => s_logic;
            set
            {
                Debug.LogError("Edge Entity can't change logic");
            }
        }
        private static LogicBase s_logic;

        protected override void Awake()
        {
            base.Awake();
            s_logic = LogicManager.GetLogic<StopLogic>();
        }
    }
}