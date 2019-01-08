using System;
using System.Collections.Generic;
using System.Text;

namespace Docker.Benchmarking.Orchestrator.Core.Interfaces
{
    public interface ICurrentHostSettings
    {
        string CurrentHost { get; }
        Uri CurrentHostUri { get; }
        int CurrentPort { get; }

        void SetCurrentHost(string currentHost);
        void SetCurrentPort(int currentPort);
    }
}
