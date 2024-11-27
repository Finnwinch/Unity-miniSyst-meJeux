using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Menu : MonoBehaviour
{
    private VisualElement root;
    private Button playButton;
    private Button creditsButton;
    private VisualElement creditsPopup;
    private Button closeCreditsButton;

    private void OnEnable()
    {
        // Charge les éléments UI
        var uiDocument = GetComponent<UIDocument>();
        root = uiDocument.rootVisualElement;

        // Récupération des boutons et de la pop-up
        playButton = root.Q<Button>("play-button");
        creditsButton = root.Q<Button>("credits-button");
        creditsPopup = root.Q<VisualElement>("credits-popup");
        closeCreditsButton = root.Q<Button>("close-credits-button");

        // Ajout des événements
        playButton.clicked += OnPlayButtonClicked;
        creditsButton.clicked += OnCreditsButtonClicked;
        closeCreditsButton.clicked += OnCloseCreditsButtonClicked;
    }

    private void OnDisable()
    {
        // Nettoie les événements pour éviter les fuites de mémoire
        playButton.clicked -= OnPlayButtonClicked;
        creditsButton.clicked -= OnCreditsButtonClicked;
        closeCreditsButton.clicked -= OnCloseCreditsButtonClicked;
    }

    private void OnPlayButtonClicked()
    {
        SceneManager.LoadScene("Scenes/Instruction");
    }

    private void OnCreditsButtonClicked()
    {
        Debug.Log("Affichage des crédits...");
        creditsPopup.style.display = DisplayStyle.Flex;
    }

    private void OnCloseCreditsButtonClicked()
    {
        Debug.Log("Fermeture des crédits...");
        creditsPopup.style.display = DisplayStyle.None;
    }
}