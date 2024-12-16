package socialmedia.server.user;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/api/users")
public class UserController {

    @Autowired
    private UserService service;
    private  UserRepository repository;

    @GetMapping
    public List<User> getAllUsers() {
        return service.getAllUsers();
    }

    @PostMapping
    public User createUser(@RequestBody User user) {
        return service.createUser(user);
    }

    @DeleteMapping("/{id}")
    public void deleteUser(@PathVariable int id) {
        service.deleteUser(id);
    }
    @GetMapping("/users/{username}")
    public List<User> getUsersByName(@PathVariable String username) {
        return service.findUsersByName(username);
    }
    @GetMapping("/usersByEmail/{email}")
    public ResponseEntity<List<User>> getUsersByEmail(@PathVariable String email) {
        List<User> users = repository.findByEmail(email);
        return ResponseEntity.ok(users);
    }
    @PutMapping("/{id}")
    public ResponseEntity<?> updateUser(@PathVariable int id, @RequestBody User updatedUser) {
        User user = repository.findById((long) id).orElse(null);
        if (user == null) {
            return ResponseEntity.notFound().build();
        }

        user.setName(updatedUser.getName());
        user.setEmail(updatedUser.getEmail());

        repository.save(user);
        return ResponseEntity.ok(user);
    }

}
