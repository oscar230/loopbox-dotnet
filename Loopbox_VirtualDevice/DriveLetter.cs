using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Loopbox_VirtualDevice
{
    public class DriveLetter
    {
        private const int alphabet_limit = 26;
        public char letter;
        public DriveLetter()
        {
            letter = FreeDriverLetter();
            Debug.WriteLine("Free letter (" + letter + ":)");
        }

        public char Letter { get => letter; }

        // Credits to user Tawnos at hardforum.com
        // Source: https://hardforum.com/threads/c-getting-free-drive-letters.1208456/
        private char FreeDriverLetter()
        {
            var comboDrives = new List<char>();
            ArrayList driveLetters = new ArrayList(alphabet_limit); // Allocate space for alphabet
            for (int i = 65; i < 91; i++) // increment from ASCII values for A-Z
            {
                driveLetters.Add(Convert.ToChar(i)); // Add uppercase letters to possible drive letters
            }

            foreach (string drive in Directory.GetLogicalDrives())
            {
                driveLetters.Remove(drive[0]); // removed used drive letters from possible drive letters
            }

            foreach (char drive in driveLetters)
            {
                comboDrives.Add(drive); // add unused drive letters to the combo box
            }
            return comboDrives.LastOrDefault();
        }
    }
}
