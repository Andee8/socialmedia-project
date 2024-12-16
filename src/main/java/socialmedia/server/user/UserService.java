package socialmedia.server.user;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.Optional;

@Service
public class UserService {

    @Autowired
    private UserRepository repository;

    public List<User> getAllUsers() {
        return repository.findAll();
    }

    public User createUser(User user) {
        return repository.save(user);
    }

    public User updateUser(int id, User updatedUser) {
        Optional<User> existingUser = repository.findById((long) id);
        if (existingUser.isPresent()) {
            User user = existingUser.get();
            user.setName(updatedUser.getName());
            user.setEmail(updatedUser.getEmail());
            return repository.save(user);
        } else {
            throw new RuntimeException("User with ID " + id + " not found");
        }
    }
    public List<User> findUsersByName(String name) {
        return repository.findByName(name);
    }

    public void deleteUser(int id) {
        repository.deleteById((long) id);
    }
}
