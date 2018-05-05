using System;
using System.Web.UI;
using System.Web;
using System.IO;

namespace FileUploadApp1
{
    public partial class _Default : Page
    {
        const int BYTES_TO_READ = sizeof(Int64);
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        
        
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            HttpPostedFile file = Request.Files["selectedFile"];
            if (file != null && file.ContentLength > 0)
            {
                string fname = Path.GetFileName(file.FileName);
                //  string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                // file.SaveAs(Server.MapPath(Path.Combine(desktop+"\\newfile", fname)));
                string folder_path = Server.MapPath("~\\newfile\\");

                if (!Directory.Exists(folder_path))  // if it doesn't exist, create
                    Directory.CreateDirectory(folder_path);
                var newfilePath = folder_path + fname;

                var isSameFileName = File.Exists(newfilePath);

                var isSameContent = false;
                if (isSameFileName)
                {
                    FileInfo oldfile = new FileInfo(newfilePath);
                    FileInfo newfile = new FileInfo(file.FileName);
                    isSameContent = FilesAreEqual(oldfile, file);
                }
                //if (isSameContent)
                //    lblFileStatus.Text = "File already present";

                if (isSameFileName && isSameContent)
                    lblFileStatus.Text = "File already present";
                else if (isSameFileName && !isSameContent)
                {

                    lblFileStatus.Text = "Rename file";
                //    ScriptManager.RegisterStartupScript(Page, this.GetType(), "Renamefile", "alert('dgg')", false);
                //   fname = hidRenamedFile.Value;
                   // file.SaveAs(folder_path + fname);
                }
                else if (!isSameFileName && isSameContent)
                    lblFileStatus.Text = "Do you wanna overwrite";
                else
                    file.SaveAs(folder_path + fname);
            }
        }

        static bool FilesAreEqual(FileInfo first, HttpPostedFile second)
        {
            if (first.Length != second.ContentLength)
                return false;

            if (string.Equals(first.FullName, second.FileName, StringComparison.OrdinalIgnoreCase))
                return true;

            int iterations = (int)Math.Ceiling((double)first.Length / BYTES_TO_READ);
            
            using (FileStream fs1 = first.OpenRead())
         //   using (FileStream fs2 = second.InputStream. as FileStream )
            {
                byte[] one = new byte[BYTES_TO_READ];
                byte[] two = new byte[BYTES_TO_READ];

                for (int i = 0; i < iterations; i++)
                {
                    fs1.Read(one, 0, BYTES_TO_READ);
                    second.InputStream.Read(two, 0, BYTES_TO_READ);

                    if (BitConverter.ToInt64(one, 0) != BitConverter.ToInt64(two, 0))
                        return false;
                }
            }

            return true;
        }

    }
}