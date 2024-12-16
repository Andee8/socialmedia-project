package socialmedia.server;

import org.springframework.boot.CommandLineRunner;
import org.springframework.stereotype.Component;
import socialmedia.server.post.Post;
import socialmedia.server.post.PostService;
import socialmedia.server.post.Status;

import java.util.List;

@Component
public class CrudTest{ //implements CommandLineRunner {

   /* private final PostService postService;

    public CrudTest(PostService postService) {
        this.postService = postService;
    }

    @Override
    public void run(String... args) {
        System.out.println("== Start CRUD Tests for Post ==");
        // 1. Create a new Post
        Post post1 = new Post("First Post", "This is the first post content", Status.PENDING);
        postService.create(post1);
        System.out.println("Post added: " + post1);

        // 2. Create another Post
        Post post2 = new Post("Second Post", "Content for the second post", Status.PENDING);
        postService.create(post2);
        System.out.println("Post added: " + post2);

        // 3. Retrieve all Posts
        List<Post> posts = postService.findAllPosts();
        System.out.println("List of all posts: " + posts);

        // 4. Update a Post
        post1.setContent("Updated content for the first post");
        post1.setStatus(Status.PUBLISHED);
        postService.updatePost(post1.getId(), post1);
        System.out.println("Post updated: " + post1);

        // 5. Delete a Post
        System.out.println("Deleting post with ID = " + post2.getId());
        postService.deletePost(post2.getId());
        System.out.println("Post deleted.");

        // 6. Retrieve all Posts again
        posts = postService.findAllPosts();
        System.out.println("List of all posts after delete: " + posts);

        System.out.println("== End of CRUD Tests for Post ==");
    }*/
}
