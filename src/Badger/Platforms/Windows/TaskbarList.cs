using System.Runtime.InteropServices;

namespace ShyFoxStudio.Badger.Platforms.Windows;


// TaskbarList interface
// C:\Program Files (x86)\Windows Kits\10\Include\[version]\um\ShObjldl_core.h
[ComImport()]
[Guid("56fdf344-fd6d-11d0-958a-006097c9a090")]
[ClassInterface(ClassInterfaceType.None)]
internal class TaskbarList { }
