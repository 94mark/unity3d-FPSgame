using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Cinemachine;

public class DirectorAction : MonoBehaviour
{
    PlayableDirector pd; //���� ������Ʈ
    public Camera targetCam;

    void Start()
    {
        //Director ������Ʈ�� ���� �ִ� PlayableDirector ������Ʈ�� ������
        pd = GetComponent<PlayableDirector>();
        //Ÿ�Ӷ��� ����
        pd.Play();
    }

    void Update()
    {
        //���� �������� �ð��� ��ü �ð��� ũ�ų� ������(��� �ð��� �� �Ǹ�)
        if(pd.time >= pd.duration)
        {
            //���� ���� ī�޶� Ÿ�� ī�޶�(�ó׸ӽ� ī�޶�)���
            //��� �ϱ� ���� ���׸ӽ� �극���� ��Ȱ��ȭ
            if(Camera.main == targetCam)
            {
                targetCam.GetComponent<CinemachineBrain>().enabled = false;
            }
            //���׸ӽſ� ����� ī�޶� ��Ȱ��ȭ
            targetCam.gameObject.SetActive(false);

            //Director �ڽ��� ��Ȱ��ȭ
            gameObject.SetActive(false);
        }
    }
}