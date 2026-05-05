using L_CAssignmentClasses.Interfaces;

namespace L_CAssignmentClasses.Transform
{
    public class FileReader:IFileReader
    {
        public List<string> Read(string path)
        {
            return File.Exists(path)
                ? new List<string>(File.ReadAllLines(path))
                : new List<string>();
        }
    }
}
