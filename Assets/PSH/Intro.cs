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
            //����������
        }
        if (timer > 10)
        {
            cam2.gameObject.SetActive(false);
            cam3.gameObject.SetActive(true);
            text.text = "Ÿ���� �Ǽ��ؼ� ������ ���Ѿ� �ؿ�";
        }
        else if (timer >= 5)
        {
            cam1.gameObject.SetActive(false);
            cam2.gameObject.SetActive(true);
            text.text = "�׷��� ���ڱ� ��������Ʈ�� ���ܼ� ������ ������ �����߾��";
            if (timer >= 6)
            {
                warpGate.SetActive(true);
                warpGateAppear.SetActive(true);
            }
        }
        else
        {
            text.text = "���� ������� ��ȭ�Ӱ� ��� �־����";
            Jump(1);
        }
    }
    void Jump(float time)
    {
        soldiers.transform.position = new Vector3(soldiers.transform.position.x,Mathf.Max(0,-5 * (timer - time) * (timer - time - 0.5f)),soldiers.transform.position.z);
    }

}
