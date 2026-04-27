namespace TheEconomy.Server.Resources.ServerInitialization.Data;

public static class ServerInitializationData
{
    public const int MaxReconnectionAttempts = 3;
    public const int MillisecondsBetweenAttempts = 5000;
    
    public const string HostnameFormat = "(Android y Windows) {0} {1} | v{2}";
}