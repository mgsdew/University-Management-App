using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace UniversityManagementApp.Models
{
    public class SampleData: DropCreateDatabaseIfModelChanges<UniversityDbContext>
    {
        protected override void Seed(UniversityDbContext context)
        {
            var departments = new List<Department>
            {
                new Department {Name = "Computer Science", Code = "CS"},
                new Department {Name = "Civil Engineering", Code = "CE"},
                new Department {Name = "Electrical Engineering", Code = "EE"},
                new Department {Name = "Mechanical Engineering", Code = "ME"},
            };
            foreach (Department department in departments)
            {
                context.Departments.Add(department);
            }
            context.SaveChanges();

            var semesters = new List<Semester>
            {
                new Semester{Name="1st"},
                new Semester{Name="2nd"},
                new Semester{Name="3rd"},
                new Semester{Name="4th"},
                new Semester{Name="5th"},
                new Semester{Name="6th"},
                new Semester{Name="7th"},
                new Semester{Name="8th"},
            };
            foreach (Semester semester in semesters)
            {
                context.Semesters.Add(semester);
            }
            context.SaveChanges();

            var classRooms = new List<ClassRoom>
            {
                new ClassRoom{RoomNo= "A1"},
                new ClassRoom{RoomNo= "A2"},
                new ClassRoom{RoomNo= "A3"},
                new ClassRoom{RoomNo = "A5"},
                new ClassRoom{RoomNo= "A6"},
            };
            foreach (ClassRoom room in classRooms)
            {
                context.ClassRooms.Add(room);
            }
            context.SaveChanges();

            var weekDays = new List<WeekDay>
            {
                new WeekDay{Day="Sunday"},
                new WeekDay{Day="Monday"},
                new WeekDay{Day="Tuesday"},
                new WeekDay{Day="Wednesday"},
                new WeekDay{Day="Thursday"},
            };
            foreach (WeekDay weekDay in weekDays)
            {
                context.WeekDays.Add(weekDay);
            }
            context.SaveChanges();

            var grades = new List<ResultGrade>
            {
                new ResultGrade{GradeLater = "A",GradeValue = 4},
                new ResultGrade{GradeLater = "B",GradeValue = 3},
                new ResultGrade{GradeLater = "C",GradeValue = 2},
                new ResultGrade{GradeLater = "D",GradeValue = 1},
                new ResultGrade{GradeLater = "F",GradeValue = 0}
            };
            foreach (ResultGrade grade in grades)
            {
                context.ResultGrades.Add(grade);
            }
            context.SaveChanges();

            var students = new List<Student>
            {
                new Student
                {
                    Name = "Faisal",
                    RegNo = "CS2014001",
                    DepartmentId = 1,
                    Address = "Rajshahi",
                    ContactcNo = "0123456789",
                    Email = "faisal@mail.com",
                    RegistrationDate = Convert.ToDateTime("1/1/2014")
                },
                new Student
                {
                    Name = "Sahed",
                    RegNo = "CS2014002",
                    DepartmentId = 1,
                    Address = "Rajshahi",
                    ContactcNo = "0123456789",
                    Email = "sahed@mail.com",
                    RegistrationDate = Convert.ToDateTime("1/1/2014")
                },
                new Student
                {
                    Name = "Sabbir",
                    RegNo = "CS2014003",
                    DepartmentId = 1,
                    Address = "Rajshahi",
                    ContactcNo = "0123456789",
                    Email = "sabbir@mail.com",
                    RegistrationDate = Convert.ToDateTime("1/1/2014")
                },
                new Student
                {
                    Name = "Sina",
                    RegNo = "CS2014004",
                    DepartmentId = 1,
                    Address = "Rajshahi",
                    ContactcNo = "0123456789",
                    Email = "sina@mail.com",
                    RegistrationDate = Convert.ToDateTime("1/1/2014")
                },
            };

            foreach (var student in students)
            {
                context.Students.Add(student);
            }
            context.SaveChanges();

            var designations = new List<Designation>
            {
                new Designation {Name = "Head Of Department"},
                new Designation {Name = "Proffesor"},
                new Designation {Name = "Asst. Proffesor"}
            };
            foreach (Designation designation in designations)
            {
                context.Designations.Add(designation);
            }
            context.SaveChanges();


            var courses = new List<Course>
            {
                new Course
                {
                    Code = "CSE 101",
                    Name = "Programming Basics",
                    Description = "Basic Programming learning",
                    Credit = 4,
                    DepartmentId = 1,
                    SemesterId = 1
                },
                new Course
                {
                    Code = "CE 101",
                    Name = "Civil Engineering Basics",
                    Description = "Basic Civil Engineering learning",
                    Credit = 4,
                    DepartmentId = 2,
                    SemesterId = 1
                }
            };
            foreach (Course course in courses)
            {
                context.Courses.Add(course);
            }
            context.SaveChanges();


            var teachers = new List<Teacher>
            {
                new Teacher{Name="Md. Sahiduzzaman",Email = "zaman@mail.com",DesignationId = 1,Address = "Rajshahi",ContactNo = "01234567890",DepartmentId = 1,TotalCredit = 20},
                new Teacher{Name="Basir Ahmed",Email = "basir@mail.com",DesignationId = 2,Address = "Rajshahi",ContactNo = "01234567890", DepartmentId = 1,TotalCredit = 20},
                new Teacher{Name="Robiul Islam",Email = "rabiul@mail.com",DesignationId = 3,Address = "Rajshahi",ContactNo = "01234567890", DepartmentId = 1,TotalCredit = 20}
            };
            foreach (Teacher teacher in teachers)
            {
                context.Teachers.Add(teacher);
            }
            context.SaveChanges();
        }
    }
}