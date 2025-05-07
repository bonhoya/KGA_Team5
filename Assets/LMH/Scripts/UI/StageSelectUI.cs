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

    [Header("SFX Setting")]
    [SerializeField] private AudioClip SelectMenuButtonClip;

    private void Awake()
    {
        settingUIPanel.SetActive(false);

        // 저장된 볼륨 설정을 다른곳에서도 유지할 수 있도록 하는 코드
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
        SoundsManager.Instance.SFXPlay("Select", SelectMenuButtonClip);
        settingUIPanel.SetActive(true);
    }

   public void Resume()
    {
        SoundsManager.Instance.SFXPlay("Select", SelectMenuButtonClip);
        settingUIPanel.SetActive(false);
    }

    public void Quit()
    {
        SoundsManager.Instance.SFXPlay("Select", SelectMenuButtonClip);
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
