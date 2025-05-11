using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CurrentSession = CurrentSessionDataProvider.CurrentSessionDataProvider;
using PostsLoader = PostingService.PostsLoader;
using PostFields = PostingService.PostFields;

namespace GoodDeeds_Interface
{
    /// <summary>
    /// Логика взаимодействия для PostsPage.xaml
    /// </summary>
    public partial class PostsPage : Page
    {
        private PostsLoader _postsLoader;
        public PostsPage()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            CurrentSession.CurrentUserId = 17;
            _postsLoader = new PostsLoader();
        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadPosts();
        }
        private void LoadPosts()
        {
            List<PostFields> posts = _postsLoader.LoadPosts();
            PostsListView.ItemsSource = posts;
        }
        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            LoadPosts();
        }
        private void RespondButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                button.Content = "Вы откликнулись!";
                button.IsEnabled = false;
                button.Foreground = new SolidColorBrush(Colors.Black);
                button.Background = new SolidColorBrush(Color.FromRgb(200, 200, 200));
            }
        }
    }
}
