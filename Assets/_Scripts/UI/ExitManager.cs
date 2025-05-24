using UnityEngine;

public class ExitManager : MonoBehaviour
{
    [SerializeField] private GameObject _exitScreen;
    [SerializeField] private InputReaderSO _inputReaderSO;

    private bool _isExitScreenShown = false;

    private void Start() 
    {
        HideExitScreen();

        _inputReaderSO.OnEscapeEvent += Toggle;
    }

    private void Toggle()
    {
        _isExitScreenShown = !_isExitScreenShown;

        if (!_isExitScreenShown) HideExitScreen();
        if (_isExitScreenShown) ShowExitScreen();
    }

    public void ShowExitScreen() => _exitScreen.SetActive(true);
    public void HideExitScreen() => _exitScreen.SetActive(false);

    public void Exit() => Application.Quit(); 
}