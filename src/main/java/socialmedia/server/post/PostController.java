package socialmedia.server.post;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;
import java.util.List;

@RestController
@RequestMapping("/api/posts")
public class PostController {

    private final PostService postService;

    @Autowired
    public PostController(PostService postService) {
        this.postService = postService;
    }
    public PostRepository repository;

    @PostMapping
    public Post createPost(@RequestBody Post post) {
        return postService.create(post);
    }

    @GetMapping
    public List<Post> getAllPosts() {
        System.out.println("GET /api/posts called");
        return postService.findAllPosts();
    }
    @GetMapping("/postsByUser/{username}")
    public List<Post> getPostsByUser(@PathVariable String username) {
        return postService.getPostsByUser(username);
    }
    @GetMapping("/byTitleAndUser")
    public List<Post> getPostsByTitleAndUser(@RequestParam String title, @RequestParam String userName) {
        return postService.getPostsByTitleAndUser(title, userName);
    }

    @DeleteMapping
    public ResponseEntity<?> deletePostByTitleAndUserId(@RequestParam String title, @RequestParam int userId) {
        List<Post> posts = repository.findByTitleAndUserId(title, userId);

        if (posts == null || posts.isEmpty()) {
            return ResponseEntity.notFound().build();
        }

        repository.deleteAll(posts);
        return ResponseEntity.ok("Posts deleted successfully.");
    }
}
