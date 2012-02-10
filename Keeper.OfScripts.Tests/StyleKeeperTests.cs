using System;

using NUnit.Framework;

namespace Keeper.OfScripts.Tests
{
	[TestFixture]
	public class StyleKeeperTests
	{
		[Test]
		public void ConstructorTest()
		{
			var styleKeeper = default(StyleKeeper);
			
			try
			{
				styleKeeper = new StyleKeeper(null);	
			}
			catch (ArgumentNullException)
			{
				Assert.IsNull(styleKeeper);
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
			var styleKeeper = new StyleKeeper(keeperHelper);
			
			Assert.IsNotNull(styleKeeper);
			Assert.IsNotNull(styleKeeper.Local);
			Assert.IsNotNull(styleKeeper.Embedded);
			
			Assert.AreEqual(styleKeeper.Local.Name, "Local");
			Assert.AreEqual(styleKeeper.Embedded.Name, "Embedded");
			
			Assert.IsTrue(object.ReferenceEquals(keeperHelper, styleKeeper.Helper));
		}
	}
}

