using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    private Dictionary<string, GameObject> uiDic = new Dictionary<string, GameObject>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        GameObject mainScreenUI = GameObject.Find("MainMenuCanvas");
        SetUI("MainScreenUI", mainScreenUI);
        //Debug.Log("���� ���� ��ũ�� UI�� ����Ǿ����ϴ�.");
    }

    /// <summary>
    /// UI�� Dictionary�� ����
    /// </summary>
    /// <param name="key">UI�� ����� UI�̸�</param>
    /// <param name="uiObject">�����ϰ��� �ϴ� ���</param>
    public void SetUI(string key, GameObject uiObject)
    {
        if (uiDic.ContainsKey(key) == false)
        {
            uiDic.Add(key, uiObject);
        }
        else
            Debug.Log($"UIManager: ���� {key}�� �̹� ��ϵ� Ű �Դϴ�."); //
    }

    public GameObject GetUI(string key)
    {
        if (uiDic.TryGetValue(key, out GameObject uiObjcet))
        {
            return uiObjcet;
        }
        else
            return null;
    }

    /// <summary>
    /// UI�� ���̰�(Ȱ��ȭ) �ϱ�
    /// </summary>
    /// <param name="key">UI�� ����� UI�̸�</param>
    public void ShowUI(string key)
    {
        GameObject uiObject = GetUI(key);
        if(uiObject != null)
        {
            uiObject.SetActive(true);
        }
    }

    /// <summary>
    /// UI�� �Ⱥ��̰�(��Ȱ��ȭ) �ϱ�
    /// </summary>
    /// <param name="key">UI�� ����� UI�̸�</param>
    public void HideUI(string key)
    {
        GameObject uiObject = GetUI(key);
        if(uiObject != null)
        {
            uiObject.SetActive(false);
        }

    }
}
