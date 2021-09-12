using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBase : MonoBehaviour
{
    protected string sign;
    protected ResData m_resData;
    protected virtual void Awake() { }
    public virtual string GetSign() { return sign; }

    public virtual void InitData(ResData _data) { m_resData = _data; }

    public virtual void Render() { }
    public virtual void DestroyUI() {
        UIManager.Instance.RemoveUI(this.gameObject);
    }

    public Sprite LoadSourceSprite(string relativePath)
    {
        Texture2D tex = (Texture2D)Resources.Load(relativePath);
        Object Preb = Resources.Load(relativePath, typeof(Sprite));
        Sprite tmpsprite = null;
        try
        {
            tmpsprite = Instantiate(Preb) as Sprite;
        }
        catch (System.Exception ex)
        {

        }
        return tmpsprite;
    }

    public void SetRotate(float _angle)
    {
        this.transform.localEulerAngles = new Vector3(0, 0, _angle);
    }
}
