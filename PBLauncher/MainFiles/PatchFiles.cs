using System.Collections.Generic;

namespace PBLauncher
{
     
     
    public class PatchFiles
    {
        public static Dictionary<string, string> Patch_Files = new Dictionary<string, string>();
        public static string Patch, URL;
    }

    public class UserFilePatch
    {
        public static Dictionary<string, string> UserPatchFile = new Dictionary<string, string>();
        public static string Patch, URL;
    }
}
