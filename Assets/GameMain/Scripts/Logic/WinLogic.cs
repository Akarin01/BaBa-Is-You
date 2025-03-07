using UnityEngine;

namespace BabaIsYou
{
    public class WinLogic : LogicBase
    {
        public override void OnFixedUpdate(EntityBase entity)
        {

        }

        public override void OnUpdate(EntityBase entity)
        {

        }

        public override bool TryInteract(EntityBase initiator, EntityBase receiver)
        {
            // 游戏胜利
            if (initiator.Logic is YouLogic)
            {
                Debug.Log("Win");

                GameEntry.Event.Fire(GameWinArgs.EventID, this, new GameWinArgs());
            }
            return true;
        }
    }
}