using UnityEngine;
using Zenject;

public class ToolbarUI : MonoBehaviour, IToolbarUI
{
    private IPauseMenuUI _pauseMenuUI;

    [Inject]
    private void Construct(IPauseMenuUI pauseMenuUI)
    {
        _pauseMenuUI = pauseMenuUI;
    }

    public void Pause()
    {
        _pauseMenuUI.ShowPauseMenu();
    }
}
