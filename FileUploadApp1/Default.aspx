<%@ Page Title="Home Page" Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="FileUploadApp1._Default" %>

<head>

    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.0/jquery.min.js" 

  type="text/javascript"></script> 
    </head>

<html>
<body>
    <form id="form1" method ="post" enctype="multipart/form-data" runat="server">    
    <div>
        <h1>File Upload</h1>
        <input id="selectedFile" type="file" name="selectedFile" runat ="server"/>   
        <p><asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click"  />        
            </p> 
        <asp:Label ID="lblFileStatus" runat="server" Text=""  />     
         <input type="hidden" id="hidRenamedFile" runat="server" /> 
        </div>
        </form>
   </body>
    </html>
      
   <script type="text/javascript">
       function renameFile() {
           var filename = prompt("Please enter your name");
           if (filename != null) {
               $("#hidRenamedFile").val(filename);
           }
       }
    </script>
