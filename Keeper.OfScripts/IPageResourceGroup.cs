using System;

namespace Keeper.OfScripts
{
    public interface IPageResourceGroup
    {
        /// <summary>
        /// Gets the name of this resource group.
        /// </summary>
        string Name { get; }
        
        /// <summary>
        /// Renders the output HTML for including all resources registered
        /// with this group in the client browser.
        /// </summary>
        /// <returns>
        /// A string of HTML that can be included in a page to include all
        /// resources registered with this instance.
        /// </returns>
        string Render();
        
        /// <summary>
        /// Registers the specified resources with this instance. If any resources
        /// are found to already exist, no action is taken.
        /// </summary>
        /// <param name='resources'>
        /// The resources to be registered.
        /// </param>
        void Register(params string[] resources);        
    }
}
