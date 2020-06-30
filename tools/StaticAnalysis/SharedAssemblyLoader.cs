﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace StaticAnalysis
{
    internal class SharedAssemblyLoader
    {
        public static HashSet<string> ProcessedFolderSet = new HashSet<string>();

        public static void Load(string directory)
        {
            directory = Path.GetFullPath(directory);
            if(!ProcessedFolderSet.Contains(directory))
            {
                ProcessedFolderSet.Add(directory);
                PreloadSharedAssemblies(directory);
            }
        }

        private static void PreloadSharedAssemblies(string directory)
        {
            var sharedAssemblyFolder = Path.Combine(directory, "Az.Accounts", "NetCoreAssemblies");
            if (Directory.Exists(sharedAssemblyFolder))
            {
                foreach (var file in Directory.GetFiles(sharedAssemblyFolder))
                {
                    try
                    {
                        Console.WriteLine($"PreloadSharedAssemblies: Starting to load assembly {file}.");
                        Assembly.LoadFrom(file);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"PreloadSharedAssemblies: Failed to load assembly {Path.GetFileNameWithoutExtension(file)} with {e}");
                    }
                }
            }
            else
            {
                Console.WriteLine($"PreloadSharedAssemblies: Could not find directory {sharedAssemblyFolder}.");
            }
        }
    }
}
