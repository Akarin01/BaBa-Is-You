using KitaFramework;
using UnityEngine;

public class Test : MonoBehaviour
{
    private const string PREFAB_ASSET_NAME = "Assets/GameMain/Prefabs/Square.prefab";
    public AddressableAssetLoader m_assetLoader;
    private GameObject[] m_prefabs;

    private void Awake()
    {
        m_prefabs = new GameObject[10];
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            // 加载并实例化预制体
            for (int i = 0; i < 10; i++)
            {
                m_assetLoader.LoadAsset<GameObject>(PREFAB_ASSET_NAME, new LoadAssetCallbacks(LoadPrefabSuccessCallback, LoadPrefabFailureCallback), i);
            }
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            for (int i = 0; i < 10; i++)
            {
                Destroy(m_prefabs[i]);
                m_assetLoader.UnloadAsset(PREFAB_ASSET_NAME, new UnloadAssetCallbacks(UnloadPrefabSuccessCallback, UnloadPrefabFailureCallback), null);
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            for (int i = 0; i < 10; i++)
            {
                Destroy(m_prefabs[i]);
            }

            m_assetLoader.Shutdown();
        }
    }

    private void LoadPrefabSuccessCallback(string assetName, object asset, object userData)
    {
        if (asset is not GameObject go)
        {
            Debug.LogError($"Asset {assetName} is not {typeof(GameObject)}");
            return;
        }
        int prefabIndex = (int)userData;

        // 实例化
        m_prefabs[prefabIndex] = Instantiate(go);
    }

    private void LoadPrefabFailureCallback(string assetName, string errorMsg, object userData)
    {
        Debug.Log(errorMsg);
    }

    private void UnloadPrefabSuccessCallback(string assetName, object userData)
    {
        Debug.Log($"Asset {assetName} success to unload");
    }

    private void UnloadPrefabFailureCallback(string assetName, string errorMsg, object userData)
    {
        Debug.Log(errorMsg);
    }
}
