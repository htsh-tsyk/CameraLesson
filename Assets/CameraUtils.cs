using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[RequireComponent(typeof (Camera))]
public class CameraUtils : MonoBehaviour {
    [SerializeField, Range(-0.05f, 0.05f)]
    private float offsetX;
    void LateUpdate() {
        Camera cam = gameObject.GetComponent<Camera>();
        SetVanishingPoint (Vector2.right * offsetX, cam);
    }

    static public Matrix4x4 PerspectiveOffCenter(float left, float right, float bottom, float top, float near, float far){
        float x =  (2f * near)      / (right - left);
        float y =  (2f * near)      / (top - bottom);
        float a =  (right + left)   / (right - left);
        float b =  (top + bottom)   / (top - bottom);
        float c = -(far + near)     / (far - near);
        float d = -(2f * far * near)/ (far - near);
        float e = -1f;
        Matrix4x4 m = new Matrix4x4();
        m[0,0] =  x;  m[0,1] = 0f;  m[0,2] = a;   m[0,3] = 0f;
        m[1,0] = 0f;  m[1,1] =  y;  m[1,2] = b;   m[1,3] = 0f;
        m[2,0] = 0f;  m[2,1] = 0f;  m[2,2] = c;   m[2,3] =  d;
        m[3,0] = 0f;  m[3,1] = 0f;  m[3,2] = e;   m[3,3] = 0f;
        return m;
    }

    static public void SetVanishingPoint(Vector2 perspectiveOffset,Camera cam){
        cam.ResetProjectionMatrix();
        Matrix4x4 m = cam.projectionMatrix;
        float w = 2f*cam.nearClipPlane/m.m00;
        float h = 2f*cam.nearClipPlane/m.m11;
        float left = -w/2f - perspectiveOffset.x;
        float right = left+w;
        float bottom = -h/2f - perspectiveOffset.y;
        float  top = bottom+h;
        cam.projectionMatrix = PerspectiveOffCenter(left, right, bottom, top, cam.nearClipPlane, cam.farClipPlane);
    }
}
