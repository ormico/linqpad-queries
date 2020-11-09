<Query Kind="Program" />

void Main()
{
	string path = @"C:\ProgramData\Oracle\Java\javapath;C:\Program Files\Common Files\Microsoft Shared\Microsoft Online Services;C:\Program Files (x86)\Common Files\Microsoft Shared\Microsoft Online Services;C:\PROGRAM FILES\COMMON FILES\MICROSOFT SHARED\WINDOWS LIVE;C:\PROGRAM FILES (X86)\COMMON FILES\MICROSOFT SHARED\WINDOWS LIVE;C:\PROGRAM FILES (X86)\NVIDIA CORPORATION\PHYSX\COMMON;C:\PROGRAM FILES (X86)\INTEL\ICLS CLIENT\;C:\PROGRAM FILES\INTEL\ICLS CLIENT\;%SYSTEMROOT%\SYSTEM32;%SYSTEMROOT%;%SYSTEMROOT%\SYSTEM32\WBEM;%SYSTEMROOT%\SYSTEM32\WINDOWSPOWERSHELL\V1.0\;C:\PROGRAM FILES\INTEL\INTEL(R) MANAGEMENT ENGINE COMPONENTS\DAL;C:\PROGRAM FILES\INTEL\INTEL(R) MANAGEMENT ENGINE COMPONENTS\IPT;C:\PROGRAM FILES (X86)\INTEL\INTEL(R) MANAGEMENT ENGINE COMPONENTS\DAL;C:\PROGRAM FILES (X86)\INTEL\INTEL(R) MANAGEMENT ENGINE COMPONENTS\IPT;C:\PROGRAM FILES\DELL\DELL DATA PROTECTION\ACCESS\ADVANCED\WAVE\GEMALTO\ACCESS CLIENT\V5\;C:\PROGRAM FILES (X86)\SECURITY INNOVATION\SI TSS\BIN\;C:\PROGRAM FILES (X86)\WINDOWS LIVE\SHARED;C:\Program Files\WIDCOMM\Bluetooth Software\;C:\Program Files\WIDCOMM\Bluetooth Software\syswow64;C:\Program Files (x86)\Microsoft ASP.NET\ASP.NET Web Pages\v1.0\;C:\Program Files (x86)\Microsoft SQL Server\100\Tools\Binn\;C:\Program Files\Microsoft SQL Server\100\Tools\Binn\;C:\Program Files\Microsoft SQL Server\100\DTS\Binn\;C:\Program Files (x86)\Microsoft SQL Server\100\Tools\Binn\VSShell\Common7\IDE\;C:\Program Files (x86)\Microsoft SQL Server\100\DTS\Binn\;C:\Program Files (x86)\Microsoft Visual Studio 9.0\Common7\IDE\PrivateAssemblies\;C:\Program Files\Intel\Intel(R) Management Engine Components\DAL;C:\Program Files\Intel\Intel(R) Management Engine Components\IPT;C:\Program Files (x86)\Intel\Intel(R) Management Engine Components\DAL;C:\Program Files (x86)\Intel\Intel(R) Management Engine Components\IPT;C:\Program Files\Intel\WiFi\bin\;C:\Program Files\Common Files\Intel\WirelessCommon\;C:\Program Files (x86)\Windows Kits\8.1\Windows Performance Toolkit\;C:\Program Files\Microsoft SQL Server\110\Tools\Binn\;C:\Program Files (x86)\LINQPad4;C:\Program Files (x86)\Microsoft SDKs\TypeScript\1.0\;C:\Program Files\Microsoft SQL Server\120\Tools\Binn\;C:\Program Files\Microsoft\Web Platform Installer\;C:\Program Files (x86)\Skype\Phone\;C:\Program Files\nodejs\;C:\Program Files (x86)\Git\cmd;C:\Program Files\TortoiseHg\";
	string[] pathArray = path.Split(';');
	pathArray.Dump();
	var distPathItems = pathArray.Distinct(StringComparer.OrdinalIgnoreCase);
	distPathItems.Dump();
	string newPath = string.Join(";", distPathItems);
	newPath.Dump();
	
	Console.WriteLine("path.Length = {0} newPath.Length = {1}", path.Length, newPath.Length);
}
