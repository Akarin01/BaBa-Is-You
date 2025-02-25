using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSceneTest : MonoBehaviour
{
    private void Start()
    {
        GameEntry.UIManager.OpenUI<MenuForm>();
    }
}
