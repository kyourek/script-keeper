using System;
using System.IO;
using System.Linq;
using System.Reflection;

using NUnit.Framework;

namespace Keeper.OfScripts.Tests
{
	[TestFixture]
	public class StyleGroupTests
	{
		[Test]
		public void ResourceTest()
		{
			var file1 = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + 
				Path.DirectorySeparatorChar + 
				"Styles/Style1.css";
			var file2 = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + 
				Path.DirectorySeparatorChar + 
				"Styles/Style2.css";
			
			Assert.IsTrue(File.Exists(file1));
			Assert.IsTrue(File.Exists(file2));
		}
		
		[Test]
		public void LocalStyleGroupAddTest()
		{
			var styleGroup = new LocalStyleGroup();	
			var path = "path/to/style";
			var style1 = new LinkedStyle(path);
			var style2 = new LinkedStyle(path);
			
			styleGroup.Add(style1);
			try
			{
				styleGroup.Add(style2);	
			}
			catch (ResourceAlreadyAddedException)
			{
				return;	
			}
			catch (Exception)
			{
				Assert.Fail("Incorrect exception thrown.");	
			}
			Assert.Fail("No exception thrown.");
		}
		
		[Test]
		public void EmbeddedStyleGroupAddTest()
		{
			var styleGroup = new EmbeddedStyleGroup();
			var source = "#style { color:red; }";
			var style1 = new EmbeddedStyle(source);
			var style2 = new EmbeddedStyle(source);
			
			styleGroup.Add(style1);
			try
			{
				styleGroup.Add(style2);	
			}
			catch (ResourceAlreadyAddedException)
			{
				return;	
			}
			catch (Exception)
			{
				Assert.Fail("Incorrect exception thrown.");	
			}
			Assert.Fail("No exception thrown.");
		}
		
		[Test]
		public void LocalStyleGroupRegisterTest()
		{
			var styleGroup = new LocalStyleGroup();
			var source = "path/to/style";
			var style1 = new LinkedStyle(source);
			var style2 = new LinkedStyle(source);
			
			styleGroup.Register(style1);
			styleGroup.Register(style2);
			Assert.AreEqual(1, styleGroup.Count);
		}
		
		[Test]
		public void EmbeddedStyleGroupRegisterTest()
		{
			var styleGroup = new EmbeddedStyleGroup();
			var source = "#style { color:blue; }";
			var style1 = new EmbeddedStyle(source);
			var style2 = new EmbeddedStyle(source);
			
			styleGroup.Register(style1);
			styleGroup.Register(style2);
			Assert.AreEqual(1, styleGroup.Count);
		}
		
		[Test]
		public void LocalStyleGroupRegisterTest2()
		{
			var styleGroup = new LocalStyleGroup();
			var source = "path/to/style";
			var style1 = new LinkedStyle(source);
			var style2 = new LinkedStyle(source);
			
			Assert.IsFalse(styleGroup.HasRegistered(style1));
			Assert.IsFalse(styleGroup.HasRegistered(style2));
			styleGroup.Register(style1);
			Assert.IsTrue(styleGroup.HasRegistered(style1));
			Assert.IsTrue(styleGroup.HasRegistered(style2));
		}
		
		[Test]
		public void EmbeddedStyleGroupRegisterTest2()
		{
			var styleGroup = new EmbeddedStyleGroup();
			var source = "#style { color:blue; }";
			var style1 = new EmbeddedStyle(source);
			var style2 = new EmbeddedStyle(source);
			
			Assert.IsFalse(styleGroup.HasRegistered(style1));
			Assert.IsFalse(styleGroup.HasRegistered(style2));
			styleGroup.Register(style1);
			Assert.IsTrue(styleGroup.HasRegistered(style1));
			Assert.IsTrue(styleGroup.HasRegistered(style2));
		}
		
		[Test]
		public void LocalStyleGroupRenderTest()
		{
			var style1 = new LinkedStyle("path/to/style1.css");
			var style2 = new LinkedStyle("path/to/style2.css");
			var styleGroup = new LocalStyleGroup();
			
			styleGroup.Add(style1);
			styleGroup.Add(style2);
			
			var rendered = styleGroup.Render();
			var expected = style1.Render() + Environment.NewLine + style2.Render();
			
			Assert.AreEqual(expected, rendered);
		}
		
		[Test]
		public void EmbeddedStyleGroupRenderTest()
		{
			var style1 = new EmbeddedStyle("#world { color:blue; }");
			var style2 = new EmbeddedStyle("#earth { color:green; }");
			var styleGroup = new EmbeddedStyleGroup();
			
			styleGroup.Add(style1);
			styleGroup.Add(style2);
			
			var rendered = styleGroup.Render();
			var expected = "<style type=\"text/css\">";
			expected += Environment.NewLine + "#world { color:blue; }";
			expected += Environment.NewLine + "#earth { color:green; }";
			expected += Environment.NewLine + "</style>";
			
			Assert.AreEqual(expected, rendered);
		}
		
		[Test]
		public void LocalStyleGroupRegisterTest3()
		{
			var styleGroup = new LocalStyleGroup { Helper = new MockLocalHelper() };
			var style1 = "~/Styles/Style1.css";
			var style2 = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/Styles/Style1.css";
			
			styleGroup.Register(style1, style2);
			Assert.AreEqual(1, styleGroup.Count);
		}
		
		[Test]
		public void LocalStyleGroupRegisterTest4()
		{
			var styleGroup = new LocalStyleGroup { Helper = new MockLocalHelper() };
			var style = "~/Styles/DoesNotExist.css";
			
			try
				
			{
				styleGroup.Register(style);	
			}
			catch (ResourceNotFoundException)
			{
				return;	
			}
			catch (Exception)
			{
				Assert.Fail("Incorrect exception thrown.");	
			}
			Assert.Fail("No exception thrown.");
		}	
		
		[Test]
		public void EmbeddedStyleGroupRegisterTest3()
		{
			var styleGroup = new EmbeddedStyleGroup();
			var style1 = "#world { color:blue; }";
			var style2 = "#world { color:blue; }";
			var style3 = "#earth { color:green; }";
			
			styleGroup.Register(style1, style2, style3);

			Assert.AreEqual(2, styleGroup.Count);
			Assert.AreEqual(style1, styleGroup.First().Source);
			Assert.AreEqual(style3, styleGroup.Skip(1).First().Source);
		}
	}
}
