<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LoginPage.aspx.cs" Inherits="Electricity_Billing_Project.LoginPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server">
    <div>
        <br />
        <br />
        &nbsp;&nbsp;&nbsp;
            Login Name  :  &nbsp; &nbsp; &nbsp; 
            <asp:TextBox ID="txtuser" runat="server"></asp:TextBox>
        <br />
        <br />
        &nbsp;&nbsp;&nbsp;
            Password : &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
            <asp:TextBox ID="txtpass" runat="server"></asp:TextBox>
        <br />
        <br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="loginBtn" runat="server" Text="Login" BackColor="CornflowerBlue" ForeColor="White" OnClick="loginBtn_Click" BorderColor="CornflowerBlue" />
        <br />
        <br />
        &nbsp;&nbsp;&nbsp;
            <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label>
    </div>
</asp:Content>
