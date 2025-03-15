using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KitaFramework
{
    public class ProcedureManager : FrameworkManager
    {
        [SerializeField] private string[] m_availableProcedureTypeNames;
        [SerializeField] private string m_entranceProcedureTypeName;

        private IFsm<ProcedureManager> m_procedureFsm;
        private FsmManager m_fsmManager;

        public ProcedureBase CurrentProcedure => (ProcedureBase)m_procedureFsm.CurrentState;

        protected override void Awake()
        {
            base.Awake();
        }

        private IEnumerator Start()
        {
            m_fsmManager = FrameworkEntry.GetManager<FsmManager>();

            // 创建可使用的流程子类的实例
            ProcedureBase[] procedureBases = new ProcedureBase[m_availableProcedureTypeNames.Length];
            ProcedureBase entranceProcedure = null;
            for (int i = 0; i < procedureBases.Length; i++)
            {
                // 实例化子类
                Type procedureType = Type.GetType(m_availableProcedureTypeNames[i]);
                if (procedureType == null)
                {
                    Debug.LogError($"Can't find type {m_availableProcedureTypeNames[i]}");
                    yield break;
                }

                procedureBases[i] = Activator.CreateInstance(procedureType) as ProcedureBase;
                if (procedureBases[i] == null)
                {
                    Debug.LogError($"Can't create instance of {procedureType}");
                    yield break;
                }

                if (m_entranceProcedureTypeName == m_availableProcedureTypeNames[i])
                {
                    entranceProcedure = procedureBases[i];
                }
            }
            m_procedureFsm = m_fsmManager.CreateFsm(this, null, procedureBases);

            yield return new WaitForEndOfFrame();

            // 启动流程
            if (entranceProcedure == null)
            {
                Debug.LogError("Entrance procedure is invalid");
                yield break;
            }

            StartProcedure(entranceProcedure.GetType());
        }

        public override void Shutdown()
        {
            m_fsmManager.RemoveFsm<IFsm<ProcedureManager>>(m_procedureFsm.Name);
        }

        private void StartProcedure<TProcedure>() where TProcedure : ProcedureBase
        {
            if (m_procedureFsm == null)
            {
                throw new Exception("Procedure fsm should be initialized first");
            }
            m_procedureFsm.Start<TProcedure>();
        }

        private void StartProcedure(Type procedureType)
        {
            if (m_procedureFsm == null)
            {
                throw new Exception("Procedure fsm should be initialized first");
            }
            m_procedureFsm.Start(procedureType);
        }
    }
}