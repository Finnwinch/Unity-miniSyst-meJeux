using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Script.Moteur {
    public class FilsTraitement : MonoBehaviour {
        private static FilsTraitement _instance;
        public Dictionary<string, Coroutine> FilsExécution;
        private void Awake() {
            if (_instance == null) {
                _instance = this;
                //DontDestroyOnLoad(gameObject);
                FilsExécution = new Dictionary<string, Coroutine>();
            } else {
                Destroy(gameObject);
            }
        }
        public static bool VérifierFils(string nomFilExécution) {
            return _instance.FilsExécution.ContainsKey(nomFilExécution);
        }
        public static void DémarrerTraitement(float délaisItérationSecondes, Action traitement, string nomFilExécution) {
            if (_instance == null) {
                Debug.LogError("FilTraitement n'est pas initialisé. Assurez-vous qu'il est attaché à un GameObject dans la scène.");
                return;
            }
            _instance.DémarrerTraitementInterne(délaisItérationSecondes, traitement, nomFilExécution);
        }
        private void DémarrerTraitementInterne(float délaisItérationSecondes, Action traitement, string nomFilExécution) {
            if (FilsExécution.ContainsKey(nomFilExécution)) {
                Debug.LogWarning($"La coroutine {nomFilExécution} est déjà en cours. Veuillez patienter.");
                return;
            }
            Coroutine coroutine = StartCoroutine(ItérationFils(délaisItérationSecondes, traitement, nomFilExécution));
            FilsExécution[nomFilExécution] = coroutine;
        } 
        private IEnumerator ItérationFils(float délaisItérationSecondes, Action traitement, string nomFilExécution) {
            while (true) {
                traitement?.Invoke();
                yield return new WaitForSeconds(délaisItérationSecondes);
            }
        }
        public static void DémarrerTraitementAvecFin(float délaisItérationSecondes, Action traitement, string nomFilExécution, int nombreItérations) {
            if (_instance == null) {
                Debug.LogError("FilTraitement n'est pas initialisé. Assurez-vous qu'il est attaché à un GameObject dans la scène.");
                return;
            }
            _instance.DémarrerTraitementAvecFinInterne(délaisItérationSecondes, traitement, nomFilExécution, nombreItérations);
        }
        private void DémarrerTraitementAvecFinInterne(float délaisItérationSecondes, Action traitement, string nomFilExécution, int nombreItérations) {
            if (FilsExécution.ContainsKey(nomFilExécution)) {
                Debug.LogWarning($"La coroutine {nomFilExécution} est déjà en cours. Veuillez patienter.");
                return;
            }
            Coroutine coroutine = StartCoroutine(ItérationFilsAvecFin(délaisItérationSecondes, traitement, nomFilExécution, nombreItérations));
            FilsExécution[nomFilExécution] = coroutine;
        }
        private IEnumerator ItérationFilsAvecFin(float délaisItérationSecondes, Action traitement, string nomFilExécution, int nombreItérations) {
            for (int i = 0; i < nombreItérations; i++) {
                traitement?.Invoke();
                yield return new WaitForSeconds(délaisItérationSecondes);
            }
            ArrêterTraitement(nomFilExécution);
        }
        public static void ArrêterTraitement(string nomFilExécution) {
            if (_instance == null) {
                Debug.LogError("FilTraitement n'est pas initialisé. Assurez-vous qu'il est attaché à un GameObject dans la scène.");
                return;
            }
            _instance.ArrêterTraitementInterne(nomFilExécution);
        }
        private void ArrêterTraitementInterne(string nomFilExécution) {
            if (FilsExécution.TryGetValue(nomFilExécution, out Coroutine coroutine)) {
                StopCoroutine(coroutine);
                FilsExécution.Remove(nomFilExécution);
                Debug.Log($"La coroutine {nomFilExécution} a été arrêtée.");
            } else {
                Debug.LogWarning($"La coroutine {nomFilExécution} n'est pas en cours.");
            }
        }
    }
}
