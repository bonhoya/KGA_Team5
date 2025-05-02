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
        //Debug.Log("현재 메인 스크린 UI가 저장되었습니다.");
    }

    /// <summary>
    /// UI를 Dictionary에 저장
    /// </summary>
    /// <param name="key">UI에 저장될 UI이름</param>
    /// <param name="uiObject">저장하고자 하는 대상</param>
    public void SetUI(string key, GameObject uiObject)
    {
        if (uiDic.ContainsKey(key) == false)
        {
            uiDic.Add(key, uiObject);
        }
        else
            Debug.Log($"UIManager: 현재 {key}는 이미 등록된 키 입니다."); //
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
    /// UI를 보이게(활성화) 하기
    /// </summary>
    /// <param name="key">UI에 저장될 UI이름</param>
    public void ShowUI(string key)
    {
        GameObject uiObject = GetUI(key);
        if(uiObject != null)
        {
            uiObject.SetActive(true);
        }
    }

    /// <summary>
    /// UI를 안보이게(비활성화) 하기
    /// </summary>
    /// <param name="key">UI에 저장될 UI이름</param>
    public void HideUI(string key)
    {
        GameObject uiObject = GetUI(key);
        if(uiObject != null)
        {
            uiObject.SetActive(false);
        }
    }
}
