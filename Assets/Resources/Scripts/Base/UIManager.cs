using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MSingleton<UIManager>
{
    public GameObject uiCanvas;
    public Button m_replayBtn;
    protected bool _initialized = false;

    protected Dictionary<int, GameObject> uiCacheDictionary;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    protected override void Awake()
    {
        base.Awake();

        Initialization();
    }

    protected virtual void Initialization()
    {
        if (_initialized)
        {
            return;
        }
        uiCacheDictionary = new Dictionary<int, GameObject>();
        m_replayBtn.onClick.AddListener(delegate ()
        {
            Replay();
        });
        _initialized = true;
    }
    public UIBase CreateUI(string _sign, Transform _parent)
    {
        GameObject tmpPrefab = (GameObject)Resources.Load("Prefabs/" + _sign);

        GameObject tmpObj = Instantiate(tmpPrefab);
        tmpObj.transform.SetParent(_parent);
        InserGameObject(tmpObj);
        return tmpObj.GetComponent<UIBase>();
    }

    public void InserGameObject(GameObject _obj)
    {
        if (_obj == null)
        {
            return;
        }
        uiCacheDictionary.Add( _obj.GetInstanceID(), _obj);
    }

    public bool RemoveUI(GameObject _gameObject)
    {
        if (_gameObject != null)
        {
            int id = _gameObject.GetInstanceID();
            if (uiCacheDictionary.ContainsKey(id))
            {
                uiCacheDictionary.Remove(id);
                return true;
            }
        }
        return false;
    }
    private void Replay()
    {
        foreach (var item in uiCacheDictionary)
        {
            Destroy(item.Value);
        }
        uiCacheDictionary.Clear();
        CreateUI("Wheel", uiCanvas.transform);
    }
}
