using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WheelReward : UIBase
{

    [SerializeField]
    private Image m_imgIcon;
    [SerializeField]
    private Text m_txtName;
    [SerializeField]
    private Button m_closeBtn;

    protected override void Awake()
    {
        base.Awake();
        sign = "WheelReward";
        InitUI();
    }
    public override void Render()
    {
        base.Render();
        RenderIcon();
        RenderName();
    }

    public void InitUI()
    {
        m_closeBtn.onClick.AddListener(delegate ()
        {
            Destroy(gameObject);
        });
    }

    public override void InitData(ResData _data)
    {
        base.InitData(_data);
    }

    private void RenderIcon()
    {
        string path = m_resData.Cols[3];
        m_imgIcon.sprite = LoadSourceSprite("Sprites/" + path);
    }
    private void RenderName()
    {
        m_txtName.text = m_resData.Cols[0];
    }
}
