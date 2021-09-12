using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class App : MonoBehaviour
{
    // Start is called before the first frame update
    public UIManager m_uiManager;
    void Start()
    {
        InitGame();
    }

    private void InitGame()
    {
        InitUI();
    }

    private void InitUI()
    {
        m_uiManager.CreateUI("Wheel", m_uiManager.uiCanvas.transform);
    }

}
