using PulsarModLoader.Chat.Commands.CommandRouter;

namespace DiscoShip
{
    class Command : ChatCommand
    {
        public override string[] CommandAliases()
        {
            return new string[] { "disco", "panic" };
        }

        public override string Description()
        {
            return "Makes the ship lights more colorful";
        }

        public override void Execute(string arguments)
        {
            if (!string.IsNullOrWhiteSpace(arguments))
            {
                int interval;
                if (int.TryParse(arguments, out interval))
                {
                    ShipInfo.interval = interval;
                    Mod.Instance.Enable();
                    return;
                }
            }
            if (Mod.Instance.IsEnabled())
            {
                Mod.Instance.Disable();
            }
            else
            {
                ShipInfo.interval = 25;
                Mod.Instance.Enable();
            }
        }
    }
}
