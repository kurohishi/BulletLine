using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChanonController : MonoBehaviour
{
    #region 銃弾
    [SerializeField] private GameObject _bulletPrefab; // 弾オブジェクト
    public float bulletVellocity = 20.0f; // 弾の速度
    #endregion
    void Update()
    {
        HandleControls(); // 弾挙動
        Movecon(); // 移動挙動
    }
    void FixedUpdate()
    {
        CameraCon(); // カメラ挙動
    }
    private void HandleControls()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.F)) // 左クリックかFキーで弾を発射する
        {
            var bullet = Instantiate(_bulletPrefab, transform.position -Vector3.up, Quaternion.identity); // 弾を生成する
            bullet.GetComponent<Rigidbody>().velocity = bulletVellocity * transform.forward; // 弾を前方に発射する
            Destroy(bullet, 10.0f); // １０秒後に弾を消す
        }
    }
    public float moveSpeed = 1.0f; // 移動速度
    void Movecon() // WASDキーでの移動操作
    {
        float xMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime; // 左右の移動
        float zMovement = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime; // 前後の移動
        transform.Translate(xMovement, 0, zMovement); // オブジェクトの位置を更新
    }
    void CameraCon()// マウスカーソルでのカメラ操作
    {
        float mx = Input.GetAxis("Mouse X"); //カーソルの横の移動量を取得
        float my = Input.GetAxis("Mouse Y"); //カーソルの縦の移動量を取得
        if (Mathf.Abs(mx) > 0.001f) // X方向に一定量移動していれば横回転
        {
            transform.RotateAround(transform.position, Vector3.up, mx); // 回転軸はワールド座標Y軸
        }
        if (Mathf.Abs(my) > 0.001f)// Y方向に一定量移動していれば縦回転
        {
            transform.RotateAround(transform.position, transform.right, -my); // 回転軸はローカル座標X軸
        }
    }
}
