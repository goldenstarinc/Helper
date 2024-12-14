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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private PostsLoader _postsLoader;
        public MainWindow()
        {
            InitializeComponent();
            CurrentSession.CurrentUserId = 17;
            _postsLoader = new PostsLoader();
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
    }
}
