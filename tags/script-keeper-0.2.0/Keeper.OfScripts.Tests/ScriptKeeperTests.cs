using System;
using System.Configuration;

using NUnit.Framework;
using Keeper.OfScripts.Configuration;

namespace Keeper.OfScripts.Tests
{   
	[TestFixture]
	public class ScriptKeeperTests
	{	
		[Test]
		public void ConstructorTest()
		{
			var scriptKeeper = default(ScriptKeeper);
			
			try
			{
				scriptKeeper = new ScriptKeeper(null);	
			}
			catch (ArgumentNullException)
			{
				Assert.IsNull(scriptKeeper);
				return;
			}
			catch (Exception)
			{
				Assert.Fail("Wrong exception thrown.");	
			}
			
			Assert.Fail("No exception thrown.");
		}
		
		[Test]
		public void ConstructorTest2()
		{
			var keeperHelper = new MockHelper();			
			var scriptKeeper = new ScriptKeeper(keeperHelper);
			
			Assert.IsNotNull(scriptKeeper);
			Assert.IsNotNull(scriptKeeper.Local);
			Assert.IsNotNull(scriptKeeper.Remote);
			Assert.IsNotNull(scriptKeeper.Inline);
			
			Assert.AreEqual(scriptKeeper.Local.Name, "Local");
			Assert.AreEqual(scriptKeeper.Remote.Name, "Remote");
			Assert.AreEqual(scriptKeeper.Inline.Name, "Inline");
			
			Assert.IsTrue(object.ReferenceEquals(keeperHelper, scriptKeeper.Helper));
		}
        
        [Test]
        public void RegisterTest()
        {
            var keeperHelper = new MockHelper();
            var scriptKeeper = new ScriptKeeper(keeperHelper);
            
            scriptKeeper.Register("jQuery");
            var rendered = "<script type=\"text/javascript\" src=\"//ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js\"></script>";
            Assert.AreEqual(rendered, scriptKeeper.Remote.Render());
        }
	}
}
