using System;
using Script;
using Script.Utilitaire.Moteur;
using UnityEngine;

public class Contrôleur : MonoBehaviour {
    [SerializeField] bool EstTroisD { get; set; } = false;

    private float rotationX = 0f;
    private float rotationY = 0f;

    public static bool JeuxEnPause = false;
    private void Start() {
        Caméra.ÉtatCaméra = EstTroisD ? ÉtatCaméra.TROISIÈME : ÉtatCaméra.PREMIÈRE;
        RegistreArticle.Initialisation();
        RegistreEntités.Initialisation();
        new Lumière().EnsembleLumière();
        new Son().EnsembleSonore();
    }

    private void Update() {
        Caméra.MettreAJourCamera(new Caméra(1f), Partageur.Joueur.transform.position, ref rotationX, ref rotationY);
    }

    private void LateUpdate() {
        Navigation.Initialisation();
    }
    
}
