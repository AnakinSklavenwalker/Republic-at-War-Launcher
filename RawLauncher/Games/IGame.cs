﻿using RawLauncher.Framework.Mods;

namespace RawLauncher.Framework.Games
{
    public interface IGame
    {
        /// <summary>
        /// Returns the full Path of the Games Root Directory
        /// </summary>
        string GameDirectory { get; }

        /// <summary>
        /// Return the full Path of the SaveGame Directory
        /// </summary>
        string SaveGameDirectory { get; }

        /// <summary>
        /// Returns the name of the Game
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Contains Data of the Process
        /// </summary>
        GameProcessData GameProcessData { get; }

        /// <summary>
        /// Checks whether a mod exists
        /// </summary>
        /// <returns></returns>
        bool Exists();

        /// <summary>
        /// Plays the default game
        /// </summary>
        void PlayGame();

        /// <summary>
        /// Plays the game with the mod
        /// </summary>
        /// <param name="mod"></param>
        void PlayGame(IMod mod);

        /// <summary>
        /// Patches the Game
        /// </summary>
        /// <returns></returns>
        bool Patch();

        /// <summary>
        /// Checks if the patch is installed
        /// </summary>
        /// <returns></returns>
        bool IsPatched();

        /// <summary>
        /// Deletes a Mod by it's name
        /// </summary>
        /// <param name="mod">Name of the mod</param>
        void DeleteMod(IMod mod);

        /// <summary>
        /// Removes any additional contnet from Rood Data folder.
        /// </summary>
        void ClearDataFolder();

        /// <summary>
        /// Determines whether the game AI files are cleared
        /// </summary>
        /// <returns>
        ///   <see langword="true"/> if game is clear; otherwise, <see langword="false"/>.
        /// </returns>
        bool IsGameAiClear();

        //void BackUpAiFiles();

        //void ResotreAiFiles();
    }
}