/*using System;
using System.Windows.Forms;

class App
{ 
	public static void Main()
	{
		MainForm mainform = new MainForm();   
		Application.Run(mainform);
	}
}

class MainForm:Form 
{ 
}*/

using System;
using Microsoft.Win32;

class MainClass
{
	public static void Main(string[] args)
	{
		RegistryKey myRegKey = Registry.LocalMachine;
		myRegKey = myRegKey.OpenSubKey("HARDWARE\\DESCRIPTION\\System\\CentralProcessor\\0");
		Object oValue = myRegKey.GetValue("VendorIdentifier");
		Console.WriteLine("The central processor of this machine is: {0}.", oValue.ToString());
	}
}


