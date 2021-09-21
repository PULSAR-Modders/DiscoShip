using PulsarModLoader;

namespace DiscoShip
{
    public class Mod : PulsarMod
    {
        public static Mod Instance;

        public Mod() : base()
        {
            Instance = this;
            Disable();
        }

        public override string Version => "0.0.0";

        public override string Author => "18107";

        public override string ShortDescription => "Rainbow ship lights";

        public override string Name => "Disco ship";

        public override bool CanBeDisabled()
        {
            return true;
        }

        public override string HarmonyIdentifier()
        {
            return "id107.discoship";
        }
    }
}
