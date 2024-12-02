using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChanonController : MonoBehaviour
{
    #region �e�e
    [SerializeField] private GameObject _bulletPrefab; // �e�I�u�W�F�N�g
    public float bulletVellocity = 20.0f; // �e�̑��x
    #endregion
    void Update()
    {
        HandleControls(); // �e����
        Movecon(); // �ړ�����
    }
    void FixedUpdate()
    {
        CameraCon(); // �J��������
    }
    private void HandleControls()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.F)) // ���N���b�N��F�L�[�Œe�𔭎˂���
        {
            var bullet = Instantiate(_bulletPrefab, transform.position -Vector3.up, Quaternion.identity); // �e�𐶐�����
            bullet.GetComponent<Rigidbody>().velocity = bulletVellocity * transform.forward; // �e��O���ɔ��˂���
            Destroy(bullet, 10.0f); // �P�O�b��ɒe������
        }
    }
    public float moveSpeed = 1.0f; // �ړ����x
    void Movecon() // WASD�L�[�ł̈ړ�����
    {
        float xMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime; // ���E�̈ړ�
        float zMovement = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime; // �O��̈ړ�
        transform.Translate(xMovement, 0, zMovement); // �I�u�W�F�N�g�̈ʒu���X�V
    }
    void CameraCon()// �}�E�X�J�[�\���ł̃J��������
    {
        float mx = Input.GetAxis("Mouse X"); //�J�[�\���̉��̈ړ��ʂ��擾
        float my = Input.GetAxis("Mouse Y"); //�J�[�\���̏c�̈ړ��ʂ��擾
        if (Mathf.Abs(mx) > 0.001f) // X�����Ɉ��ʈړ����Ă���Ή���]
        {
            transform.RotateAround(transform.position, Vector3.up, mx); // ��]���̓��[���h���WY��
        }
        if (Mathf.Abs(my) > 0.001f)// Y�����Ɉ��ʈړ����Ă���Ώc��]
        {
            transform.RotateAround(transform.position, transform.right, -my); // ��]���̓��[�J�����WX��
        }
    }
}
