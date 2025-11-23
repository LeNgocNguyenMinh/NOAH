
using UnityEngine;


public class ButtonEnum : MonoBehaviour
{
    public enum PauseMenuButtonType
    {
        ResumeBtn,
        SettingBtn,
        MainMenuBtn,
        MainMenuConfirm,
        QuitBtn,
        QuitConfirm,
        BackToPauseMenu,

    }
    /* public enum MainMenuButtonType
    {
        NewGameBtn,
        LoadBtn,
        SettingBtn,
        QuitBtn,
        NewGameConfirm,
        QuitConfirm,
    } */
    public PauseMenuButtonType pauseBtnType;
    /* public MainMenuButtonType mainMenuBtnType; */
}
