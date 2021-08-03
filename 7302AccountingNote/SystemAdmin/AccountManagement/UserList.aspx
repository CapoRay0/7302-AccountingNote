<%@ Page Title="" Language="C#" MasterPageFile="~/SystemAdmin/SystemAdmin.Master" AutoEventWireup="true" CodeBehind="UserList.aspx.cs" Inherits="_7302AccountingNote.SystemAdmin.AccountManagement.UserList" %>
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
                        <asp:BoundField HeaderText="帳號" />
                        <asp:BoundField HeaderText="姓名" />
                        <asp:BoundField HeaderText="Email" />
                        <asp:BoundField HeaderText="等級" />
                        <asp:BoundField HeaderText="建立時間" />
                        <asp:TemplateField HeaderText="編輯"></asp:TemplateField>
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

                <%--<asp:Literal ID="ltpager" runat="server"></asp:Literal>--%>

                <%--<uc1:ucpager runat="server" ID="ucPager" PageSize="10" CurrentPage="1" TotalSize="10" Url="AccountingList.aspx" />--%>
                <asp:PlaceHolder ID="plcNoUserData" runat="server" Visible="false">
                    <p style="color: red; background-color: cornflowerblue">
                        無會員資料
                    </p>
                </asp:PlaceHolder>
                
            </td>
        </tr>
    </table>
</asp:Content>
