using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using HarmonyLib;

namespace Marsey;

public class MarseyPatcher
{
    /// <summary>
    /// Boots up the patcher, executed by the loader assembly only.
    /// </summary>
    /// <param name="robClientAssembly">Robust.Client assembly as *loaded* by the *loader*</param>
    /// <exception cref="Exception">Excepts if Robust.Client assembly is null</exception>
    public void Boot(Assembly? robClientAssembly)
    {
        if (robClientAssembly == null) throw new Exception("Robust.Client was null.");

        GameAssemblyManager.Init(new Harmony("com.validhunters.marseyloader"));

        GameAssemblyManager.GetGameAssemblies(out var clientAss, out var robustSharedAss, out var clientSharedAss);

        PatchAssemblyManager.SetAssemblies(robClientAssembly, clientAss, robustSharedAss, clientSharedAss);

        FileHandler.LoadAssemblies(new []{"Marsey", "Enabled"});

        GameAssemblyManager.PatchProc();
    }
}