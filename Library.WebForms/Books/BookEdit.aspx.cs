using System;
using System.Configuration;
using Library.Core.Models;
using Library.Core.Data;

namespace Library.WebForms.Books
{
    public partial class BookEdit : System.Web.UI.Page
    {
        private BookRepository _repository;

        protected void Page_Load(object sender, EventArgs e)
        {
            _repository = new BookRepository(ConfigurationManager.ConnectionStrings["LibraryDb"].ConnectionString);

            if (!IsPostBack)
            {
                LoadBook();
            }
        }

        private void LoadBook()
        {
            if (!int.TryParse(Request.QueryString["id"], out int id))
            {
                Response.Redirect("Books.aspx");
                return;
            }

            Book book = _repository.GetById(id);

            if (book == null)
            {
                Response.Redirect("Books.aspx");
                return;
            }

            BookId.Value = book.Id.ToString();
            TitleTextBox.Text = book.Title;
            AuthorTextBox.Text = book.Author;
            GenreTextBox.Text = book.Genre;
            ISBNTextBox.Text = book.ISBN;
            YearTextBox.Text = book.PublishYear.ToString();
            DescriptionTextBox.Text = book.Description;

            initialContentsXml.Value = book.Contents ?? "";
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            if (!int.TryParse(BookId.Value, out int id))
            {
                ErrorLabel.Text = "Некорректный идентификатор книги.";
                return;
            }

            if (!int.TryParse( YearTextBox.Text, out int year))
            {
                ErrorLabel.Text = "Некорректный год издания.";
                return;
            }

            var book = new Book
            {
                Id = id,
                Title = TitleTextBox.Text,
                Author = AuthorTextBox.Text,
                Genre = GenreTextBox.Text,
                ISBN = ISBNTextBox.Text,
                PublishYear = year,
                Description = DescriptionTextBox.Text,
                Contents = contentsXml.Value
            };

            try
            {
                _repository.Update(book);

                Response.Redirect("BookDetails.aspx?id=" + id);
            }
            catch (Exception ex)
            {
                ErrorLabel.Text = "Ошибка при сохранении: " + ex.Message;
            }
        }
    }
}