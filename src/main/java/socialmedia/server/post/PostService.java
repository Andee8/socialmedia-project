package socialmedia.server.post;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.Optional;
import java.util.stream.Collectors;

@Service
public class PostService {

    @Autowired
    private PostRepository repository;

    // Create a new Post
    public Post create(Post post) {
        return repository.save(post);
    }

    // Retrieve all Posts
    public List<Post> findAllPosts() {
        return repository.findAll();
    }

    // Update an existing Post
    public Post updatePost(int id, Post updatedPost) {
        Optional<Post> existingPost = repository.findById(id);
        if (existingPost.isPresent()) {
            Post post = existingPost.get();
            post.setTitle(updatedPost.getTitle());
            post.setContent(updatedPost.getContent());
            post.setStatus(updatedPost.getStatus());
            return repository.save(post);
        } else {
            throw new RuntimeException("Post with ID " + id + " not found");
        }
    }

    // Delete a Post by ID
    public void deletePost(int id) {
        repository.deleteById(id);
    }
    public List<Post> getAllPublishedPostsForUser(int userId) {
        return repository.findAllPublishedPostsByUser(userId, Status.PUBLISHED);
    }
    public List<Post> getPostsByUser(String username) {
        return repository.findAll().stream()
                .filter(post -> post.getUser().getName().equalsIgnoreCase(username))
                .collect(Collectors.toList());
    }
    public List<Post> getPostsByTitleAndUser(String title, String userName) {
        return repository.findByTitleAndUserName(title, userName);

    }
}
