using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wheel : UIBase
{
    public Button m_playBtn;
    [SerializeField]
    private Spin m_spin;
    [SerializeField]
    private Transform m_sectionsPoint;
    [HideInInspector]
    public WheelData wheelData;
    private int winId;
    private bool m_isTesting;
    protected override void Awake()
    {
        base.Awake();
        sign = "Wheel";
        wheelData = (WheelData)Resources.Load("DataResources/datas/WheelData");
        InitUI();
    }

    private void OnDestroy()
    {
        base.DestroyUI();
    }

    #region public
    public override void Render()
    {
        base.Render();
    }
    #endregion

    #region private
    private void InitUI()
    {
        m_playBtn.onClick.AddListener(delegate ()
        {
            OnBtnPlayClick();
        });
        InitSectionItems();
    }

    private void InitSectionItems()
    {
        float rotateOffset = 360 / wheelData.dataset.Length;
        float startAngle = -rotateOffset / 2;
        foreach (var item in wheelData.dataset)
        {
            UIBase uiItem = UIManager.Instance.CreateUI("SectionItem", m_sectionsPoint);
            uiItem.InitData(item);
            uiItem.Render();
            uiItem.SetRotate(startAngle);
            startAngle = startAngle - rotateOffset;
        }
    }

    private void OnBtnPlayClick()
    {
        m_playBtn.gameObject.SetActive(false);
        PlayWheel();
    }

    private void PlayWheel()
    {
        m_spin.SetSpin(true);
        m_spin.SetOnSuccessDelegate(SpinSuccess);
        SetStopSpinAngle();
    }

    private void SpinSuccess()
    {
        UIBase panel = UIManager.Instance.CreateUI("WheelReward", UIManager.Instance.uiCanvas.transform);
        panel.InitData(wheelData.dataset[winId]);
        panel.Render();
        Destroy(gameObject);
    }

    private void SetStopSpinAngle()
    {
        winId = GetResultIndex();
        float rotateOffset = 360 / wheelData.dataset.Length;
        float startAngle = -rotateOffset / 2;
        m_spin.SetStopAngle(startAngle - (wheelData.dataset.Length - winId -1) * rotateOffset);
    }

    /// <summary>
    /// set the result data index
    /// </summary>
    private int GetResultIndex()
    {
        int randNum = Random.Range(1, 100);
        int tmpIndex = 0;
        int sum = 0;
        foreach (var item in wheelData.dataset)
        {
            sum = sum + int.Parse(item.Cols[2]);
            if (sum < randNum)
            {
                tmpIndex = item.Id;
            }
        }
        if (m_isTesting)
        {
            m_isTesting = false;
            return winId;
        }
        return tmpIndex;
    }
    #endregion

    #region testing functions
    public void testResults(int _testTimes)
    {
        Dictionary<int, int> countMap = new Dictionary<int, int>();
        for (int i = 1; i <= wheelData.dataset.Length; i++)
        {
            countMap.Add(i, 0);
        }
        for (int i = 0; i < _testTimes; i++)
        {
            int tmpSection = GetResultIndex() + 1;
            countMap[tmpSection] += 1;
        }
        for (int i = 1; i <= wheelData.dataset.Length; i++)
        {
            Debug.Log("Section" + i + "  show up " + countMap[i] + ", the rate is " + System.Math.Round(countMap[i] / (_testTimes * 1.0), 4) * 100 + "%");
        }
    }

    public void testSectionResult(int _id)
    {
        m_isTesting = true;
        winId = _id;
        OnBtnPlayClick();
    }

    #endregion
}
