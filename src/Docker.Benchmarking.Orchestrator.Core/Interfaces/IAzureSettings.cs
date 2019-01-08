namespace Docker.Benchmarking.Orchestrator.Core.Interfaces
{
    public interface IAzureSettings
    {
        string[] AllowedVMSizes { get; }
        string ShellScriptFilePath { get; }
        string ShellScriptFileName { get; }
    }
}
