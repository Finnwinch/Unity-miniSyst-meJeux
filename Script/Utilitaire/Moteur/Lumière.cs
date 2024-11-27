using UnityEngine;

namespace Script.Utilitaire.Moteur {
    public struct Lumière {
        public float x;
        public float y;
        public float z;
        public int intensité;
        public Color couleur;
        public void CréerPointLumière() {
            GameObject lightObject = new GameObject("Point Light");
            Light lightComp = lightObject.AddComponent<Light>();
            lightComp.type = LightType.Point;
            lightObject.transform.position = new Vector3(x, y, z);
            lightComp.intensity = intensité;
            lightComp.color = couleur;
            lightComp.range = 10f;
        }
        public void EnsembleLumière() {
            Lumière[] lumières = new Lumière[] {
                new Lumière { x = 16.6f, y = 0.15f, z = 16.9f, intensité = 10, couleur = Color.red },
                //new Lumière { x = 19.13f, y = 3.69f, z = 19.38f, intensité = 1000, couleur = Color.green },
                //new Lumière { x = 10.4f, y = 1.3f, z = 34.1f, intensité = 10000, couleur = Color.blue }
            };
            foreach (Lumière lumière in lumières) {
                lumière.CréerPointLumière();
            }
        }
    }
}