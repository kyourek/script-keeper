using System;
using NUnit.Framework;

namespace Keeper.OfScripts.Tests
{
	[TestFixture]
	public class ScriptTests
	{
		[Test]
		public void LinkedScriptConstructorTest()
		{
			var path = "path/to/script";
			var script = new LinkedScript(path);
			
			Assert.AreEqual(path, script.Source);
		}
		
		[Test]
		public void InlineScriptConstructorTest()
		{
			var source = "var s = 'Hello World!';";
			var script = new InlineScript(source);
			
			Assert.AreEqual(source, script.Source);
		}
		
		[Test]
		public void LinkedEqualsLinkedTest()
		{
			var script1 = new LinkedScript("path/to/script");
			var script2 = new LinkedScript("path/to/script");
			
			Assert.IsTrue(script1.Equals(script2));
			Assert.IsTrue(script2.Equals(script1));
		}
		
		[Test]
		public void LinkedEqualsLinkedTest2()
		{
			var script1 = new LinkedScript("path/to/script1");
			var script2 = new LinkedScript("path/to/script2");
			
			Assert.IsFalse(script1.Equals(script2));
			Assert.IsFalse(script2.Equals(script1));
		}
		
		[Test]
		public void LinkedEqualsLinkedTest3()
		{
			var script1 = new LinkedScript("path/to/script");
			var script2 = new LinkedScript("path/to/script");
			
			var obj = (object)script2;
			
			Assert.IsTrue(script1.Equals(obj));
			Assert.IsTrue(obj.Equals(script1));
		}
		
		[Test]
		public void LinkedEqualsInlineTest()
		{
			var script1 = new LinkedScript("path/to/script");
			var script2 = new InlineScript("path/to/script");
			
			Assert.IsFalse(script1.Equals(script2));
			Assert.IsFalse(script2.Equals(script1));
		}
		
		[Test]
		public void LinkedEqualsInlineTest2()
		{
			var script1 = new LinkedScript("path/to/script");
			var script2 = new InlineScript("path/to/script");
			
			var obj = (object)script2;
			
			Assert.IsFalse(script1.Equals(obj));
			Assert.IsFalse(obj.Equals(script1));
		}
		
		[Test]
		public void InlineEqualsInlineTest()
		{
			var script1 = new InlineScript("var s = 'Hello World!';");
			var script2 = new InlineScript("var s = 'Hello World!';");
			
			Assert.IsTrue(script1.Equals(script2));
			Assert.IsTrue(script2.Equals(script1));
		}
		
		[Test]
		public void InlineEqualsInlineTest2()
		{
			var script1 = new InlineScript("var s = 'Hello World!';");
			var script2 = new InlineScript("var s = 'Hello Earth!';");
			
			Assert.IsFalse(script1.Equals(script2));
			Assert.IsFalse(script2.Equals(script1));
		}
		
		[Test]
		public void LinkedScriptRenderTest()
		{
			var path = "path/to/script";
			var script = new LinkedScript(path);	
			
			var render = script.Render();
			var expected = "<script type=\"text/javascript\" src=\"" + path + "\"></script>";
			
			Assert.AreEqual(expected, render);
		}
		
		[Test]
		public void InlineScriptRenderTest()
		{
			var source = "var s = 'Hello World!';";
			var script = new InlineScript(source);
			
			var render = script.Render();
			var expected = "<script type=\"text/javascript\">";
			expected += Environment.NewLine + "// <![CDATA[";
			expected += Environment.NewLine + source;
			expected += Environment.NewLine + "// ]]>";
			expected += Environment.NewLine + "</script>";
			
			Assert.AreEqual(expected, render);
		}
	}
}
