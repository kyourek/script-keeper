using System;

namespace Keeper.OfScripts
{
    public interface ILocalResourceHelper
    {
        /// <summary>
        /// Resolves the absolute path to <paramref name="contentPath"/>.
        /// </summary>
        /// <returns>
        /// The absolute path of <paramref name="contentPath"/>.
        /// </returns>
        /// <param name='contentPath'>
        /// The content path to resolve. This path can begin with a '~'
        /// to denote a path relative to the web root.
        /// </param>
        string ServerPath(string contentPath);
        
        /// <summary>
        /// Resolves the path to <paramref name="contentPath"/> relative to the web root.
        /// </summary>
        /// <returns>
        /// The resolved path of <paramref name="contentPath"/> relative to the web root.
        /// </returns>
        /// <param name='contentPath'>
        /// The content path to resolve. This path can begin with a '~'
        /// to denote a path relative to the web root.
        /// </param>
        string UrlContent(string contentPath);        
    }
}
