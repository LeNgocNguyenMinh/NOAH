
using UnityEngine;


public class ButtonEnum : MonoBehaviour
{
    public enum ButtonType
    {
        ResumeBtn,
        SettingBtn,
        MainMenuBtn,
        MainMenuConfirm,
        QuitBtn,
        QuitConfirm,
        BackToPauseMenu
    }

    public ButtonType buttonType;
}
