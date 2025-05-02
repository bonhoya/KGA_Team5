using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class StageSelectUI : MonoBehaviour
{

    [SerializeField] private GameObject settingUIPanel;

    [Header("Saved Volume_Value")]
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;

    private void Awake()
    {
        settingUIPanel.SetActive(false);

        if(bgmSlider != null)
        {
            bgmSlider.value = SoundsManager.Instance.bgmVolume;
            bgmSlider.onValueChanged.AddListener(StageSelectBGMController);
        }

        if(sfxSlider != null)
        {
            sfxSlider.value = SoundsManager.Instance.sfxVolume;
            sfxSlider.onValueChanged.AddListener(StageSelectSFXContoller);
        }
    }
    public void OpenSetting()
    {
        settingUIPanel.SetActive(true);
    }

   public void Resume()
    {
        settingUIPanel.SetActive(false);
    }

    public void Quit()
    {
        SceneChangeManager.Instance.ChangeScene("TestingMainScreen");
        settingUIPanel.SetActive(false);
    }

    public void StageSelectBGMController(float value)
    {
        SoundsManager.Instance.audioMixer.SetFloat("BGMParam", Mathf.Log10(value) * 20);
        SoundsManager.Instance.bgmVolume = value;
    }

    public void StageSelectSFXContoller(float value)
    {
        SoundsManager.Instance.audioMixer.SetFloat("SFXParam", Mathf.Log10(value) * 20);
        SoundsManager.Instance.sfxVolume = value;
    }
}
