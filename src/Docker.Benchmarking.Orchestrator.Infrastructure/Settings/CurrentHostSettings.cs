using Ardalis.GuardClauses;
using Docker.Benchmarking.Orchestrator.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace Docker.Benchmarking.Orchestrator.Infrastrcture
{
    public sealed class CurrentHostSettings : ICurrentHostSettings
    {
        private string _currentHost { get; set; }
        public string CurrentHost
        {
            get
            {
                return _currentHost;

            }
        }

        private int _currentPort { get; set; }
        public int CurrentPort
        {
            get
            {
                return _currentPort;

            }
        }

        private Uri _currentHostUri { get; set; }
        public Uri CurrentHostUri
        {
            get
            {
                return _currentHostUri;

            }
        }

        public void SetCurrentHost(string currentHost)
        {
            Uri uriHost;

            Guard.Against.NullOrEmpty(currentHost, nameof(currentHost));

            if (IsValidDomainName(currentHost, out uriHost))
            {
                _currentHost = currentHost;
                _currentHostUri = uriHost;
                return;
            }
            else
            {
                currentHost = "http://" + currentHost;
                if (IsValidDomainName(currentHost, out uriHost))
                {
                    _currentHost = currentHost;
                    _currentHostUri = uriHost;
                    return;

                }
            }

            throw new ArgumentOutOfRangeException("hostName provided isn't valid.  Must be IP Address or DNS/Domain name.");
        }

        public void SetCurrentPort(int currentPort)
        {
            if (currentPort > 10 && currentPort < 65536)
                _currentPort = currentPort;
            else
                throw new ArgumentOutOfRangeException("portNumber is not a valid portNumber.");
        }

        //https://stackoverflow.com/questions/30436643/validate-whether-a-string-is-valid-for-ip-address-or-not
        public bool IsValidIP(string address)
        {
            Guard.Against.NullOrEmpty(address, nameof(address));

            IPAddress ip;

            if (IPAddress.TryParse(address, out ip))
            {
                switch (ip.AddressFamily)
                {
                    case System.Net.Sockets.AddressFamily.InterNetwork:
                        if (address.Length > 6 && address.Contains("."))
                        {
                            string[] s = address.Split('.');
                            if (s.Length == 4 && s[0].Length > 0 && s[1].Length > 0 && s[2].Length > 0 && s[3].Length > 0)
                                return true;
                        }
                        break;
                    case System.Net.Sockets.AddressFamily.InterNetworkV6:
                        if (address.Contains(":") && address.Length > 15)
                            return true;
                        break;
                    default:
                        break;
                }
            }

            return false;
        }

        //https://stackoverflow.com/questions/967516/best-way-to-determine-if-a-domain-name-would-be-a-valid-in-a-hosts-file
        private bool IsValidDomainName(string name, out Uri uriResult)
        {
            Guard.Against.NullOrEmpty(name, nameof(name));

            bool result = Uri.TryCreate(name, UriKind.Absolute, out uriResult);
            return result;
        }
    }
}
