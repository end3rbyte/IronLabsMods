namespace IronLabs.SavesCharactersOnStop
{
    internal static class RestartServerCommand
    {
        private const string CommandName = "restartserver";

        internal static void Register()
        {
            new Terminal.ConsoleCommand(
                CommandName,
                "Saves connected characters and the world, then restarts the dedicated server.",
                Execute,
                isCheat: false,
                isNetwork: false,
                onlyServer: true,
                allowInDevBuild: true,
                remoteCommand: true,
                onlyAdmin: true);
        }

        private static object Execute(Terminal.ConsoleEventArgs args)
        {
            if (!SavesCharactersOnStopPlugin.RestartCommandEnabled)
            {
                return "The restartserver command is disabled on this server.";
            }

            if (!SavesCharactersOnStopPlugin.Coordinator.RequestRestart())
            {
                return "A server restart cannot be started in the current state.";
            }

            args.Context.AddString("Server restart requested; saving connected characters.");
            SavesCharactersOnStopPlugin.Log.LogMessage("An administrator requested a graceful server restart.");
            return true;
        }
    }
}
