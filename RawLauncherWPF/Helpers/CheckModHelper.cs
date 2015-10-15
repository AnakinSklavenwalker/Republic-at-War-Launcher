﻿using RawLauncherWPF.UI;
using RawLauncherWPF.Xml;

namespace RawLauncherWPF.Helpers
{
    public static class CheckModHelper
    {
        internal static string GetReferenceDir(FileContainerFolder folder, ILauncherPane pane)
        {
            var rootDir = folder.TargetType == TargetType.Ai
                ? pane.MainWindowViewModel.LauncherViewModel.Foc.GameDirectory
                : pane.MainWindowViewModel.LauncherViewModel.CurrentMod.ModDirectory;

            var referenceDir = rootDir + folder.TargetPath;
            return referenceDir;
        }

        internal static string RestorePathGenerator(bool online, ILauncherPane pane)
        {
            if (online)
                return @"RescueFiles\" + pane.MainWindowViewModel.LauncherViewModel.CurrentMod.Version + @"\";
            return pane.MainWindowViewModel.LauncherViewModel.RestoreDownloadDir + @"\RescueFiles\" +
                   pane.MainWindowViewModel.LauncherViewModel.CurrentMod.Version + @"\";
        }
    }
}