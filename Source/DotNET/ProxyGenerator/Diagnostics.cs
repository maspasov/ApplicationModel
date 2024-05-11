// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.CodeAnalysis;

namespace Cratis.Applications.ProxyGenerator;

/// <summary>
/// Holds <see cref="Diagnostic"/> outputs to use.
/// </summary>
public static class Diagnostics
{
    /// <summary>
    /// The <see cref="Diagnostic"/> to report when the output path is missing.
    /// </summary>
    public static readonly Diagnostic MissingOutputPath = Diagnostic.Create(
        new DiagnosticDescriptor(
            "CRATIS0001",
            "Missing output path",
            "Missing output path for generating proxies to. Add <CratisProxyOutput/> to your .csproj file. Will not output proxies.",
            "Generation",
            DiagnosticSeverity.Warning,
            true),
        default);

    /// <summary>
    /// The <see cref="Diagnostic"/> to report when the output path is missing.
    /// </summary>
    public static readonly Func<string, Diagnostic> UnableToResolveModelType = (string queryName) => Diagnostic.Create(
        new DiagnosticDescriptor(
            "CRATIS0003",
            "Unable to resolve model type",
            $"Unable to resolve model type from '{queryName}'.",
            "Generation",
            DiagnosticSeverity.Error,
            true),
        default);

    /// <summary>
    /// The <see cref="Diagnostic"/> to report when the output path is missing.
    /// </summary>
    public static readonly Func<string, string, Diagnostic> ReturnTypeWouldOverwriteParentType = (string typeName, string parentFile) => Diagnostic.Create(
        new DiagnosticDescriptor(
            "CRATIS0004",
            "The return type has a name that matches its method, this would overwrite a conflict when writing the generated return type",
            $"The type '{typeName}' would overwrite '{parentFile}'. This could be because the name of your command / query is the same.",
            "Generation",
            DiagnosticSeverity.Error,
            true),
        default);

    /// <summary>
    /// The <see cref="Diagnostic"/> to report when key in dictionary is not string.
    /// </summary>
    public static readonly Func<string, string, Diagnostic> KeyOfDictionaryMustBeString = (string typeName, string route) => Diagnostic.Create(
        new DiagnosticDescriptor(
            "CRATIS0005",
            "The type of the key in the dictionary must be string",
            $"The type '{typeName}' must have string as key for the dictionary when generating type used in route `{route}`. Only strings are supported as this maps directly to an object literal.",
            "Generation",
            DiagnosticSeverity.Warning,
            true),
        default);

    /// <summary>
    /// The <see cref="Diagnostic"/> to report when the output path is missing.
    /// </summary>
    /// <param name="exception">The exception that occurred.</param>
    /// <returns><see cref="Diagnostics"/> for reporting.</returns>
    public static Diagnostic UnknownError(Exception exception) => Diagnostic.Create(
        new DiagnosticDescriptor(
            "CRATIS0000",
            "Error during proxy generation.",
            "Error '{0}' happened during proxy generation.",
            "Generation",
            DiagnosticSeverity.Warning,
            true,
            "Stack:\n{1}"),
        default,
        messageArgs: new object[] { exception.Message, exception.StackTrace });
}
