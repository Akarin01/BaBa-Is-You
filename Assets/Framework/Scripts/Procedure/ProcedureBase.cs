using ProcedureOwner = KitaFramework.IFsm<KitaFramework.ProcedureManager>;

namespace KitaFramework
{
    public abstract class ProcedureBase : FsmState<ProcedureManager>
    {
        protected internal override void OnInit(ProcedureOwner procedureOwner)
        {
            base.OnInit(procedureOwner);
        }

        protected internal override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);
        }

        protected internal override void OnUpdate(ProcedureOwner procedureOwner, float deltaTime, float realDeltaTime)
        {
            base.OnUpdate(procedureOwner, deltaTime, realDeltaTime);
        }

        protected internal override void OnExit(ProcedureOwner procedureOwner, bool isShutdown)
        {
            base.OnExit(procedureOwner, isShutdown);
        }

        protected internal override void OnRelease(ProcedureOwner procedureOwner)
        {
            base.OnRelease(procedureOwner);
        }
    }
}