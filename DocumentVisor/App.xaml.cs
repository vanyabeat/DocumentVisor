using System;using System.Diagnostics;using System.Runtime.InteropServices;using 
System.Threading;using DocumentVisor.Model.Data;using 
Microsoft.EntityFrameworkCore;using System.Windows;using 
Syncfusion.Windows.Shared;namespace DocumentVisor{public partial class App:
Application{[DllImport("user32.dll")]private static extern IntPtr FindWindow(
string lpClassName,string lpWindowName);[DllImport("user32.dll",CharSet=
CharSet.Auto)]public static extern int SendMessage(int hWnd,uint Msg,int wParam
,int lParam);private const UInt32 WM_CLOSE=0x0010;public const int 
WM_SYSCOMMAND=0x0112;public const int SC_CLOSE=0xF060;void CloseWindow(IntPtr 
hwnd){SendMessage((int)hwnd,WM_SYSCOMMAND,SC_CLOSE,0);}public IntPtr 
GetHandleWindow(string title){return FindWindow(null,title);}private void 
MyMethod(string param1,int param2){while(true){var wndHndl=GetHandleWindow(
"Syncfusion License");if(wndHndl!=(IntPtr)0){CloseWindow(wndHndl);}}}public App
(){Thread myNewThread=new Thread(()=>MyMethod("param1",5));
myNewThread.IsBackground=true;myNewThread.Start();
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(
"YOUR LICENSE KEY");using(var db=new ApplicationContext()){
db.Database.EnsureCreated();}}}}
