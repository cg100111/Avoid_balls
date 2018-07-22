using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

public class FirstPersonCameraController : MonoBehaviour {

    private Vector3 base_position;

	// Use this for initialization
	void Start () {
        // TODO: ここで固定したい位置があれば指定しておく
        base_position = Vector3.zero;
    }

    // Update is called once per frame
    void Update () {
        // VR.InputTracking から hmd の位置を取得
        Vector3 trackingPos = InputTracking.GetLocalPosition(VRNode.CenterEye);
        // 固定したい位置から hmd の位置を
        // 差し引いて実質 hmd の移動を無効化する
        transform.localPosition = base_position - trackingPos;
    }
}
