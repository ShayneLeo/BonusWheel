using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SectionItem : UIBase
{
    [SerializeField]
    private Image m_imgIcon;
    [SerializeField]
    private Text m_txtName;
    [SerializeField]
    private Text m_txtSecName;
    protected override void Awake()
    {
        base.Awake();
        sign = "SectionItem";
    }

    public override void Render()
    {
        base.Render();
        RenderIcon();
        RenderName();
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
        m_txtSecName.text = m_resData.Cols[1];
    }
}
