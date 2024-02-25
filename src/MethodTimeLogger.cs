// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MethodTimeLogger.cs" company="Stone Assemblies">
// Copyright © 2023 - 2024 Stone Assemblies development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Reflection;

/// <summary>
/// The method time logger.
/// </summary>
internal static class MethodTimeLogger
{
    /// <summary>
    /// The log.
    /// </summary>
    /// <param name="methodBase">
    /// The method base.
    /// </param>
    /// <param name="timeSpan">
    /// The time span.
    /// </param>
    /// <param name="message">
    /// The message.
    /// </param>
    public static void Log(MethodBase methodBase, TimeSpan timeSpan, string message)
    {
        string? finalMessage = $"[Method Time] => {methodBase.DeclaringType?.Name}.{methodBase.Name} took '{timeSpan}'";

        if (!string.IsNullOrWhiteSpace(message))
        {
            finalMessage += $" | {message}";
        }

        Serilog.Log.Information(finalMessage);

#if DEBUG
        Console.WriteLine(finalMessage);
#endif
    }
}