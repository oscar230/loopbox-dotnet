using System.IO;
using System.Diagnostics;
using System.Threading;

namespace Loopbox_VirtualDevice
{
    public class Mirror
    {
        private const string _dokansetup_filename = "DokanSetup_redist.exe";
        private const string _mirror64_filename = "mirror64.exe";
        private const string _redist_directory = "Redist/";
        private const string _dokansetup = _redist_directory + _dokansetup_filename;
        private const string _mirror64 = _redist_directory + _mirror64_filename;
        private const int _allocation_size = 512;
        private Process process;
        private DirectoryInfo directory;
        private DriveLetter driveLetter;

        public bool Ok() => !new FileInfo(_dokansetup).Exists ? false : !new FileInfo(_mirror64).Exists ? false : true;

        public Mirror(DirectoryInfo directory)
        {
            this.directory = directory;
            this.directory.Create();
            driveLetter = new DriveLetter();
            Debug.WriteLine("Mirror process initiated with target directory for virtual device (" + driveLetter.Letter + ":) at: " + directory.FullName);
        }

        public DriveLetter Create()
        {
            // .\mirror.exe /r test /l m /m
            // /r root folder
            // /l driver letter
            // /m make removable device
            string program = "powershell.exe";
            string arguments = " \"" + new DirectoryInfo(_mirror64).FullName + "\" /r \"" + directory.FullName + "\" /l " + driveLetter.Letter + " /m /d /a " + _allocation_size;
            Debug.WriteLine("Creating mirror.exe command: \n\t" + program + "\n\t" + arguments);
            process = ExecuteAsAdmin(program, arguments);
            process.Start();
            Debug.WriteLine("Process started with id: " + process.Id);
            return driveLetter;
        }

        public void Destroy(DriveLetter driveLetter, DirectoryInfo directory)
        {
            process.Kill();
        }
        private Process ExecuteAsAdmin(string program, string arguments)
        {
            Process proc = new Process();
            proc.StartInfo.FileName = program;
            proc.StartInfo.Arguments = arguments;
            proc.StartInfo.UseShellExecute = true;
            proc.StartInfo.Verb = "runas";
            return proc;
        }
    }
}
