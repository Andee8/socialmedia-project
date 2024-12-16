package socialmedia.server.comment;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/api/comments")
public class CommentController {

    @Autowired
    private CommentService commentService;

    // Retrieve all comments
    @GetMapping
    public List<Comment> getAllComments() {
        return commentService.getAllComments();
    }

    // Create a new comment
    @PostMapping
    public Comment createComment(@RequestBody Comment comment) {
        return commentService.createComment(comment);
    }

    // Retrieve comments for a specific post
    @GetMapping("/post/{postId}")
    public List<Comment> getAllCommentsForPost(@PathVariable int postId) {
        return commentService.getAllCommentsForPost(postId);
    }
}
