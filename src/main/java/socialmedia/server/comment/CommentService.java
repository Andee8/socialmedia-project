package socialmedia.server.comment;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.Optional;

@Service
public class CommentService {

    @Autowired
    private CommentRepository repository;

    public List<Comment> getAllComments() {
        return repository.findAll();
    }

    public Comment createComment(Comment comment) {
        return repository.save(comment);
    }

    public Comment updateComment(int id, Comment updatedComment) {
        Optional<Comment> existingComment = repository.findById(Long.valueOf(id));
        if (existingComment.isPresent()) {
            Comment comment = existingComment.get();
            comment.setContent(updatedComment.getContent());
            return repository.save(comment);
        } else {
            throw new RuntimeException("Comment with ID " + id + " not found");
        }
    }

    public void deleteComment(int id) {
        repository.deleteById((long) id);
    }
    public List<Comment> getAllCommentsForPost(int postId) {
        return repository.findAllCommentsByPost(postId);
    }

}
