<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm3.aspx.cs" Inherits="LoanPricerWebBased.WebForm3" %>

<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
<script src="http://code.jquery.com/jquery-1.10.1.min.js"></script>
    <script type="text/javascript">
       
        //$('a#someID').attr({
        //    target: '_blank',
        //    href: 'http://localhost/directory/file.pdf'
        //});

        function downloadURL(url) {
        
            var hiddenIFrameID = 'hiddenDownloader',
                iframe = document.getElementById(hiddenIFrameID);
            if (iframe === null) {
                
                iframe = document.createElement('iframe');
                iframe.id = hiddenIFrameID;
                iframe.style.display = 'none';
                document.body.appendChild(iframe);
                return false;
            }
            iframe.src = url;
        };
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <a id="test" href="#" onclick="downloadURL('http://localhost/vonajobs/source/upload/resume/635242810326992843.doc');">This is a sample download url</a>
        </div>
    </form>
</body>
</html>
