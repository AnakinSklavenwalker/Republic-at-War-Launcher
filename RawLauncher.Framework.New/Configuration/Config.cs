﻿using System;
using System.Collections.Generic;
using System.IO;
using RawLauncher.Framework.Localization;

namespace RawLauncher.Framework.Configuration
{
    /// <summary>
    /// This Class provides constant datas the Launcher will read. 
    /// </summary>
    public static class Config
    {
        public static Language CurrentLanguage { get; set; }

        public const string EeawForum = "http://www.everythingeaw.com/forum/index.php";
        public const string RaWHomepage = "https://republicatwar.com";
        public const string RaWChangelog = "https://republicatwar.com/changelog";
        public const string ModdbPage = "http://www.moddb.com/mods/republic-at-war";
        public const string FastLaunchFileName = "RawLauncherInfo.txt";
        public const string ServerUrl = "https://gitlab.com/Republic-at-War/Republic-At-War/raw/";
        public const string ModVersionListRelativePath = "master/AvailableModVersions.txt";
        public const string SessionServerUrl = "http://raworganize.com/";

        internal const string CurrentMafHash = "55addbdd742127a14f0ca86f7ba90444";

        public static string RaWAppDataPath => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"RaW_Modding_Team\");

        #region Beta Stuff
        public static Dictionary<string, string> BetaUsers = new Dictionary<string, string>
        {
            {"Anakin", "d783bdc09ad8a57e4afaf4243fd0af7c"}
        };

        public const string BetaPassword = "26fb2b525ebc73751cb420e0180b7acc";

        #endregion


    }
}