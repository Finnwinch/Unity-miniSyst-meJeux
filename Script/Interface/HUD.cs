using System;
using Script.Utilitaire;
using UnityEngine;
using UnityEngine.UIElements;

namespace Script {
    public class HUD : MonoBehaviour {
        private VisualElement _root;
        private ProgressBar enduranceBar;
        private float enduranceTarget;
        private ProgressBar vieBar;
        private float vieTarget;
        private ProgressBar experienceBar;
        private float experienceTarget;
        private Label experienceText;

        private Image modelImage;
        private Label modelName;

        protected void OnEnable() {
            var uiDocument = GetComponent<UIDocument>();
            _root = uiDocument.rootVisualElement;

            enduranceBar = _root.Q<ProgressBar>("Saturation");
            vieBar = _root.Q<ProgressBar>("Vie");
            experienceBar = _root.Q<ProgressBar>("Experience");
            experienceText = _root.Q<Label>("ExperienceTitre");
            
            var modelContainer = _root.Q<VisualElement>("model-container");
            modelImage = modelContainer.Q<Image>("model-image");
            modelName = modelContainer.Q<Label>("model-name");
        }

        protected void Update() {
            if (Partageur.Joueur == null) return;

            experienceText.text = "Niveau: " + Partageur.Joueur.Expérience.Niveau;

            enduranceTarget = Partageur.Joueur.Endurance.Saturation * 100;
            enduranceBar.value = Mathf.Lerp(enduranceBar.value, enduranceTarget, Time.deltaTime * 5f);

            vieTarget = Partageur.Joueur.Vie.Saturation;
            vieBar.value = Mathf.Lerp(vieBar.value, vieTarget, Time.deltaTime * 5f);

            experienceTarget = Partageur.Joueur.Expérience.Quantité * 100;
            experienceBar.value = Mathf.Lerp(experienceBar.value, experienceTarget, Time.deltaTime * 5f);
            
            modelImage.sprite = SacÀDos.ObtenirArticle(Partageur.Joueur)?.Model;
            modelName.text = SacÀDos.ObtenirArticle(Partageur.Joueur)?.Nom;
        }
    }
}
