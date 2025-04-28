using TMPro;
using UnityEngine;

public class Intro : MonoBehaviour
{
    private float timer;
    public TextMeshProUGUI text;
    [SerializeField] Camera cam1;
    [SerializeField] Camera cam2;
    [SerializeField] Camera cam3;
    public GameObject soldiers;
    public GameObject warpGate;
    public GameObject warpGateAppear;


    private void Start()
    {
        timer = 0;
        cam1.gameObject.SetActive(true);
        cam2.gameObject.SetActive(false);
        cam3.gameObject.SetActive(false);
        warpGate.gameObject.SetActive(false);
        warpGateAppear.gameObject.SetActive(false);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        T();
    }

    void T()
    {
        if (timer > 15)
        {
            //다음씬으로
        }
        if (timer > 10)
        {
            cam2.gameObject.SetActive(false);
            cam3.gameObject.SetActive(true);
            text.text = "타워를 건설해서 마을을 지켜야 해요";
        }
        else if (timer >= 5)
        {
            cam1.gameObject.SetActive(false);
            cam2.gameObject.SetActive(true);
            text.text = "그런데 갑자기 워프게이트가 생겨서 마물이 나오기 시작했어요";
            if (timer >= 6)
            {
                warpGate.SetActive(true);
                warpGateAppear.SetActive(true);
            }
        }
        else
        {
            text.text = "마을 사람들은 평화롭게 살고 있었어요";
            Jump(1);
        }
    }
    void Jump(float time)
    {
        soldiers.transform.position = new Vector3(soldiers.transform.position.x,Mathf.Max(0,-5 * (timer - time) * (timer - time - 0.5f)),soldiers.transform.position.z);
    }

}
