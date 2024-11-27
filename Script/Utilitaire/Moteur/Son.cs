using UnityEngine;

namespace Script.Utilitaire.Moteur {
    public struct Son {
        public float x;
        public float y;
        public float z;
        public float radius;
        public AudioClip musique;
        public void CréerSpotSonore() {
            GameObject soundObject = new GameObject("Spot Sonore");
            AudioSource audioSource = soundObject.AddComponent<AudioSource>();
            audioSource.clip = musique;
            audioSource.loop = true;
            audioSource.spatialBlend = 1f;
            audioSource.rolloffMode = AudioRolloffMode.Linear;
            audioSource.maxDistance = radius;
            audioSource.playOnAwake = true;
            soundObject.transform.position = new Vector3(x, y, z);
            audioSource.Play();
        }
        public void EnsembleSonore() {
            Son[] sons = new Son[] {
                new Son { x = 16.6f, y = 0.15f, z = 16.9f, radius = 10f, musique = Resources.Load<AudioClip>("Audio/Royalty Free Music - Action Dramatic Trailer Hybrid Cinematic Epic Powerful Intense Background") },
                //new Son { x = 19.13f, y = 3.69f, z = 19.38f, radius = 15f, musique = Resources.Load<AudioClip>("Musique2") },
                //new Son { x = 10.4f, y = 1.3f, z = 34.1f, radius = 20f, musique = Resources.Load<AudioClip>("Musique3") }
            };
            foreach (Son son in sons) {
                son.CréerSpotSonore();
            }
        }
    }
}
