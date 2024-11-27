using System.Collections.Generic;
using JetBrains.Annotations;
using Script.Moteur;
using UnityEngine;

namespace Script.Utilitaire {
    public class SacÀDos {
        public List<Article> articles { get; set; }
        public int Pointeur { get; set; }
        public SacÀDos() {
            articles = new List<Article>();
            Pointeur = 0;
        }
        private void Prochain() {
            if (articles.Count == 0) {
                Debug.LogWarning("La liste est vide. Impossible de naviguer.");
                return;
            }

            // Vérifier si la liste contient des éléments
            if (articles.Count > 0) {
                Pointeur = (Pointeur + 1) % articles.Count; // Incrémentation avec boucle
            }
        }

        private void Précédent() {
            if (articles.Count == 0) {
                Debug.LogWarning("La liste est vide. Impossible de naviguer.");
                return;
            }

            // Vérifier si la liste contient des éléments
            if (articles.Count > 0) {
                Pointeur = (Pointeur - 1 + articles.Count) % articles.Count; // Décrémentation avec boucle
            }
        }


        [CanBeNull] public Article Navigation(Entité entité, bool défilerVersLeHaut) { if (défilerVersLeHaut) { Prochain(); } else { Précédent(); } return ObtenirArticle(entité); }
        public static void PrendreArticle(Entité entité, Article article) {
            entité.SacÀDos.articles.Add(article);
        }
        public static void LâcherArticle(Entité entité, Article article) {
            entité.SacÀDos.articles.Remove(article);
        }
        [CanBeNull] public static Article ObtenirArticle(Entité entité) { return entité.SacÀDos.articles[entité.SacÀDos.Pointeur]; }
    }
}