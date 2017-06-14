namespace Acando.AspNet.NodeServices.Util
{
    using System;
    using System.IO;

    /// <summary>
    /// Makes it easier to pass script files to Node in a way that's sure to clean up after the process exits.
    /// </summary>
    public sealed class StringAsTempFile : IDisposable
    {
        private bool _disposedValue;
        private bool _hasDeletedTempFile;
        private object _fileDeletionLock = new object();

        /// <summary>
        /// Create a new instance of <see cref="StringAsTempFile"/>.
        /// </summary>
        /// <param name="content">The contents of the temporary file to be created.</param>
        public StringAsTempFile(string content)
        {
            FileName = Path.GetTempFileName();
            File.WriteAllText(FileName, content);

            // Because .NET finalizers don't reliably run when the process is terminating, also
            // add event handlers for other shutdown scenarios.
            AppDomain.CurrentDomain.ProcessExit += HandleProcessExit;
            AppDomain.CurrentDomain.DomainUnload += HandleProcessExit;
        }

        /// <summary>
        /// Specifies the filename of the temporary file.
        /// </summary>
        public string FileName { get; }

        /// <summary>
        /// Disposes the instance and deletes the associated temporary file.
        /// </summary>
        public void Dispose()
        {
            DisposeImpl(true);
            GC.SuppressFinalize(this);
        }

        private void DisposeImpl(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    // Dispose managed state
                    AppDomain.CurrentDomain.ProcessExit -= HandleProcessExit;
                    AppDomain.CurrentDomain.DomainUnload -= HandleProcessExit;
                }

                EnsureTempFileDeleted();

                _disposedValue = true;
            }
        }

        private void EnsureTempFileDeleted()
        {
            lock (_fileDeletionLock)
            {
                if (!_hasDeletedTempFile)
                {
                    File.Delete(FileName);
                    _hasDeletedTempFile = true;
                }
            }
        }
        private void HandleProcessExit(object sender, EventArgs args)
        {
            EnsureTempFileDeleted();
        }


        /// <summary>
        /// Implements the finalization part of the IDisposable pattern by calling Dispose(false).
        /// </summary>
        ~StringAsTempFile()
        {
            DisposeImpl(false);
        }
    }
}