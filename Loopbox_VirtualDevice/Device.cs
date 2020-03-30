using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Loopbox_VirtualDevice
{
    public class Device
    {
        private const string _virtualdevice_directory_base = "virtualdevices";
        private DirectoryInfo virtualdevice_directory;
        private string devicename;
        private DriveLetter driveLetter;
        private Mirror mirror;
        private bool created;
        public Device(string devicename)
        {
            this.devicename = devicename;
            virtualdevice_directory = new DirectoryInfo(_virtualdevice_directory_base + devicename + new Random().GetHashCode());
            try
            {
                mirror = new Mirror(virtualdevice_directory);
                driveLetter = mirror.Create();
            }
            catch (Exception e)
            {
                Debug.WriteLine("Device creation failed: " + e);
                created = false;
            }
        }
        public DirectoryInfo GetVirtualDeviceStorageDirectory { get => virtualdevice_directory; }
        public string DeviceName { get => devicename; }
        public bool DeviceExists { get => mirror != null && created; }
        public override string ToString() => "Directory: " + virtualdevice_directory.FullName;
    }
}
