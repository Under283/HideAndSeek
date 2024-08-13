using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{

    [SerializeField] float move_speed = 5f;
    [SerializeField] float jump_force = 3f; // ������ ����� ��
    [SerializeField] Camera Main_Camera; // ���� ī�޶� �����ϱ� ���� ����
    
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform anchor_transform;

    void Start()
    {
        // Rigidbody ������Ʈ�� ��������
        rb = GetComponent<Rigidbody>();
        anchor_transform = transform.Find("Root_Anchor");
    }

    // Update is called once per frame
    void Update()
    {

        float moveX = Input.GetAxis("Horizontal") * move_speed * Time.deltaTime;
        float moveZ = Input.GetAxis("Vertical") * move_speed * Time.deltaTime;

        // �̵� ����
        transform.Translate(moveX, 0, moveZ);

        // ���콺 ��ġ�� ���� ��ǥ�� ��ȯ
        //Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        //if (Physics.Raycast(ray, out RaycastHit hit)) {
        //    // ���콺�� ����Ű�� ��ġ�� XZ ��� ��ǥ�� ���
        //    Vector3 targetPosition = hit.point;
        //    targetPosition.y = transform.position.y; // Y�� ����
        //
        //    // �÷��̾ ���콺�� �ٶ󺸵��� ȸ�� (XZ ��鸸)
        //    Vector3 direction = (targetPosition - transform.position).normalized;
        //    Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        //    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime);
        //}

        float cursor_h = Input.GetAxis("Mouse X") * 1.5f;
        float cursor_v = Input.GetAxis("Mouse Y") * 1.5f;

        anchor_transform.Rotate(new Vector3(-cursor_v, cursor_h, 0f));
        Main_Camera.transform.position = anchor_transform.position + anchor_transform.forward * -7f;
        Main_Camera.transform.LookAt(anchor_transform.position);

        Quaternion anchor_rotation = anchor_transform.rotation;

        transform.rotation = Quaternion.Euler(0f, anchor_transform.rotation.eulerAngles.y, 0f);

        anchor_transform.rotation = anchor_rotation;

        // �����̽� �ٸ� ������ �� ����
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jump_force, ForceMode.Impulse);
        }

        if (Input.GetMouseButton(0))
        {
            On_Click();
        }
    }

    private void On_Click()
    {
        RaycastHit[] ray_hits = Physics.RaycastAll(Main_Camera.transform.position, Main_Camera.transform.forward, 10f);
        for(int i = 0; i < ray_hits.Length; i++)
        {
            Debug.Log($"{ray_hits[i].collider.gameObject.name}");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(anchor_transform.position, anchor_transform.forward * 10f);
    }
}
