using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//�ݵ�� �ʿ��� ������Ʈ�� ����� �ش� ������Ʈ�� �����Ǵ� ���� �����ϴ� ��Ʈ����Ʈ
[RequireComponent(typeof(AudioSource))]
public class FireCtrl : MonoBehaviour
{
    //�Ѿ� ������
    public GameObject bullet;
    //�Ѿ� �߻� ��ǥ
    public Transform firePos;
    //�ѼҸ��� ����� ����� ����
    public AudioClip fireSfx;

    // AudioSource ������Ʈ�� ������ ����
    private new AudioSource audio;
    //Muzzle Flash�� MeshRenderer ������Ʈ
    private MeshRenderer muzzleFlash;

    //Raycast �ᱣ���� �����ϱ� ���� ����ü ����
    private RaycastHit hit;


    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();

        //FirePos ������ �ִ� MuzzleFlash�� Material ������Ʈ�� ����
        muzzleFlash = firePos.GetComponentInChildren<MeshRenderer>();
        // ó�� ������ �� ��Ȱ��ȭ
        muzzleFlash.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Ray�� �ð������� ǥ���ϱ� ���� ���
        Debug.DrawRay(firePos.position, firePos.forward * 10.0f, Color.green);

        //���콺 ���� ��ư�� Ŭ������ �� Fire �Լ� ȣ��
        if(Input.GetMouseButtonDown(0))
        {
            Fire();

            //Ray�� �߻�
            //RayCast(������ ����, ������ ����, ������ ���� ��� ������, ������ �Ÿ�, �����ϴ� ���̾�)
            if (Physics.Raycast(firePos.position, firePos.forward, out hit, 10.0f, 1 << 6))
            {
                Debug.Log($"Hit={hit.transform.name}");
                hit.transform.GetComponent<MonsterCtrl>()?.OnDamage(hit.point, hit.normal);
            }
        }
        
    }

    void Fire()
    {
        //Bullet �������� �������� ����(������ ��ü, ��ġ, ȸ��)
        Instantiate(bullet, firePos.position, firePos.rotation);
        //�ѼҸ� �߻�
        audio.PlayOneShot(fireSfx, 1.0f);
        //�ѱ� ȭ�� ȿ�� �ڷ�ƾ �Լ� ȣ��\
        StartCoroutine(ShowMuzzleFlash());
    }

    IEnumerator ShowMuzzleFlash()
    {
        //������ ��ǩ���� �����Լ��� ����
        Vector2 offset = new Vector2(Random.Range(0, 2), Random.Range(0, 2)) * 0.5f;
        //�ؽ�ó�� ������ �� ����
        muzzleFlash.material.mainTextureOffset = offset;

        //MuzzleFlash�� ȸ�� ����
        float angle = Random.Range(0, 360);
        muzzleFlash.transform.localRotation = Quaternion.Euler(0, 0, angle);

        //MuzzleFlash�� ũ�� ����
        float scale = Random.Range(1.0f, 2.0f);
        muzzleFlash.transform.localScale = Vector3.one * scale;

        //MuzzleFlash Ȱ��ȭ
        muzzleFlash.enabled = true;

        //0.2�� ���� ���(����)�ϴ� ���� �޽��� ������ ������� �纸
        yield return new WaitForSeconds(0.2f);

        //MuzzleFlash ��Ȱ��ȭ
        muzzleFlash.enabled = false;
    }

}
