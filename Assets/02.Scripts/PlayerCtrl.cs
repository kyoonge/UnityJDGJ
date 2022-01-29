using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{

    // ������Ʈ�� ĳ�� ó���� ����
    private Transform tr;
    // Animation ������Ʈ�� ������ ����
    private Animation anim;
    // �̵� �ӵ� ���� ( public���� ����Ǿ� �ν����� �信 ����� )
    public float moveSpeed = 10.0f;
    // ȸ�� �ӵ� ����
    public float turnSpeed = 80.0f;


    // Start is called before the first frame update
    void Start()
    {
        // Transform ������Ʈ�� ������ ������ ����
        tr = GetComponent<Transform>();  // �Լ��� < ����(������Ŭ����) > ( �Ű����� )
        anim = GetComponent<Animation>();

        // �ִϸ��̼� ����
        anim.Play("Idle");
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal"); //-1.0f ~ 0.0f ~ +1.0f
        float v = Input.GetAxis("Vertical"); //-1.0f ~ 0.0f ~ +1.0f
        float r = Input.GetAxis("Mouse X"); // ���콺 ����: ���� , ������: ��� ��ȯ

        //Debug.Log("h=" + h);
        //Debug.Log("v=" + v);

        //�����¿� �̵� ���� ���� ���
        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);

        //Trasnsform ������Ʈ�� ��ġ�� ����
        // transform.position += new Vector3(0,0,1);

        //����ȭ ���͸� ����� �ڵ�
        //transform.position += Vector3.forward * 1;  // ( �������� ) * ( �ӷ� )

        // Translate �Լ��� ����� �̵� ����
        tr.Translate(moveDir.normalized * Time.deltaTime * moveSpeed ); // ( ���� ) * deltaTime * {����/����} * ( �ӵ� ) 

        // Vector3.up ���� �������� turnSpeed��ŭ�� �ӵ��� ȸ��
        tr.Rotate(Vector3.up * turnSpeed * Time.deltaTime * r); // (ȸ����ǥ��) * (ȸ���ӵ�) * Time.deltaTime * (�����Է°�)

        // ���ΰ� ĳ������ �ִϸ��̼� ����
        PlayerAnim(h, v);

    }

    void PlayerAnim(float h, float v)
    {
        // Ű���� �Է°��� ���� ������ �ִϸ��̼� ����

        if (v >= 0.1f)
        {
            anim.CrossFade("RunF", 0.25f); // ���� �ִϸ��̼� ����
        }
        else if (v <= -0.1f)
        {
            anim.CrossFade("RunB", 0.25f); // ���� �ִϸ��̼� ����
        }
        else if (h >= 0.1f)
        {
            anim.CrossFade("RunR", 0.25f); // ���� �ִϸ��̼� ����
        }
        else if (h <= -0.1f)
        {
            anim.CrossFade("RunL", 0.25f); // ���� �ִϸ��̼� ����
        }
        else
        {
            anim.CrossFade("Idle", 0.25f);
        }
    }
}