namespace Assets.Scripts.GameUI
{
    public interface IPauseMenuUI
    {
        void Continue();
        void EndBattle();
        void HidePauseMenu();
        void Settings();
        void ShowPauseMenu();
    }
}