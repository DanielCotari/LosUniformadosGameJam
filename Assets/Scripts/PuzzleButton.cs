using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class PuzzleButton : MonoBehaviour
{
    public int buttonIndex;
    public PuzzleManager puzzleManager;
    private Button uiButton;

    void Start()
    {
        uiButton = GetComponent<Button>();
        if (puzzleManager == null)
            puzzleManager = GetComponentInParent<PuzzleManager>();

        uiButton.onClick.AddListener(OnButtonPressed);
    }

    void OnButtonPressed()
    {
        if (puzzleManager != null)
            puzzleManager.PressButton(buttonIndex);
    }
}
