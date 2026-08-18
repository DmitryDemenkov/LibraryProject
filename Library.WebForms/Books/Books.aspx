<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Books.aspx.cs" Inherits="Library.WebForms.Books.Books" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Библиотека</title>

    <link href="../Content/bootstrap.min.css" rel="stylesheet" />
</head>

<body>

<form id="form1" runat="server">

    <div class="container">

        <h2 class="mt-4 mb-4">
            Книги
        </h2>

        <asp:GridView
            ID="BooksGrid"
            runat="server"
            AutoGenerateColumns="False"
            CssClass="table table-bordered table-hover"
            EmptyDataText="Книги отсутствуют"
            OnRowCommand="BooksGrid_RowCommand">

            <Columns>

                <asp:BoundField
                    DataField="Id"
                    HeaderText="ID" />

                <asp:BoundField
                    DataField="Title"
                    HeaderText="Название" />

                <asp:BoundField
                    DataField="Author"
                    HeaderText="Автор" />

                <asp:BoundField
                    DataField="PublishYear"
                    HeaderText="Год издания" />

                <asp:TemplateField
                    HeaderText="Действия">

                    <ItemTemplate>

                        <asp:HyperLink
                            ID="DetailsLink"
                            runat="server"
                            Text="Просмотр"
                            CssClass="btn btn-sm btn-info"
                            NavigateUrl='<%# "BookDetails.aspx?id=" + Eval("Id") %>' />

                        &nbsp;

                        <asp:Button
                            ID="DeleteButton"
                            runat="server"
                            Text="Удалить"
                            CssClass="btn btn-sm btn-danger"
                            CommandName="DeleteBook"
                            CommandArgument='<%# Eval("Id") %>'
                            OnClientClick="return confirm('Вы действительно хотите удалить эту книгу?');" />

                    </ItemTemplate>

                </asp:TemplateField>

            </Columns>

        </asp:GridView>

        <asp:Button
            ID="CreateButton"
            runat="server"
            Text="Добавить книгу"
            CssClass="btn btn-primary mb-3"
            OnClick="CreateButton_Click" />

    </div>

</form>

</body>
</html>
