<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<%@ Register Assembly="PageOffice, Version=3.0.0.1, Culture=neutral, PublicKeyToken=1d75ee5788809228"
    Namespace="PageOffice" TagPrefix="po" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <title>OfficeEditAspxIndex</title>
    <script runat="server">
    
        protected void PageOfficeCtrl1_Load(object sender, EventArgs e)
        {
            string filePath = Server.MapPath("/UploadFile/MBDoc/firereportmb_sc.doc");
            PageOfficeCtrl1.ServerPage = "/pageoffice/server.aspx"; //设置授权页面
            PageOfficeCtrl1.SetWriter(ViewData["doc"]); //实现数据填充
            PageOfficeCtrl1.SaveFilePage = "/OfficeManager/SaveDoc";   //处理文件保存
            PageOfficeCtrl1.WebOpen(filePath, OpenModeType.docAdmin, "Tom"); // 打开文件
        }
    </script>
</head>
<body>
    <div id="textcontent" style="width: 100%; height: 600px;">
        <!--**************   卓正 PageOffice组件 ************************-->
        <po:PageOfficeCtrl ID="PageOfficeCtrl1" runat="server"
            OnLoad="PageOfficeCtrl1_Load" CustomToolbar="False">
        </po:PageOfficeCtrl>
    </div>

</body>
</html>
