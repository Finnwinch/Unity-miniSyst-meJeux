namespace Script.Utilitaire.titre {
    public struct EnduranceCoût {
        public float Coût { get; }
        public string Nom { get; }
        public EnduranceCoût(float coût, string nom) {
            Coût = coût;
            Nom = nom;
        }
        public static readonly EnduranceCoût DÉPLACEMENT = new EnduranceCoût(0.1f, "DÉPLACEMENT");
        public static readonly EnduranceCoût DÉPLACEMENT_RAPIDE = new EnduranceCoût(0.4f, "DÉPLACEMENT_RAPIDE");
        public static readonly EnduranceCoût SAUT = new EnduranceCoût(0.3f, "SAUT");
        public static readonly EnduranceCoût ATTAQUER = new EnduranceCoût(0.4f, "ATTAQUER");
        public static readonly EnduranceCoût INVOCATION = new EnduranceCoût(0.6f, "INVOCATION");
    }

}