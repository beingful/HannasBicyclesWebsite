namespace Chat
{
    class ConnectionData
    {
        public readonly int LocalPort;
        public readonly int RemotePort;
        public readonly string Username;
        public const string IP = "127.0.0.1";

        public ConnectionData(int localPort, int remotePort, string userName)
        {
            LocalPort = localPort;
            RemotePort = remotePort;
            Username = userName;
        }
    }
}
