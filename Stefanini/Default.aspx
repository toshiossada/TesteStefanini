<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Stefanini._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid mt-2">
        <div class="row" style="text-align: center">
            <div class="row">
                <div class="col-12">
                    E-mail :
                    <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-12">
                    Senha :
                    <asp:TextBox ID="txtSenha" runat="server" TextMode="Password"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-12">
                    <asp:Button ID="btnEntrar" runat="server" Text="Entrar" OnClick="btnEntrar_Click" />
                </div>
            </div>
        </div>
    </div>

</asp:Content>
