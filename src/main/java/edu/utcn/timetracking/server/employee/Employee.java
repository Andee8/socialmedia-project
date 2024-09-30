package edu.utcn.timetracking.server.employee;

import io.swagger.annotations.ApiModelProperty;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;
import org.hibernate.annotations.CreationTimestamp;

import javax.persistence.*;
import java.util.Date;

@Entity
@Data
@AllArgsConstructor
@NoArgsConstructor
public class Employee {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @ApiModelProperty(hidden = true)
    private Integer id;

    private String name;
    private String hourlyRate;

    @Column(updatable = false, nullable = false)
    @CreationTimestamp
    @ApiModelProperty(hidden = true)
    private Date enrollDate;
}
