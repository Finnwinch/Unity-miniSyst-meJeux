using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Mort : MonoBehaviour {
    private VisualElement root;
    private Button retryButton;

    private void OnEnable() {
        var uiDocument = GetComponent<UIDocument>();
        root = uiDocument.rootVisualElement;
        retryButton = root.Q<Button>("retry-button");
        retryButton.clicked += OnRetryButtonClicked;
    }


    private void OnDisable() {
        retryButton.clicked -= OnRetryButtonClicked;
    }

    private void OnRetryButtonClicked() {
        SceneManager.LoadScene("Scenes/Jeux");
    }
}