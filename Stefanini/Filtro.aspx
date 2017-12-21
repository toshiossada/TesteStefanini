<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Filtro.aspx.cs" Inherits="Stefanini._Filtro" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

  <link rel="stylesheet" href="https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
  <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
  
  <script>
      $(function () {

          $.datepicker.regional['pt'] = {
              closeText: 'Fechar',
              prevText: '<Anterior',
              nextText: 'Seguinte',
              currentText: 'Hoje',
              monthNames: ['Janeiro', 'Fevereiro', 'Mar&ccedil;o', 'Abril', 'Maio', 'Junho',
              'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'],
              monthNamesShort: ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun',
              'Jul', 'Ago', 'Set', 'Out', 'Nov', 'Dez'],
              dayNames: ['Domingo', 'Segunda-feira', 'Ter&ccedil;a-feira', 'Quarta-feira', 'Quinta-feira', 'Sexta-feira', 'S&aacute;bado'],
              dayNamesShort: ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'S&aacute;b'],
              dayNamesMin: ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'S&aacute;b'],
              weekHeader: 'Sem',
              dateFormat: 'dd/mm/yy',
              firstDay: 0,
              isRTL: false,
              showMonthAfterYear: false,
              yearSuffix: ''
          };
          $.datepicker.setDefaults($.datepicker.regional['pt']);
          $(".data").datepicker();


  });
  </script>

    <style>
        input #btnSair{
            padding:10px;
        }
        .filtros input, select {
            width: 100%;
        }

        .filtros .row {
            padding: 5px;
        }
    </style>
    <div class="row" style="padding:10px;">
        <asp:Button ID="btnSair" runat="server" Text="Sair" OnClick="btnSair_Click"/>
    </div>
    <div class="filtros container-fluid mt-2">
        <div class="col-md-10">
            <div class="row">
                <div class="col-md-5">
                    <div class="col-md-4">
                        <asp:Label ID="lblName" runat="server" Text="Name"></asp:Label>
                    </div>
                    <div class="col-md-6">
                        <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-5">
                    <div class="col-md-4">
                        <asp:Label ID="lblGender" runat="server" Text="Gender"></asp:Label>
                    </div>
                    <div class="col-md-6">
                        <asp:DropDownList ID="lstGender" runat="server">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-5">
                    <div class="col-md-4">
                        <asp:Label ID="lblCity" runat="server" Text="City"></asp:Label>
                    </div>
                    <div class="col-md-6">
                        <asp:DropDownList ID="lstCity" runat="server"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-5">
                    <div class="col-md-4">
                        <asp:Label ID="lblRegion" runat="server" Text="Region"></asp:Label>
                    </div>
                    <div class="col-md-6">
                        <asp:DropDownList ID="lstRegion" runat="server"></asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-5">
                    <div class="col-md-4">
                        <asp:Label ID="lblLastPurchase" runat="server" Text="Last Purchase "></asp:Label>
                    </div>
                    <div class="col-md-6">
                        <asp:TextBox ID="txtLastPurchase" CssClass="data" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-5">
                    <div class="col-md-4">
                        <asp:Label ID="lblUntil" runat="server" Text="Until"></asp:Label>
                    </div>
                    <div class="col-md-6">
                        <asp:TextBox ID="txtUntil" CssClass="data" runat="server"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-5">
                    <div class="col-md-4">
                        <asp:Label ID="lblClassification" runat="server" Text="Classification"></asp:Label>
                    </div>
                    <div class="col-md-6">
                        <asp:DropDownList ID="lstClassification" runat="server"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-5">
                    <div class="col-md-4">
                        <asp:Label ID="lblSeller" runat="server" Text="Seller"></asp:Label>
                    </div>
                    <div class="col-md-6">
                        <asp:DropDownList ID="lstSeller" runat="server"></asp:DropDownList>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-2">
            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
            <asp:Button ID="btnClear" runat="server" Text="Clear Fields" OnClick="btnClear_Click" />
        </div>
    </div>
    <div class="row">
        <asp:GridView ID="gvResult" AutoGenerateColumns="false" CssClass="table table-striped" runat="server">
            <Columns>
                <asp:BoundField DataField="Classification" HeaderText="Classification" />
                <asp:BoundField DataField="Name" HeaderText="Name" />
                <asp:BoundField DataField="Phone" HeaderText="Phone" />
                <asp:BoundField DataField="Gender" HeaderText="Gender" />
                <asp:BoundField DataField="City" HeaderText="City" />
                <asp:BoundField DataField="Region" HeaderText="Region" />
                <asp:BoundField DataField="LastPurchase" HeaderText="Last Purchase" />
                <asp:BoundField DataField="Seller" HeaderText="Seller" />
            </Columns>
        </asp:GridView>
    </div>

</asp:Content>
