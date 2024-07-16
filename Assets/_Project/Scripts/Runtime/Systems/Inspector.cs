using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inspector : MonoBehaviour
{
    public GameObject gObject;
    public float speed;

    public GameObject itemView;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            itemView.SetActive(!itemView.activeSelf);
        }

        if (Input.GetMouseButton(0))
        {
            float x = Input.GetAxis("Mouse X");
            float y = Input.GetAxis("Mouse Y");

            gObject.transform.Rotate(Vector3.up, -x * speed);
            gObject.transform.Rotate(Vector3.right, y * speed);
        }

        if (Input.GetMouseButtonUp(0))
        {
            gObject.transform.rotation = Quaternion.identity;
        }
    }
}
