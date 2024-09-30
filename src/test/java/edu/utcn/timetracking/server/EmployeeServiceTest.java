package edu.utcn.timetracking.server;

import edu.utcn.timetracking.server.employee.Employee;
import edu.utcn.timetracking.server.employee.EmployeeRepository;
import edu.utcn.timetracking.server.employee.EmployeeService;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.mockito.InjectMocks;
import org.mockito.Mock;
import org.mockito.MockitoAnnotations;

import java.util.ArrayList;
import java.util.Date;
import java.util.List;

import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.mockito.Mockito.*;

public class EmployeeServiceTest {
    @Mock
    private EmployeeRepository employeeRepository;

    @InjectMocks
    private EmployeeService employeeService; // this is the service under test

    @BeforeEach
    void setUp() {
        MockitoAnnotations.initMocks(this);
    }

    @Test
    void testFindAllEmployees(){
        // setup test
        Employee emp1 = new Employee(1, "Vasile", "10", new Date(1));
        Employee emp2 = new Employee(1, "Vasile", "10", new Date(1));
        List<Employee> employeesMock = new ArrayList<>();
        employeesMock.add(emp1);
        employeesMock.add(emp2);
        when(employeeRepository.findAll()).thenReturn(employeesMock);

        // call method under test
        List<Employee> employees = employeeService.findAllEmployees();

        // check what was called by the method and check the result
        verify(employeeRepository, times(1)).findAll();
        assertEquals(2, employees.size());
        assertEquals(emp1, employees.get(0));
        assertEquals(emp2, employees.get(1));
    }
}
