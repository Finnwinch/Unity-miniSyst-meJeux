using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement; // Optionnel si vous voulez charger une autre scène

public class INSTRUCTION : MonoBehaviour
{
    private VisualElement root;
    private Button nextButton;

    private void OnEnable()
    {
        // Récupère la racine de l'UI Toolkit
        var uiDocument = GetComponent<UIDocument>();
        root = uiDocument.rootVisualElement;

        // Récupère le bouton "Next"
        nextButton = root.Q<Button>("next-button");
        nextButton.clicked += OnNextButtonClicked;
    }

    private void OnDisable()
    {
        // Nettoie les événements
        nextButton.clicked -= OnNextButtonClicked;
    }

    private void OnNextButtonClicked() {
        SceneManager.LoadScene("Scenes/Jeux");
    }
}