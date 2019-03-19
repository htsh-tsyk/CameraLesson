using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode] 
public class ObliqueProection : MonoBehaviour {

    [Range(-1f,1f)] public float horizontal;
    [Range(-1f,1f)] public float vertical;
    public float distance;
    private Vector3 org_position_;

    void OnEnable()
   {
       EditorApplication.update += EditorUpdate;
       org_position_ = transform.localPosition;
   }

   void OnDisable()
   {
       EditorApplication.update -= EditorUpdate;
   }

    void EditorUpdate ()
    {
        var cam = GetComponent<Camera>();
        var proj = cam.projectionMatrix;
        proj.m02 = horizontal;
        proj.m12 = vertical;
        cam.projectionMatrix = proj;        
        var tan = Mathf.Tan(cam.fieldOfView*0.5f*Mathf.Deg2Rad);
        var rect = cam.pixelRect;
        var ratio = (float)rect.width/(float)rect.height;
        transform.localPosition = org_position_ + new Vector3(-horizontal*distance*tan*ratio, -vertical*distance*tan, 0f);
    }
}
