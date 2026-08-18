<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BookDetails.aspx.cs" Inherits="Library.WebForms.Books.BookDetails" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Карточка книги</title>

    <link href="../Content/bootstrap.min.css" rel="stylesheet" />
</head>

<body>

<form id="form1" runat="server">

    <div class="container">

        <h2 class="mt-4 mb-4">
            Карточка книги
        </h2>

        <div class="card">

            <div class="card-body">

                <div class="row mb-3">
                    <div class="col-md-3 font-weight-bold">
                        Название:
                    </div>

                    <div class="col-md-9">
                        <asp:Label
                            ID="TitleLabel"
                            runat="server" />
                    </div>
                </div>


                <div class="row mb-3">
                    <div class="col-md-3 font-weight-bold">
                        Автор:
                    </div>

                    <div class="col-md-9">
                        <asp:Label
                            ID="AuthorLabel"
                            runat="server" />
                    </div>
                </div>


                <div class="row mb-3">
                    <div class="col-md-3 font-weight-bold">
                        Жанр:
                    </div>

                    <div class="col-md-9">
                        <asp:Label
                            ID="GenreLabel"
                            runat="server" />
                    </div>
                </div>


                <div class="row mb-3">
                    <div class="col-md-3 font-weight-bold">
                        ISBN:
                    </div>

                    <div class="col-md-9">
                        <asp:Label
                            ID="ISBNLabel"
                            runat="server" />
                    </div>
                </div>


                <div class="row mb-3">
                    <div class="col-md-3 font-weight-bold">
                        Год издания:
                    </div>

                    <div class="col-md-9">
                        <asp:Label
                            ID="PublishYearLabel"
                            runat="server" />
                    </div>
                </div>


                <div class="row mb-4">
                    <div class="col-md-3 font-weight-bold">
                        Описание:
                    </div>

                    <div class="col-md-9">
                        <asp:Label
                            ID="DescriptionLabel"
                            runat="server" />
                    </div>
                </div>

            </div>

        </div>


        <h4 class="mt-4 mb-3">
            Оглавление
        </h4>

        <asp:GridView
            ID="ContentsGrid"
            runat="server"
            AutoGenerateColumns="False"
            CssClass="table table-bordered table-hover"
            EmptyDataText="Оглавление отсутствует">

            <Columns>

                <asp:BoundField
                    DataField="Number"
                    HeaderText="№" />

                <asp:BoundField
                    DataField="Title"
                    HeaderText="Название" />

                <asp:BoundField
                    DataField="Page"
                    HeaderText="Страница" />

            </Columns>

        </asp:GridView>


        <div class="mt-4">

            <asp:HyperLink
                ID="EditButton"
                runat="server"
                Text="Редактировать"
                CssClass="btn btn-warning" />

            <asp:HyperLink
                ID="BackButton"
                runat="server"
                Text="Назад"
                NavigateUrl="Books.aspx"
                CssClass="btn btn-secondary ml-2" />

        </div>

    </div>

</form>

</body>
</html>