using System;
using System.Configuration;
using Library.Core.Models;
using Library.Core.Data;

namespace Library.WebForms.Books
{
    public partial class BookDetails : System.Web.UI.Page
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

            TitleLabel.Text = book.Title;
            AuthorLabel.Text = book.Author;
            GenreLabel.Text = book.Genre;
            ISBNLabel.Text = book.ISBN;
            PublishYearLabel.Text = book.PublishYear.ToString();
            DescriptionLabel.Text = book.Description;

            LoadContents(id);

            EditButton.NavigateUrl = "BookEdit.aspx?id=" + id;
        }

        private void LoadContents(int bookId)
        {
            var contents = _repository.GetContents(bookId);

            ContentsGrid.DataSource = contents;
            ContentsGrid.DataBind();
        }
    }
}