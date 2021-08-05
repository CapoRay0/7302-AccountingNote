<%@ Page Title="" Language="C#" MasterPageFile="~/SystemAdmin/SystemAdmin.Master" AutoEventWireup="true" CodeBehind="UserList.aspx.cs" Inherits="_7302AccountingNote.SystemAdmin.AccountManagement.UserList" %>

<%@ Register Src="~/UserControls/Pager.ascx" TagPrefix="uc1" TagName="Pager" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
        <tr>
            <td colspan="2">
                <h3>會員管理</h3>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnCreate" runat="server" Text="Add" OnClick="btnCreate_Click" />
                <asp:GridView ID="gvUserList" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField HeaderText="帳號" DataField="Account" />
                        <asp:BoundField HeaderText="姓名" DataField="Name" />
                        <asp:BoundField HeaderText="Email" DataField="Email" />
                        <asp:TemplateField HeaderText="等級">
                            <ItemTemplate>
                                <%# ((int)Eval("UserLevel") == 0) ? "管理者" : "一般會員" %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="建立時間" DataField="CreateDate" />
                        <asp:TemplateField HeaderText="編輯">
                            <ItemTemplate>
                                <a href="/SystemAdmin/AccountManagement/UserDetail.aspx?UID=<%# Eval("UID") %>">修改</a>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EditRowStyle BackColor="#2461BF" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EFF3FB" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                    <SortedDescendingHeaderStyle BackColor="#4870BE" />

                </asp:GridView>

                <uc1:Pager runat="server" ID="Pager" PageSize="5" Url="/SystemAdmin/AccountManagement/UserList.aspx" />

                <asp:PlaceHolder ID="plcNoUserData" runat="server" Visible="false">
                    <p style="color: red; background-color: cornflowerblue">
                        無會員資料
                    </p>
                </asp:PlaceHolder>
                <asp:Literal ID="ltmsg" runat="server"></asp:Literal>

            </td>
        </tr>
    </table>
</asp:Content>
