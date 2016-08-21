﻿using System;
using System.Net;
using System.Threading.Tasks;
using static RawLauncherWPF.Utilities.MessageProvider;

namespace RawLauncherWPF.Server
{
    internal class SessionServer : IServer
    {
        public SessionServer(string address)
        {
            ServerRootAddress = address;
        }

        public async Task<bool> CheckRunningAsync() => await Task.FromResult(IsRunning());

        public string DownloadString(string resource)
        {
            string result;
            try
            {
                var webClient = new WebClient();
                result = webClient.DownloadString(ServerRootAddress + resource);
            }
            catch (Exception)
            {
                Show(GetMessage("ExceptionHostServerGetData", ServerRootAddress + resource));
                result = string.Empty;
            }
            return result;
        }

        public bool IsRunning() => UrlExists(string.Empty);
        public string ServerRootAddress { get; set; }

        public bool UrlExists(string resource)
        {
            var request = (HttpWebRequest) WebRequest.Create(ServerRootAddress + resource);
            request.Method = "HEAD";
            request.Timeout = 3000;
            try
            {
                request.GetResponse();
                request.Abort();
            }
            catch (WebException ex)
            {
                var response = ex.Response as HttpWebResponse;
                return (response != null) && response.StatusCode == HttpStatusCode.Forbidden;
            }
            return true;
        }
    }
}