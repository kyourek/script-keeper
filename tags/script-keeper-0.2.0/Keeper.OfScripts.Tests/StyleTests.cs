using System;
using NUnit.Framework;

namespace Keeper.OfScripts.Tests
{
	[TestFixture]
	public class StyleTests
	{
		[Test]
		public void LinkedStyleConstructorTest()
		{
			// for sanity...
			var style = new LinkedStyle("source");
			Assert.IsNotNull(style);
		}
		
		[Test]
		public void EmbeddedStyleConstructorTest()
		{
			// for sanity...
			var style = new EmbeddedStyle("source");
			Assert.IsNotNull(style);
		}
		
		[Test]
		public void LinkedStyleEqualsTest()
		{
			var style1 = new LinkedStyle("path/to/style.css");
			var style2 = new LinkedStyle("path/to/style.css");
			Assert.IsFalse(ReferenceEquals(style1, style2));
			Assert.AreEqual(style1, style2);
			Assert.AreEqual(style2, style1);
			Assert.IsTrue(style1.Equals(style2));
			Assert.IsTrue(style2.Equals(style1));
		}
		
		[Test]
		public void LinkedStyleEqualsTest2()
		{
			var style1 = new LinkedStyle("path/to/style1.css");
			var style2 = new LinkedStyle("path/to/style2.css");
			Assert.AreNotEqual(style1, style2);
			Assert.AreNotEqual(style1, style2);
			Assert.IsFalse(style1.Equals(style2));
			Assert.IsFalse(style2.Equals(style1));
		}
		
		[Test]
		public void LinkedEqualsEmbeddedTest()
		{
			var source = "some style";
			var style1 = new LinkedStyle(source);
			var style2 = new EmbeddedStyle(source);
			Assert.IsFalse(style1.Equals(style2));
			Assert.IsFalse(style2.Equals(style1));
		}
		
		[Test]
		public void LinkedStyleRenderTest()
		{
			var style = new LinkedStyle("path/to/style.css");
			var str = style.Render();
			Assert.AreEqual("<link rel=\"stylesheet\" type=\"text/css\" href=\"path/to/style.css\" />", str);
		}
		
		[Test]
		public void EmbeddedStyleRenderTest()
		{
			var style = new EmbeddedStyle("#style { color:blue; }");
			var rendered = style.Render();
			var expected = "<style type=\"text/css\">" + Environment.NewLine;
			expected += "#style { color:blue; }" + Environment.NewLine;
			expected += "</style>";
			Assert.AreEqual(expected, rendered);
		}
	}
}

