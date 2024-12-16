using SocialMediaClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using Msg = SocialMediaClient.Message;



namespace SocialMediaDesktopClient
{
    public partial class Form1 : Form
    {
        private PostService postService;
        private List<Post> posts;
        private UserService userService;
        private CommentService commentService;
        private System.Windows.Forms.TabControl tabControl1;


        public Form1()
        {
            InitializeComponent();
            postService = new PostService(); 
            posts = new List<Post>();
            userService = new UserService();
            commentService = new CommentService();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.Size = new System.Drawing.Size(400, 300);
            this.tabControl1.TabIndex = 0;
            this.Controls.Add(this.tabControl1);

        }


        private async void button6_Click(object sender, EventArgs e)
        {
            string userName = textUserName.Text;
            string postTitle = textPostTitle.Text;
            if (!LoggedInUser.IsLoggedIn)
            {
                Form2 loginForm = new Form2();

                loginForm.ShowDialog();

                if (!LoggedInUser.IsLoggedIn)
                {
                    MessageBox.Show("You must log in to comment.");
                    return;
                }
            }

          
                if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(postTitle))
                {
                    MessageBox.Show("Please fill in all fields!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                var posts = await postService.GetPostsByTitleAndUserAsync(postTitle, userName);

                if (posts == null || posts.Count == 0)
                {
                    MessageBox.Show("No posts found for this user and title.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (posts.Count > 1)
                {
                    var dates = posts.Select(p => p.CreatedOn.ToString("yyyy-MM-dd HH:mm:ss")).ToArray();
                    string selectedDate = ShowDateSelectionDialog(dates);

                    if (string.IsNullOrEmpty(selectedDate))
                    {
                        MessageBox.Show("Action cancelled.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }


                    var selectedPost = posts.FirstOrDefault(p => p.CreatedOn.ToString("yyyy-MM-dd HH:mm:ss") == selectedDate);
                    if (selectedPost != null)
                    {
                        PostComment(selectedPost);
                    }
                }
                else
                {

                    PostComment(posts[0]);
                }
            
        }
        private void PostComment(Post post)
        {
            MessageBox.Show($"Comment added to post: {post.Title}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private string ShowDateSelectionDialog(string[] dates)
        {
            using (var form = new Form())
            {
                form.Text = "Select Post Date";
                var listBox = new ListBox { Dock = DockStyle.Fill, DataSource = dates };
                var btnOk = new Button { Text = "OK", Dock = DockStyle.Bottom };
                var btnCancel = new Button { Text = "Cancel", Dock = DockStyle.Bottom };

                form.Controls.Add(listBox);
                form.Controls.Add(btnOk);
                form.Controls.Add(btnCancel);

                string selectedDate = null;

                btnOk.Click += (sender, e) =>
                {
                    selectedDate = listBox.SelectedItem?.ToString();
                    form.DialogResult = DialogResult.OK;
                    form.Close();
                };

                btnCancel.Click += (sender, e) =>
                {
                    form.DialogResult = DialogResult.Cancel;
                    form.Close();
                };

                return form.ShowDialog() == DialogResult.OK ? selectedDate : null;
            }
        }

        private async void btnSearchPost_Click(object sender, EventArgs e)
        {
            string username = txtTitlePostUsers.Text;

            if (string.IsNullOrWhiteSpace(username))
            {
                MessageBox.Show("Please enter a username.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var posts = await postService.GetPostsByUserAsync(username);

            if (posts == null || posts.Count == 0)
            {
                MessageBox.Show($"No posts found for user '{username}'.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                txtPostUsers.Clear();
                foreach (var post in posts)
                {
                    txtPostUsers.AppendText(post.ToString() + Environment.NewLine);
                }
            }
        }

        private async void btnSearchUser_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(textNameUser.Text))
            {
                MessageBox.Show("Please enter a username!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var userService = new UserService();
            var users = await userService.GetUsersByNameAsync(textNameUser.Text);

            if (users == null || users.Count == 0)
            {
                textUsers.Text = "No users found with this name.";
            }
            else
            {
                textUsers.Clear();
                foreach (var user in users)
                {
                    textUsers.AppendText($"Name: {user.Name}, Email: {user.Email}{Environment.NewLine}");
                }
            }
        }
        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (!LoggedInUser.IsLoggedIn && (e.TabPage == tabCreatePost || e.TabPage == tabProfile))
            {
                e.Cancel = true;
                Form2 loginForm = new Form2();

                loginForm.ShowDialog();

                if (!LoggedInUser.IsLoggedIn)
                {
                    MessageBox.Show("You must log in to access this section.");
                }
            }
        }

        private async void btnCreatePost_Click(object sender, EventArgs e)
        {
            if (Global.LoggedInUser == null)
            {
                MessageBox.Show("You must be logged in to create a post.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string title = textTitlePost.Text.Trim();
            string message = textMessage.Text.Trim();

            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(message))
            {
                MessageBox.Show("Please enter both Title and Message.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var newPost = new Post
            {
                Title = title,
                Content = message,
                UserId = Global.LoggedInUser.Id,
                CreatedOn = DateTime.Now,
                Status = Post.PostStatus.PENDING


            };


            bool success = await postService.CreatePostAsync(newPost);

            if (success)
            {
                MessageBox.Show("Post created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textTitlePost.Clear();
                textMessage.Clear();
            }
            else
            {
                MessageBox.Show("Failed to create post. Try again later.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string recipientName =textBox1.Text.Trim();
            string recipientEmail = textBox3.Text.Trim();
            string messageContent = textBox2.Text.Trim();

            if (string.IsNullOrWhiteSpace(recipientName) || string.IsNullOrWhiteSpace(recipientEmail) || string.IsNullOrWhiteSpace(messageContent))
            {
                MessageBox.Show("Please fill in all fields!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

 
            var users = await userService.GetUsersByNameAndEmailAsync(recipientName, recipientEmail);
            if (users == null || users.Count == 0)
            {
                MessageBox.Show("No user found with the given name and email.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var recipient = users.First();

    
            var newMessage = new Msg
            {
                SenderId = Global.LoggedInUser.Id,
                ReceiverId = recipient.Id,
                Content = messageContent,
                SentOn = DateTime.Now
            };

            var messageService = new MessageService();
            bool success = await messageService.SendMessageAsync(newMessage);

            if (success)
            {
                MessageBox.Show("Message sent successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox1.Clear();
                textBox3.Clear();
                textBox2.Clear();
            }
            else
            {
                MessageBox.Show("Failed to send message. Try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            if (Global.LoggedInUser == null)
            {
                MessageBox.Show("You must log in first to see your comments.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var userId = Global.LoggedInUser.Id;
            var comments = await commentService.GetCommentsByUserIdAsync(userId);

            if (comments == null || comments.Count == 0)
            {
                MessageBox.Show("No comments found for your account.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                textBoxComments.Clear(); // Asigurăm că ștergem conținutul anterior
                foreach (var comment in comments)
                {
                    textBoxComments.AppendText(comment.ToString() + Environment.NewLine);
                }
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            if (Global.LoggedInUser == null)
            {
                MessageBox.Show("You must log in first to see your posts.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var userId = Global.LoggedInUser.Id;
            var posts = await postService.GetPostsByUserIdAsync(userId);

            if (posts == null || posts.Count == 0)
            {
                MessageBox.Show("No posts found for your account.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                textBoxPosts.Clear(); 
                foreach (var post in posts)
                {
                    textBoxPosts.AppendText(post.ToString() + Environment.NewLine);
                }
            }
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            string name = textBox5.Text.Trim();
            string email = textBox6.Text.Trim();

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Please fill in all fields!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var success = await userService.UpdateUserProfileAsync(Global.LoggedInUser.Id, name, email);

            if (success)
            {
                Global.LoggedInUser.Name = name;
                Global.LoggedInUser.Email = email;

                MessageBox.Show("Profile updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Failed to update profile.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            string title = textBox8.Text.Trim();

            if (string.IsNullOrWhiteSpace(title))
            {
                MessageBox.Show("Please enter a title.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var success = await postService.DeletePostByTitleAsync(title, Global.LoggedInUser.Id);

            if (success)
            {
                MessageBox.Show("Post deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No matching post found or deletion failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
    public static class Global
    {
        public static User LoggedInUser { get; set; } = null;
    
    }
}
