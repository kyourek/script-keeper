using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

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
    
    internal abstract class PageResourceGroup<TResource> : IPageResourceGroup, IEnumerable<TResource>
        where TResource : PageResource
    {
        private readonly List<TResource> _List = new List<TResource>(); 
        
        private string _Name;
        
        public int Count { get { return _List.Count; } }
        
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        
        public abstract bool HasRegistered(string resource);      
        public abstract void Register(params string[] resources);
        
        public virtual string Render()
        {
            var str = string.Empty;
            foreach (var resource in _List)
                str += string.IsNullOrEmpty(str) ? resource.Render() : Environment.NewLine + resource.Render();         
            return str;
        }
        
        public void Add(TResource resource)
        {
            if (_List.Contains(resource)) throw new ResourceAlreadyAddedException("The resource has already been added.");
            _List.Add(resource);  
        }
        
        public void Register(params TResource[] resources)
        {
            foreach (var resource in resources)
                if (!HasRegistered(resource))
                    Add(resource);    
        }
        
        public bool HasRegistered(TResource resource)
        {
            return _List.Contains(resource);  
        }       
        
        public IEnumerator<TResource> GetEnumerator()
        {
            return _List.GetEnumerator();
        }
        
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _List.GetEnumerator();
        }
    }    
    
    internal abstract class LocalResourceGroup<TResource> : PageResourceGroup<TResource>
        where TResource : PageResource
    {       
        private ILocalResourceHelper _Helper;     
  
        protected abstract TResource CreateResource(string source);
        
        public ILocalResourceHelper Helper 
        { 
            get { return _Helper; } 
            set { _Helper = value; }
        }       
        
        public override bool HasRegistered(string resource)
        {
            if (Helper == null) throw new InvalidOperationException("Cannot resolve a URL with a null helper.");
            var source = Helper.UrlContent(resource);
            return this.FirstOrDefault(s => s.Source == source) != null;
        }
        
        public override void Register(params string[] resources)
        {
            if (resources == null) throw new ArgumentNullException("resources");
            if (resources.Length < 1) throw new ArgumentException("At least one resource is required to register.");
            if (Helper == null) throw new InvalidOperationException("Cannot resolve a URL with a null helper.");
        
            foreach (var resource in resources)
            {
                if (!HasRegistered(resource)) 
                {
                    var path = Helper.UrlContent(resource);
                    var file = Helper.ServerPath(resource);
                    
                    if (!File.Exists(file)) throw new ResourceNotFoundException("The resource '" + path + "' does not exist.");
                    
                    Add(CreateResource(path));
                }
            }
        }
    }    
    
    internal abstract class RemoteResourceGroup<TResource> : PageResourceGroup<TResource>
        where TResource : PageResource
    {
        protected abstract TResource CreateResource(string source);
        
        public override bool HasRegistered(string resource)
        {
            return this.FirstOrDefault(s => s.Source == resource) != null;
        }
        
        public override void Register(params string[] resources)
        {
            if (resources == null) throw new ArgumentNullException("resources");
            if (resources.Length < 1) throw new ArgumentException("At least one resource is required to register.");
            
            foreach (var resource in resources)
                if (!HasRegistered(resource))
                    Add(CreateResource(resource));                  
        }
    }    
}
