using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KitaFramework
{
    public class ProcedureManager : FrameworkManager
    {
        [SerializeField] private string m_startProcedureName;

        private IFsm<ProcedureManager> m_procedureFsm;
        private FsmManager m_fsmManager;

        protected override void Awake()
        {
            base.Awake();
        }

        private IEnumerator Start()
        {
            m_fsmManager = FrameworkEntry.GetManager<FsmManager>();

            // 通过反射获取并创建 ProcedureBase 的所有基类
            Type[] procedureTypes = GetProceduraTypes();
            ProcedureBase[] procedureBases = new ProcedureBase[procedureTypes.Length];
            for (int i = 0; i < procedureTypes.Length; i++)
            {
                Debug.Log(procedureTypes[i]);
                procedureBases[i] = (ProcedureBase)Activator.CreateInstance(procedureTypes[i]);
            }
            m_procedureFsm = m_fsmManager.CreateFsm(this, null, procedureBases);

            yield return new WaitForEndOfFrame();

            // 启动流程
            if (procedureTypes.Length == 0)
            {
                Debug.LogError("There are no subclass of ProcedureBase");
                yield break;
            }

            StartProcedure(Type.GetType(m_startProcedureName));
        }

        public override void Shutdown()
        {
            
        }

        private Type[] GetProceduraTypes()
        {
            System.Reflection.Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            List<Type> procedureTypes = new();

            foreach (var assembly in assemblies)
            {
                Type[] types = assembly.GetTypes();
                foreach (var type in types)
                {
                    if (type.IsSubclassOf(typeof(ProcedureBase)) && !type.IsAbstract)
                    {
                        procedureTypes.Add(type);
                    }
                }
            }

            return procedureTypes.ToArray();
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