using BusinessObjects.Models;
using Repositories;
using System;
using System.Windows;
using System.Windows.Controls;

namespace MilkShop.Views.Staff.Control
{
    /// <summary>
    /// Interaction logic for BlogManagementControl.xaml
    /// </summary>
    public partial class BlogManagementControl : UserControl
    {
        private readonly IArticleRepository _repo = new ArticleRepository();
        private bool NewOrUpdate = false;

        public BlogManagementControl()
        {
            InitializeComponent();
        }

        private void ArticleControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadArticle();
        }

        private void LoadArticle()
        {
            var listArticle = _repo.GetAllArticles();
            dbArticle.ItemsSource = listArticle;
        }

        private void DbArticle_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dbArticle.SelectedItem is Article selectedArticle)
            {
                txtArticleID.Text = selectedArticle.ArticleId.ToString();
                txtTitle.Text = selectedArticle.Title;
                txtContent.Text = selectedArticle.Content;
                txtAuthorID.Text = selectedArticle.AuthorId.ToString();

                dpPublishDate.Text = selectedArticle.PublishDate != null ? selectedArticle.PublishDate.ToString() : string.Empty;
            }
            else
            {
                ClearTextbox();
            }
        }

        private void ClearTextbox()
        {
            txtArticleID.Text = string.Empty;
            txtTitle.Text = string.Empty;
            txtContent.Text = string.Empty;
            txtAuthorID.Text = string.Empty;
            dpPublishDate.Text = string.Empty;
        }

        private void EnableText(bool status)
        {
            txtTitle.IsEnabled = status;
            txtContent.IsEnabled = status;
            txtAuthorID.IsEnabled = status;
            dpPublishDate.IsEnabled = status;
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            if (btnNew.Content.ToString() == "New")
            {
                btnNew.Content = "Cancel";
                btnSave.IsEnabled = true;
                btnUpdate.IsEnabled = false;
                NewOrUpdate = true;

                ClearTextbox();
                EnableText(true);
            }
            else if (btnNew.Content.ToString() == "Cancel")
            {
                btnNew.Content = "New";
                btnSave.IsEnabled = false;
                btnUpdate.IsEnabled = true;
                NewOrUpdate = false;

                EnableText(false);
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (dbArticle.SelectedItem is Article selectedArticle)
            {
                if (btnUpdate.Content.ToString() == "Update")
                {
                    EnableText(true);
                    txtArticleID.IsEnabled = false;

                    btnUpdate.Content = "Cancel";
                    btnSave.IsEnabled = true;
                    NewOrUpdate = false;
                }
                else
                {
                    EnableText(false);
                    btnUpdate.Content = "Update";
                    btnSave.IsEnabled = false;
                }
            }
            else
            {
                MessageBox.Show("Please choose an article to update.", "Manage Article System",
                                MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text) || string.IsNullOrWhiteSpace(txtContent.Text) ||
                string.IsNullOrWhiteSpace(txtAuthorID.Text) || dpPublishDate.SelectedDate == null)
            {
                MessageBox.Show("Please fill in all fields with complete information.", "Article Management System", MessageBoxButton.OK,
                                MessageBoxImage.Information);
            }
            else
            {
                var article = new Article
                {
                    Title = txtTitle.Text,
                    Content = txtContent.Text,
                    PublishDate = dpPublishDate.SelectedDate.Value
                };

                // Convert AuthorId from string to integer
                if (int.TryParse(txtAuthorID.Text, out int authorId))
                {
                    article.AuthorId = authorId;

                    if (NewOrUpdate)
                    {
                        _repo.SaveArticle(article);
                        MessageBox.Show("New article has been added.", "Success", MessageBoxButton.OK,
                                        MessageBoxImage.Information);
                        btnNew.Content = "New";
                    }
                    else
                    {
                        if (int.TryParse(txtArticleID.Text, out int articleId))
                        {
                            article.ArticleId = articleId;
                            _repo.UpdateArticle(article);
                            MessageBox.Show("Article has been updated.", "Success", MessageBoxButton.OK,
                                            MessageBoxImage.Information);
                            btnUpdate.Content = "Update";
                        }
                        else
                        {
                            MessageBox.Show("Invalid article ID format. Please enter a valid integer.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }

                    EnableText(false);
                    btnSave.IsEnabled = false;
                    LoadArticle();
                }
                else
                {
                    MessageBox.Show("Invalid author ID format. Please enter a valid integer.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dbArticle.SelectedItem is Article selectedArticle)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this article?",
                                                          "Delete Confirmation", MessageBoxButton.YesNo,
                                                          MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    _repo.DeleteArticle(selectedArticle);
                    LoadArticle();
                }
            }
            else
            {
                MessageBox.Show("Please select an article to delete.", "Warning", MessageBoxButton.OK,
                                MessageBoxImage.Warning);
            }
        }
    }
}
