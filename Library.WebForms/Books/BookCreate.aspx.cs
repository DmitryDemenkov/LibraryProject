using System;
using System.Configuration;
using Library.Core.Models;
using Library.Core.Data;

namespace Library.WebForms.Books
{
    public partial class BookCreate : System.Web.UI.Page
    {
        private BookRepository _repository;

        protected void Page_Load(object sender, EventArgs e)
        {
            _repository = new BookRepository(ConfigurationManager.ConnectionStrings["LibraryDb"].ConnectionString);
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            if (!int.TryParse(YearTextBox.Text, out int year))
            {
                ErrorLabel.Text = "Некорректный год.";
                return;
            }

            var book = new Book
            {
                Title = TitleTextBox.Text,
                Author = AuthorTextBox.Text,
                PublishYear = year,
                Genre = GenreTextBox.Text,
                ISBN = ISBNTextBox.Text,
                Description = DescriptionTextBox.Text,
                Contents = contentsXml.Value
            };

            try
            {
                _repository.Insert(book);
                Response.Redirect("Books.aspx");
            }
            catch (Exception ex)
            {
                ErrorLabel.Text = "Ошибка при сохранении: " + ex.Message;
            }
        }
    }
}