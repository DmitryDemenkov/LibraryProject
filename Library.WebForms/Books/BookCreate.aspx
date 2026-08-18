<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BookCreate.aspx.cs" Inherits="Library.WebForms.Books.BookCreate" ValidateRequest="false"%>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Добавление книги</title>

    <link href="../Content/bootstrap.min.css" rel="stylesheet" />
</head>

<body>

<form id="form1" runat="server">

    <div class="container">

        <h2 class="mt-4 mb-4">
            Добавление книги
        </h2>

        <div class="form-group">
            <label>Название</label>

            <asp:TextBox
                ID="TitleTextBox"
                runat="server"
                CssClass="form-control" />

            <asp:RequiredFieldValidator
                ID="TitleValidator"
                runat="server"
                ControlToValidate="TitleTextBox"
                ErrorMessage="Введите название книги"
                CssClass="text-danger"
                Display="Dynamic" />
        </div>

        <div class="form-group">
            <label>Автор</label>

            <asp:TextBox
                ID="AuthorTextBox"
                runat="server"
                CssClass="form-control" />

            <asp:RequiredFieldValidator
                ID="AuthorValidator"
                runat="server"
                ControlToValidate="AuthorTextBox"
                ErrorMessage="Введите автора"
                CssClass="text-danger"
                Display="Dynamic" />
        </div>

        <div class="form-group">
            <label>Год издания</label>

            <asp:TextBox
                ID="YearTextBox"
                runat="server"
                CssClass="form-control"
                TextMode="Number" />

            <asp:RequiredFieldValidator
                ID="YearRequiredValidator"
                runat="server"
                ControlToValidate="YearTextBox"
                ErrorMessage="Введите год издания"
                CssClass="text-danger"
                Display="Dynamic" />

            <asp:CompareValidator
                ID="YearValidator"
                runat="server"
                ControlToValidate="YearTextBox"
                Operator="DataTypeCheck"
                Type="Integer"
                ErrorMessage="Год должен быть целым числом"
                CssClass="text-danger"
                Display="Dynamic" />
        </div>

        <div class="form-group">
            <label>Жанр</label>

            <asp:TextBox
                ID="GenreTextBox"
                runat="server"
                CssClass="form-control" />
        </div>

        <div class="form-group">
            <label>ISBN</label>

            <asp:TextBox
                ID="ISBNTextBox"
                runat="server"
                CssClass="form-control" />
        </div>

        <div class="form-group">
            <label>Описание</label>

            <asp:TextBox
                ID="DescriptionTextBox"
                runat="server"
                CssClass="form-control"
                TextMode="MultiLine"
                Rows="5" />
        </div>

        <div class="form-group mt-4">

            <label>Оглавление</label>

            <table class="table table-bordered">
                <thead>
                    <tr>
                        <th style="width: 80px;">№</th>
                        <th>Название</th>
                        <th style="width: 150px;">Страница</th>
                        <th style="width: 100px;"></th>
                    </tr>
                </thead>

                <tbody id="chapters-container">
                </tbody>
            </table>

            <button
                type="button"
                id="add-chapter"
                class="btn btn-secondary">
                Добавить главу
            </button>

            <asp:HiddenField
                ID="initialContentsXml"
                runat="server" />

            <asp:HiddenField
                ID="contentsXml"
                runat="server" />

        </div>

        <div class="form-group mt-4">

            <asp:Button
                ID="SaveButton"
                runat="server"
                Text="Создать"
                CssClass="btn btn-primary"
                OnClick="SaveButton_Click" />

            <asp:HyperLink
                ID="CancelButton"
                runat="server"
                Text="Отмена"
                NavigateUrl="Books.aspx"
                CssClass="btn btn-secondary ml-2" />

        </div>

        <asp:Label
            ID="ErrorLabel"
            runat="server"
            CssClass="text-danger" />

    </div>

</form>

</body>

<script src="../Scripts/jquery-3.4.1.min.js"></script>
<script src="../Scripts/XmlEditor/contents-editor.js"></script>

</html>