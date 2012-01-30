using System;
using System.IO;
using System.Linq;
using System.Reflection;

using NUnit.Framework;

namespace Keeper.OfScripts.Tests
{
	[TestFixture]
	public class ScriptGroupTests
	{	
		[Test]
		public void ResourceTest()
		{
			var file1 = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + 
				Path.DirectorySeparatorChar + 
				"Scripts/Script1.js";
			var file2 = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + 
				Path.DirectorySeparatorChar + 
				"Scripts/Script2.js";
			
			Assert.IsTrue(File.Exists(file1));
			Assert.IsTrue(File.Exists(file2));
		}
		
		[Test]
		public void LocalScriptGroupConstructorTest()
		{
			var scriptGroup = new LocalScriptGroup();
			
			Assert.IsNotNull(scriptGroup);
		}
		
		[Test]
		public void InlineScriptGroupConstructorTest()
		{
			var scriptGroup = new InlineScriptGroup();	
			
			Assert.IsNotNull(scriptGroup);
		}
		
		[Test]
		public void RemoteScriptGroupConstructorTest()
		{
			var scriptGroup = new RemoteScriptGroup();
			
			Assert.IsNotNull(scriptGroup);
		}
		
		[Test]
		public void LocalScriptGroupAddTest()
		{
			var scriptGroup = new LocalScriptGroup();
			
			var path = "path/to/script";
			var script1 = new LinkedScript(path);
			var script2 = new LinkedScript(path);
			
			scriptGroup.Add(script1);
			
			try
			{
				scriptGroup.Add(script2);
			}
			catch (ScriptAlreadyAddedException)
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
		public void RemoteScriptGroupAddTest()
		{
			var scriptGroup = new RemoteScriptGroup();
			
			var path = "path/to/script";
			var script1 = new LinkedScript(path);
			var script2 = new LinkedScript(path);
			
			scriptGroup.Add(script1);
			
			try
			{
				scriptGroup.Add(script2);
			}
			catch (ScriptAlreadyAddedException)
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
		public void InlineScriptGroupAddTest()
		{
			var scriptGroup = new InlineScriptGroup();
			
			var path = "var s = 'Hello World!';";
			var script1 = new InlineScript(path);
			var script2 = new InlineScript(path);
			
			scriptGroup.Add(script1);
			
			try
			{
				scriptGroup.Add(script2);
			}
			catch (ScriptAlreadyAddedException)
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
		public void LocalScriptGroupRegisterTest()
		{
			var scriptGroup = new LocalScriptGroup();
			
			var path = "path/to/script";
			var script1 = new LinkedScript(path);
			var script2 = new LinkedScript(path);
			
			scriptGroup.Register(script1);
			scriptGroup.Register(script2);
			
			Assert.AreEqual(1, scriptGroup.Count);
		}
		
		[Test]
		public void RemoteScriptGroupRegisterTest()
		{
			var scriptGroup = new RemoteScriptGroup();
			
			var path = "path/to/script";
			var script1 = new LinkedScript(path);
			var script2 = new LinkedScript(path);
			
			scriptGroup.Register(script1);
			scriptGroup.Register(script2);
			
			Assert.AreEqual(1, scriptGroup.Count);			
		}
		
		[Test]
		public void InlineScriptGroupRegisterTest()
		{
			var scriptGroup = new InlineScriptGroup();
			
			var path = "var s = 'Hello World!';";
			var script1 = new InlineScript(path);
			var script2 = new InlineScript(path);
			
			scriptGroup.Register(script1);
			scriptGroup.Register(script2);
			
			Assert.AreEqual(1, scriptGroup.Count);			
		}
		
		[Test]
		public void LocalScriptGroupHasRegisteredTest()
		{
			var scriptGroup = new LocalScriptGroup();
			
			var path = "path/to/script";
			var script1 = new LinkedScript(path);
			var script2 = new LinkedScript(path);
			
			Assert.IsFalse(scriptGroup.HasRegistered(script1));
			Assert.IsFalse(scriptGroup.HasRegistered(script2));
			
			scriptGroup.Register(script1);
			
			Assert.IsTrue(scriptGroup.HasRegistered(script1));
			Assert.IsTrue(scriptGroup.HasRegistered(script2));
		}
		
		[Test]
		public void RemoteScriptGroupHasRegisteredTest()
		{
			var scriptGroup = new RemoteScriptGroup();
			
			var path = "path/to/script";
			var script1 = new LinkedScript(path);
			var script2 = new LinkedScript(path);
			
			Assert.IsFalse(scriptGroup.HasRegistered(script1));
			Assert.IsFalse(scriptGroup.HasRegistered(script2));
			
			scriptGroup.Register(script1);
			
			Assert.IsTrue(scriptGroup.HasRegistered(script1));
			Assert.IsTrue(scriptGroup.HasRegistered(script2));
		}
		
		[Test]
		public void InlineScriptGroupHasRegisteredTest()
		{
			var scriptGroup = new InlineScriptGroup();
			
			var path = "var s = 'Hello World!';";
			var script1 = new InlineScript(path);
			var script2 = new InlineScript(path);
			
			Assert.IsFalse(scriptGroup.HasRegistered(script1));
			Assert.IsFalse(scriptGroup.HasRegistered(script2));
			
			scriptGroup.Register(script1);
			
			Assert.IsTrue(scriptGroup.HasRegistered(script1));
			Assert.IsTrue(scriptGroup.HasRegistered(script2));
		}
		
		[Test]
		public void LocalScriptGroupRenderTest()
		{
			var scriptGroup = new LocalScriptGroup();
			
			var path1 = "path/to/script1";
			var path2 = "path/to/script2";
			var script1 = new LinkedScript(path1);
			var script2 = new LinkedScript(path2);
			
			scriptGroup.Add(script1);
			scriptGroup.Add(script2);
			
			Func<string, string> renderTest = s => "<script type=\"text/javascript\" src=\"" + s + "\"></script>";

			var render = scriptGroup.Render();
			var expected = renderTest(path1) + Environment.NewLine + renderTest(path2);

			Assert.AreEqual(expected, render);
		}
		
		[Test]
		public void RemoteScriptGroupRenderTest()
		{
			var scriptGroup = new RemoteScriptGroup();
			
			var path1 = "//path/to/script1";
			var path2 = "//path/to/script2";
			var script1 = new LinkedScript(path1);
			var script2 = new LinkedScript(path2);
			
			scriptGroup.Add(script1);
			scriptGroup.Add(script2);
			
			Func<string, string> renderTest = s => "<script type=\"text/javascript\" src=\"" + s + "\"></script>";

			var render = scriptGroup.Render();
			var expected = renderTest(path1) + Environment.NewLine + renderTest(path2);

			Assert.AreEqual(expected, render);
		}
		
		[Test]
		public void InlineScriptGroupRenderTest()
		{
			var scriptGroup = new InlineScriptGroup();
			
			var source1 = "var s = 'Hello World!';";
			var source2 = "var r = 'Hello Earth!';";
			var script1 = new InlineScript(source1);
			var script2 = new InlineScript(source2);
			
			scriptGroup.Add(script1);
			scriptGroup.Add(script2);
			
			var expected = "<script type=\"text/javascript\">" + Environment.NewLine;
			expected += "// <![CDATA[" + Environment.NewLine;
			expected += source1 + Environment.NewLine;
			expected += source2 + Environment.NewLine;
			expected += "// ]]>" + Environment.NewLine;
			expected += "</script>";

			var render = scriptGroup.Render();

			Assert.AreEqual(expected, render);			
		}
		
		[Test]
		public void LocalScriptGroupRegisterTest2()
		{
			var scriptGroup = new LocalScriptGroup { Helper = new MockLocalHelper() };
			
			var script1 = "~/Scripts/Script1.js";
			var script2 = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/Scripts/Script1.js";
			
			scriptGroup.Register(script1, script2);
			
			Assert.AreEqual(1, scriptGroup.Count);
		}
		
		[Test]
		public void LocalScriptGroupRegisterTest3()
		{
			var scriptGroup = new LocalScriptGroup { Helper = new MockLocalHelper() };
			
			var script = "~/Scripts/DoesNotExist.js";
			
			try
			{
				scriptGroup.Register(script);	
			}
			catch (ScriptNotFoundException)
			{
				return;	
			}
			catch (Exception)
			{
				Assert.Fail("Wrong exception thrown.");	
			}
			
			Assert.Fail("No exception thrown.");			
		}
		
		[Test]
		public void RemoteScriptGroupRegisterTest2()
		{
			var scriptGroup = new RemoteScriptGroup();
			
			var script1 = "http://path/to/script";
			var script2 = "http://path/to/script";
			
			scriptGroup.Register(script1, script2);
			
			Assert.AreEqual(1, scriptGroup.Count);
			Assert.AreEqual(script1, scriptGroup.First().Source);
		}
		
		[Test]
		public void RemoteScriptRegisterTest3()
		{
			var scriptGroup = new RemoteScriptGroup();
			
			var script1 = "http://path/to/script1";
			var script2 = "http://path/to/script2";
			
			scriptGroup.Register(script1, script2);
			
			Assert.AreEqual(2, scriptGroup.Count);
			Assert.AreEqual(script1, scriptGroup.First().Source);
			Assert.AreEqual(script2, scriptGroup.Skip(1).First().Source);
		}
	}
}
