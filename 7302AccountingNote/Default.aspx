<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="_7302AccountingNote.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">

        <div>
            <asp:Label ID="lblFstRec" runat="server" Text="初次記帳"></asp:Label><br />
            <asp:Literal ID="ltFstRec" runat="server"></asp:Literal><br />
            <asp:Label ID="lblLstRec" runat="server" Text="最後記帳"></asp:Label><br />
            <asp:Literal ID="ltLstRec" runat="server"></asp:Literal><br />
            <asp:Label ID="lblTotalAcc" runat="server" Text="記帳數量"></asp:Label><br />
            <asp:Literal ID="ltTotalAcc" runat="server"></asp:Literal><br />
            <asp:Label ID="lblUserNum" runat="server" Text="會員數"></asp:Label><br />
            <asp:Literal ID="ltUserNum" runat="server"></asp:Literal><br />

            <asp:Button ID="btnLogin" runat="server" Text="登入系統" OnClick="btnLogin_Click" />
        </div>

    </form>
</body>
</html>
