using Script.Moteur;
using Script.Utilitaire.titre;

namespace Script.Utilitaire {
    public class Endurance {
        public float Saturation { get; set; }
        public Endurance() {
            Saturation = 1;
        }
        public static void Fatigué(Entité entité, EnduranceCoût action) {
            if (entité.Endurance.Saturation - action.Coût < 0) { return; }

            if (FilsTraitement.VérifierFils("Repos" + entité.GetHashCode())) {
                FilsTraitement.ArrêterTraitement("Repos" + entité.GetHashCode());
            }

            entité.Endurance.Saturation -= action.Coût;

            FilsTraitement.DémarrerTraitement(1, () => {
                entité.Endurance.Saturation += 0.1f;
                if (entité.Endurance.Saturation >= 1) {
                    entité.Endurance.Saturation = 1;
                    FilsTraitement.ArrêterTraitement("Repos" + entité.GetHashCode());
                }
            }, "Repos" + entité.GetHashCode());
        }
    }
}