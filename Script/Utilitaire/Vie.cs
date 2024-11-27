using Script.Moteur;
using Script.Utilitaire.Moteur;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace Script.Utilitaire {
    public class Vie {
        public int Saturation { get; set; }
        public int Chance { get; set; }
        public bool EstIntuable { get; set; } = false;
        public Vie() {
            Saturation = 100;
            Chance = 3;
        }
        public Vie(int saturation, int chance) {
            Saturation = saturation;
            Chance = chance;
        }
        public Vie(int saturation) {
            Saturation = saturation;
            Chance = 0;
        }
        public static void PrendreDommage(Entité attaquant, Entité victime, int valeur) {
            if (victime.Vie.EstIntuable) {return;}
            victime.Vie.Saturation -= valeur;
            if (victime.Vie.Saturation <= 0) victime.Vie.RetirerUneVie();
            if (victime.Vie.Chance <= 0) Tuer(victime);
        }
        private void RetirerUneVie() {
            Chance--;
            Saturation = 100;
        }
        private static void Tuer(Entité victime) {
            if (victime == Partageur.Joueur) {
                GameObject.Destroy(victime.gameObject);
                SceneManager.LoadScene("Scenes/Mort");
            } else {
                GameObject.Destroy(GameObject.Find(victime.name));
                Expérience.PrendreNiveau(Partageur.Joueur,Expérience.JetterNiveau(victime));
                if (victime.name.Contains("Méchant")) {
                    Spawneur.Méchants--;
                }
            }
        }
    }
}