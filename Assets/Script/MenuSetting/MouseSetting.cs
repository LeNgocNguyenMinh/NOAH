using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseSetting : MonoBehaviour
{
    private UIMouseAndPriority uiMouseAndPriority;
    Vector2 targetPos;
    float rotateSpeed;
    private bool mouseShouldVisible;
    void Start()
    {
        uiMouseAndPriority = GameObject.FindObjectOfType<UIMouseAndPriority>().GetComponent<UIMouseAndPriority>();
        Cursor.visible = false;
        rotateSpeed = 350;
    }

    // Update is called once per frame
    void Update()
    {
        if(!uiMouseAndPriority.CanOpenThisUI() || mouseShouldVisible)//Mean one of the UI is Now open
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
