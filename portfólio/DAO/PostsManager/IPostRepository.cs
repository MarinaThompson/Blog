using portfólio.Models;
using portfólio.ModelViews;
using System.Collections.Generic;

namespace portfólio.DAO.PostManager
{
   public interface IPostRepository
    {
        IEnumerable<Post> GetPosts();
        Post GetPost(int id);
        Post CreatePost(Post post);
        IEnumerable<Post> MyPosts(string userID);
        bool DeletePost(Post post);
        Post UpdatePost(Post post);

        string Username(int userID);
    }
}
