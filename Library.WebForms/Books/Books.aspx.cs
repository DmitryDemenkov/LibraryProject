using System;
using System.Configuration;
using System.Web.UI.WebControls;
using Library.Core.Data;

namespace Library.WebForms.Books
{
    public partial class Books : System.Web.UI.Page
    {
        private BookRepository _repository;

        protected void Page_Load(object sender, EventArgs e)
        {
            _repository = new BookRepository(ConfigurationManager.ConnectionStrings["LibraryDb"].ConnectionString);

            if (!IsPostBack)
            {
                LoadBooks();
            }
        }

        private void LoadBooks()
        {
            var books = _repository.GetAll();

            BooksGrid.DataSource = books;
            BooksGrid.DataBind();
        }



        protected void CreateButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("BookCreate.aspx");
        }

        protected void BooksGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName != "DeleteBook")
                return;

            if (!int.TryParse(e.CommandArgument.ToString(), out int id))
            {
                return;
            }

            _repository.Delete(id);

            LoadBooks();
        }
    }
}