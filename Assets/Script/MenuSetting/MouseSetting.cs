using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseSetting : MonoBehaviour
{
    Vector2 targetPos;
    float rotateSpeed;
    private bool mouseShouldVisible;
    void Start()
    {
        Cursor.visible = false;
        rotateSpeed = 350;
    }

    // Update is called once per frame
    void Update()
    {
        if(!UIMouseAndPriority.Instance.CanOpenThisUI() || mouseShouldVisible)//Mean one of the UI is Now open
        {
            Cursor.visible = true;
            return;
        }
        Cursor.visible = false;
        targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = targetPos;
        transform.Rotate(0, 0, rotateSpeed*Time.deltaTime);
    }
    public void SetMouseShouldVisible(bool newValue)
    {
        mouseShouldVisible = newValue;
    }
}
