// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Bicep.Core.Diagnostics;

namespace Bicep.Core.FileSystem
{
    public interface IFileResolver
    {
        /// <summary>
        /// Tries to read a file contents to string. If an exception is encountered, returns null and sets a non-null failureMessage.
        /// </summary>
        /// <param name="fileUri">The file URI to read.</param>
        /// <param name="fileContents">The contents of the file, if successful.</param>
        /// <param name="failureBuilder">Builder for the failure to return, if unsuccessful.</param>
        /// <param name="fileEncoding">Encoding to use when reading file. Auto if set to null</param>
        /// <param name="maxCharacters">Maximum number of text characters to read. if negative - read all.</param>
        bool TryRead(Uri fileUri, [NotNullWhen(true)] out string? fileContents, [NotNullWhen(false)] out DiagnosticBuilder.ErrorBuilderDelegate? failureBuilder);

        bool TryRead(Uri fileUri, [NotNullWhen(true)] out string? fileContents, [NotNullWhen(false)] out DiagnosticBuilder.ErrorBuilderDelegate? failureBuilder, Encoding fileEncoding, int maxCharacters, [NotNullWhen(true)] out Encoding? detectedEncoding);
        /// <summary>
        /// Tries to resolve a child file path relative to a parent module file path.
        /// </summary>
        /// <param name="parentFileUri">The file URI of the parent.</param>
        /// <param name="childFilePath">The file path of the child.</param>
        Uri? TryResolveFilePath(Uri parentFileUri, string childFilePath);


        /// <summary>
        /// Tries to get Directories given a uri and pattern. Both argument and returned URIs MUST have a trailing '/'
        /// </summary>
        /// <param name="fileUri">The base fileUri</param>
        /// <param name="pattern">optional pattern to filter the dirs</param>
        IEnumerable<Uri> GetDirectories(Uri fileUri, string pattern);

        /// <summary>
        /// Tries to get Files given a uri and pattern. fileUri MUST have a trailing '/'
        /// </summary>
        /// <param name="fileUri">The base fileUri</param>
        /// <param name="pattern">optional pattern to filter the resulting files</param>
        IEnumerable<Uri> GetFiles(Uri fileUri, string pattern);

        /// <summary>
        /// Check whether specified URI exsists (depends on URI types). fileUri MUST have a trailing '/'
        /// </summary>
        /// <param name="fileUri">The fileUri to test</param>
        bool TryDirExists(Uri fileUri);


        /// <summary>
        /// Tries to read a file and encode it as base64 string. If an exception is encoutered, returns null and sets a non-null failureMessage.
        /// </summary>
        /// <param name="fileUri">The file URI to read.</param>
        /// <param name="fileContents">The base64 encoded contents of the file, if successful.</param>
        /// <param name="failureBuilder">Builder for the failure to return, if unsuccessful.</param>
        /// <param name="maxCharacters">Maximum number of output base64 text characters to read. if negative - read all. Maximum file size is calculated using (maxCharacters/4)*3 formula.</param>
        bool TryReadAsBase64(Uri fileUri, [NotNullWhen(true)] out string? fileBase64, [NotNullWhen(false)] out DiagnosticBuilder.ErrorBuilderDelegate? failureBuilder, int maxCharacters = -1);
    }
}
