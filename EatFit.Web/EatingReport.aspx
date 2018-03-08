<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EatingReport.aspx.cs" Inherits="EatingReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>EatFit - Eating Report</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <rsweb:reportviewer id="ReportViewer1" runat="server" font-names="Verdana" font-size="8pt"
            height="650px" processingmode="Remote" width="1000px">
        <%--<ServerReport ReportServerUrl="http://flexo/reportserver" ReportPath="/EatFit/Eating Sessions by User Report"></ServerReport>

--%>
            <ServerReport></ServerReport>
        </rsweb:reportviewer>
    </div>
    </form>
</body>
</html>
